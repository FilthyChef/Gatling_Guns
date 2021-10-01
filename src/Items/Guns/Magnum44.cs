using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Guns")]
    public class Magnum44 : Gun
    {
        public Magnum44(float xval, float yval)
          : base(xval, yval)
        {
            this._editorName = "Magnum 44";
			this.editorTooltip = "The most powerful handgun in the world from stevenl15's weapon stash. Different recoil pattern compared to default Magnum.";
			this.ammo = 6;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 370f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Items/Guns/Magnum44"));
            this.center = new Vec2(6f, 5f);
            this.collisionOffset = new Vec2(-8f, -7f);
            this.collisionSize = new Vec2(16f, 11f);
            this._barrelOffsetTL = new Vec2(16f, 2f);
            this._fireSound = GetPath("Ammo/Magnum44.wav");
            this._fullAuto = false;
            this._fireWait = 2.4f;
            this._kickForce = 3f;
            this.loseAccuracy = 0.6f;
            this.maxAccuracyLost = 1.2f;
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
