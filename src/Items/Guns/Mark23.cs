using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Guns")]
    public class Mark23 : Gun
    {
        public Mark23(float xval, float yval)
          : base(xval, yval)
        {
            this._editorName = "Mark 23";
			this.editorTooltip = ".45 Caliber, huge size, SEAL's pistol of choice. From stevenl15's weapon stash.";
			this.ammo = 13;
            this._ammoType = new AT9mm();
            this._ammoType.range = 320f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Items/Guns/Mark23"));
            this.center = new Vec2(5f, 4f);
            this.collisionOffset = new Vec2(-8f, -6f);
            this.collisionSize = new Vec2(16f, 9f);
            this._barrelOffsetTL = new Vec2(16f, 1f);
            this._fireSound = GetPath("Ammo/Mark23.wav");
            this._fullAuto = false;
            this._fireWait = 1.8f;
            this._kickForce = 2f;
            this.loseAccuracy = 0.3f;
            this.maxAccuracyLost = 0.9f;
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
