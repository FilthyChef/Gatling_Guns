using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    public class ArcticEssence : GrenadeMain
    {
        private SpriteMap _sprite;
        public ArcticEssence(float xval, float yval) : base(xval, yval)
		{
            this._editorName = "Arctic Essence";
			this.editorTooltip = "Releases a chilling Arctic mist.";
            this._bio = "Releases a chilling Arctic mist.";
            this._sprite = new SpriteMap(this.GetPath("Items/Explosives/arcticEssence"), 16, 16, false);
            this.graphic = (Sprite)this._sprite;
            this.center = new Vec2(7f, 8f);
            this.collisionOffset = new Vec2(-4f, -5f);
            this.collisionSize = new Vec2(8f, 10f);
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }
        
        public override void Explode()
        {
            QuickFlash();
            if(Network.isServer)
			{
                DestroyWindowsInRadius(40f);
                ArcticExplosion(100);
            }
            SFX.Play("fart3", 1f, 0.0f, 0.0f, false);
            Level.Remove(this);
            base.Explode();
        }

		public virtual void ArcticExplosion(int smokeAmount)
        {
            for (int i=0; i<=smokeAmount; i++)
			{
				Vec2 moveSpeed=new Vec2(Rando.Float(-3,3),Rando.Float(-3,3));
				ExtinguisherSmoke smoke=new ExtinguisherSmoke(base.barrelPosition.x, base.barrelPosition.y, false);
				smoke.hSpeed=moveSpeed.x;
				smoke.vSpeed=moveSpeed.y;
				Level.Add(smoke);
			}
        }
    }
}
