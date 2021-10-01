using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    [BaggedProperty("isFatal", false)]
    public class CigaretteEssence : GrenadeMain{
        public CigaretteEssence(float xval, float yval) : base(xval, yval){
            _editorName = "Cigarette Essence";
			this.editorTooltip = "Hide yourself from view with power of 50 smoked fags!";
            sprite = new SpriteMap(GetPath("Items/Explosives/cigaretteEssence.png"), 16, 16, false);
            graphic = sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
        }
        public override void Initialize(){
            base.Initialize();
        }
        public override void Explode(){
            MistOfSmoke(30);
            SFX.Play(GetPath("Items/Explosives/cigaretteEssenceExplode.wav"), 1f, 0.0f, 0.0f, false);
            Level.Remove(this);
            base.Explode();
        }
        public virtual void MistOfSmoke(int smokeAmount){
            for(int i = 0; i < smokeAmount; i++){
                Level.Add(new Nicotine(x, y, 9f + Rando.Float(1f)));
            }
        }
    }
}
