using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    public class HotPotatoGame : GrenadeMain
    {
        public HotPotatoGame(float xval, float yval) : base(xval, yval)
        {
            Timer = Rando.Float(5f, 20f);
            
            _editorName = "Hot Potato Game";
			this.editorTooltip = "Play the good old Hot Potato game, until potato explodes.";
            _bio = "Play the good old Hot Potato game, until potato explodes.";
            sprite = new SpriteMap(GetPath("Items/Explosives/hotPotatoGame.png"), 16, 16, false);
            graphic = sprite;
            center = new Vec2(7f, 8f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 10f);
        }

        public virtual void CreateExplosion(Vec2 pos)
        {
            float xpos = pos.x;
            float ypos = pos.y - 2f;

            Level.Add(new ExplosionPart(xpos, ypos, true));

            int num1 = 6;

            if(Graphics.effectsLevel < 2)
            {
                num1 = 3;
            }

            for(int index = 0; index < num1; ++index)
            {
                float deg = (float)index * 60f + Rando.Float(-10f, 10f);
                float num2 = Rando.Float(12f, 20f);
                Level.Add(new ExplosionPart(xpos + (float)Math.Cos((double)Maths.DegToRad(deg)) * num2, ypos - (float)Math.Sin((double)Maths.DegToRad(deg)) * num2, true));
            }
        }

        public virtual void CreateBullets()
        {
            if(Network.isServer)
            {
                for(int index = 0; index < 20; ++index)
                {
                    float num2 = (float)((double)index * 18.0 - 5.0) + Rando.Float(10f);

                    ATShrapnel atShrapnel = new ATShrapnel();
                    atShrapnel.range = 60f + Rando.Float(18f);

                    Bullet bullet = new Bullet(x + (float)(Math.Cos((double)Maths.DegToRad(num2)) * 6.0), (y - 2f) - (float)(Math.Sin((double)Maths.DegToRad(num2)) * 6.0), atShrapnel, num2, null, false, -1f, false, true);
                    bullet.firedFrom = this;
                    Level.Add(bullet);

                    firedBullets.Add(bullet);
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Explode()
        {
            CreateExplosion(position);
            DestroyWindowsInRadius(40f);
            CreateBullets();
            
            SFX.Play("explode", 1f, 0.0f, 0.0f, false);

            Level.Remove(this);

            base.Explode();
        }
    }
}
