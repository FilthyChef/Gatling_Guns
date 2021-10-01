using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns{
    [EditorGroup("Equipment|Gatling Guns|Explosives")]
    [BaggedProperty("isFatal", false)]
    public class BlindingGrenade : GrenadeMain
    {
        public BlindingGrenade(float xval, float yval) : base(xval, yval){
            _editorName = "Blinding Grenade";
			this.editorTooltip = "Bright enough to blind, loud enough to break windows.";
            _bio = "Bright enough to blind, loud enough to break windows.";
            sprite = new SpriteMap(GetPath("Items/Explosives/blindingGrenade.png"), 16, 16, false);
            graphic = sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
        }
        public override void Initialize(){
            base.Initialize();
        }
        public override void Update(){
            base.Update();
        }
        public override void Explode(){
            QuickFlash(60f, 60f);
			DestroyWindowsInRadius(420f);
            SFX.Play(GetPath("Items/Explosives/blindingGrenadeExplode.wav"), 1f, 0.0f, 0.0f, false);
            Level.Remove(this);
            base.Explode();
        }
    }
}
