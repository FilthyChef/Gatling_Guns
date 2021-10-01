using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [BaggedProperty("isInDemo", true), EditorGroup("Equipment|Gatling Guns|Guns")]
    public class Blaster : Gun
    {
        public Blaster(float xval, float yval) : base(xval, yval)
        {
            this._editorName = "Blaster";
			this.editorTooltip = "Very inaccurate laser semi-automatic.";
            this.ammo = 36;
            this._ammoType = new ATBlaster();
            this._type = "gun";
            base.graphic = new Sprite(GetPath("Items/Guns/blaster"), 0f, 0f);
            this.center = new Vec2(16f, 16f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(16f, 7f);
            this._barrelOffsetTL = new Vec2(31f, 15f);
            this._fireSound = GetPath("Ammo/blasterFire.wav");
            this._fullAuto = true;
            this._fireWait = 1f;
            this._kickForce = 1f;
            this._holdOffset = new Vec2(0f, 0f);
            this._flare = new SpriteMap(GetPath("Items/Guns/blasterFlare"), 16, 16, false);
            this._flare.center = new Vec2(0f, 8f);
        }

        public override void OnPressAction()
        {
            this.Fire();
        }

        public override void OnHoldAction()
        {
        }
    }
}
