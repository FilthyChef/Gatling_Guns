using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Guns")]
    public class XD4 : Gun
    {
        public XD4(float xval, float yval)
          : base(xval, yval)
        {
            this._editorName = "XD 4 Ported";
			this.editorTooltip = "Versatile pistol from stevenl15's weapon stash.";
			this.ammo = 13;
            this._ammoType = new AT9mm();
            this._ammoType.range = 300f;
            this._ammoType.accuracy = 0.75f;
            this._ammoType.penetration = 0f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Items/Guns/XD4"));
            this.center = new Vec2(6f, 5f);
            this.collisionOffset = new Vec2(-8f, -6f);
            this.collisionSize = new Vec2(15f, 11f);
            this._barrelOffsetTL = new Vec2(15f, 2f);
            this._fireSound = GetPath("Ammo/XD4.wav");
            this._fullAuto = false;
            this._fireWait = 1f;
            this._kickForce = 1f;
            this.loseAccuracy = 0.15f;
            this.maxAccuracyLost = 0.45f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        /*public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                this.ammo--;
                if (this.isServerForObject)
                {
                    Crate crate = new Crate(0, 0);
                    crate.position = Offset(this._barrelOffsetTL);
                    crate.vSpeed = this.barrelVector.x * 7f;
                    crate.hSpeed = this.barrelVector.y * 7f;
                    Level.Add((Thing)crate);
                }
            }
            base.OnPressAction();
        }*/


    }
}
