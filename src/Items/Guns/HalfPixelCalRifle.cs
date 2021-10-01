using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Guns")]
    [BaggedProperty("isSuperWeapon", true)]
    public class HalfPixelCalRifle : Sniper
    {
        public HalfPixelCalRifle(float xval, float yval) : base(xval, yval)
        {
            this._editorName = "5mil.Pix Caliber Rifle";
			this._bio = "Marksman rifle packed with rounds small enough to penetrate anything.";
			this.editorTooltip = "Marksman rifle packed with rounds small enough to penetrate anything.";
			this.graphic = new Sprite(GetPath("Items/Guns/halfPixelCalRifle.png"));
            this._center = new Vec2(18f, 9f/2f);
            this._collisionOffset = new Vec2(-17.5f, -9f/2f);
            this._collisionSize = new Vec2(35f, 9f);
            this._holdOffset = new Vec2(1.88f, 1f);
            this._barrelOffsetTL = new Vec2(33f, 2f);
            this._laserOffsetTL = new Vec2(34f, 2.5f);
            this.ammoType.penetration = 16f;
        }
    }
}