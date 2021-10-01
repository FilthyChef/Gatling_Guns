using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    public class MustardGasCan : GrenadeMain
    {
        public MustardGasCan(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Mustard Gas Can";
            _bio = "Releases deadly Mustard Gas, lethal for the ones who don't wear Tactical Mask";
            editorTooltip = "Releases deadly Mustard Gas, lethal for the ones who don't wear Tactical Mask";
            sprite = new SpriteMap(GetPath("Items/Explosives/mustardGasCan.png"), 16, 16, false);
            graphic = sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 11f);
        }

        public override void Explode()
        {
            MistOfMustardSmoke(30);

            SFX.Play(GetPath("Items/Explosives/cigaretteEssenceExplode.wav"), 1f, 0.0f, 0.0f, false);

            Level.Remove(this);

            base.Explode();
        }

        public virtual void MistOfMustardSmoke(int toxicSmokeAmount)
        {
            for(int i = 0; i < toxicSmokeAmount; i++)
            {
                Level.Add(new MustardGasSmoke(x, y, 6f + Rando.Float(1f)));
            }
        }
    }
}
