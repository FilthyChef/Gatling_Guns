using System;
using DuckGame;
namespace DuckGame.GatlingGuns
{
	[EditorGroup("Equipment|Gatling Guns|Guns")]
	public class M27IAR : Gun
	{
		public M27IAR(float xval, float yval) : base(xval, yval){
			this.editorTooltip = "Lightweight Carbine with moderate recoil.";
			this.ammo = 31;
			this._ammoType = new ATMilitary();
			this._type = "gun";
			this.graphic = new Sprite(GetPath("Items/Guns/m27IAR.png"));
			this.center = new Vec2(23f, 7f);
			this.collisionOffset = new Vec2(-1f, -1f);
			this.collisionSize = new Vec2(18f, 10f);
			this._barrelOffsetTL = new Vec2(47f, 5f);
			this._fireSound = "deepMachineGun2";
			this._fullAuto = true;
			this._fireWait = 0.9f;
			this._kickForce = 0f;
			this.loseAccuracy = 0.10f;
			this.maxAccuracyLost = 0.8f;
		}
	}
}
