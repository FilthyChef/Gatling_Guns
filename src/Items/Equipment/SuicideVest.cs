using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
	[EditorGroup("Equipment|Gatling Guns|Equipment")]
    public class SuicideVest : Equipment
    {
        private SuicideVestDetonator SuicideVestDetonator = null;
        protected SpriteMap sprite;
        private float time = 0;
        private bool exploded = false;

        public SuicideVest(float xp, float yp) : base(xp, yp)
        {
            this._editorName = "Suicide Vest";
			this.editorTooltip = "Once equipped, you'll be given a detonator to your hand, use it or pass it to someone else. Not sure how this will win you a round though.";
			sprite = new SpriteMap(GetPath("Items/Equipment/suicideVest"), 16, 16, true);
            this.graphic = (Sprite)this.sprite;
            _collisionSize = new Vec2(10, 8);
            _collisionOffset = new Vec2(-5, -2);
            this.center = new Vec2(8f, 8f);
            this._holdOffset = new Vec2(-3, -3);
            this._offset = new Vec2(-3f, 3f); 
            this._wearOffset = new Vec2(-2f, 0.0f);
        }

        public override void Update()
        {
            base.Update();
            time += 1;
        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
            time = 0;

            if (SuicideVestDetonator == null)
            {
                if (!Network.isActive || isServerForObject)
                {
                    SuicideVestDetonator = new SuicideVestDetonator(this, d.position.x, d.position.y);
                    Level.Add(SuicideVestDetonator);
                    d.GiveHoldable(SuicideVestDetonator);
                }
            }
        }

        public void Explode()
        {
            if (time > 30 && !exploded)
            {
                for (int index = 0; index < 6; ++index) //puff of smoke
                {
                    float deg = (float)index * 60f + Rando.Float(-10f, 10f);
                    float num2 = Rando.Float(12f, 20f);
                    Level.Add((Thing)new ExplosionPart(position.x + (float)Math.Cos((double)Maths.DegToRad(deg)) * num2, position.y - (float)Math.Sin((double)Maths.DegToRad(deg)) * num2, true));
                }

                SFX.Play("explode", 1f, 0.0f, 0.0f, false); //"boom" sound

                for (int index = 0; index < 12; ++index) //shrapnel that slices ducks
                {
                    float num2 = (float)((double)index * 30.0 - 10.0) + Rando.Float(20f);
                    ATShrapnel atShrapnel = new ATShrapnel();
                    atShrapnel.range = 25f + Rando.Float(10f);
                    Level.Add((Thing)new Bullet(this.x + (float)(Math.Cos((double)Maths.DegToRad(num2)) * 8.0), this.y - (float)(Math.Sin((double)Maths.DegToRad(num2)) * 8.0), (AmmoType)atShrapnel, num2, (Thing)null, false, -1f, false, true)
                    {
                        firedFrom = (Thing)this
                    });
                }

                exploded = true;

                if (_equippedDuck != null)
                {
                    equippedDuck.Kill(new DTRocketExplosion(this));
                }

                Level.Remove(this);
                this.Destroy();
            }
        }
    }

    [BaggedProperty("canSpawn", false)]
    public class SuicideVestDetonator : Gun
    {
        private SuicideVest connectedSuicideVest;

        public SuicideVestDetonator(SuicideVest SuicideVest, float xp, float yp) : base(xp, yp)
        {
            connectedSuicideVest = SuicideVest;

            _collisionOffset = new Vec2(-2, -4);
            _collisionSize = new Vec2(4, 8);
            this._holdOffset = new Vec2(-3, 0); 
            this.center = new Vec2(8f, 8f);

            this.graphic = (Sprite)new SpriteMap(GetPath("Items/Equipment/suicideVestDetonator"), 16, 16, false);

            ammo = 100;
        }

        public override void Update()
        {
            base.Update();
            ammo = 100;
        }

        public override void Fire()
        {
 	        //base.Fire(); //causes crash

            if(connectedSuicideVest != null)
                connectedSuicideVest.Explode();
        }
    }
}
