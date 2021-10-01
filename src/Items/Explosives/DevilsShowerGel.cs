using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    public class VolcanoEssence : GrenadeMain{
        public VolcanoEssence(float xval, float yval) : base(xval, yval){
            _editorName = "Devil's Shower Gel";
            _bio = "Not just into eye, don't let that get on any part of your body!";
			this.editorTooltip = "Not just into eye, don't let that get on any part of your body!";
            sprite = new SpriteMap(GetPath("Items/Explosives/volcanoEssence.png"), 16, 16, false);
            graphic = sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
        }
        public override void Initialize(){
            base.Initialize();
        }
        public override void Explode(){
            if(Network.isServer){
                DestroyWindowsInRadius(40f);
                FireExplosion(40);
            }
            SFX.Play(GetPath("Items/Explosives/volcanoEssenceExplode.wav"), 1f, 0.0f, 0.0f, false);
            Level.Remove(this);
            base.Explode();
        }
        public virtual void FireExplosion(int fireAmount){
            for(int i = 0; i < fireAmount; i++){
                float speed = Rando.Float(3f, 5f);
                Level.Add(SmallFire.New(x, y - 2f, Rando.Float(-speed, speed), Rando.Float(-speed, speed + 2f), false, (MaterialThing)null, true, (Thing)this, false));
            }
        }
    }
}
