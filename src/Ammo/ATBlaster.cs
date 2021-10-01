using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;

namespace DuckGame.GatlingGuns
{
    public class ATBlaster : ATLaser
    {
        public ATBlaster()
        {
            this.accuracy = 0.3f;
            this.range = 600f;
            this.penetration = 0f;
            this.bulletSpeed = 10f;
            this.bulletLength = 40f;
            this.bulletThickness = 0.3f;
            this.rangeVariation = 50f;
            this.bulletType = typeof(BlasterBullet);
            this.angleShot = false;
        }
    }
	public class BlasterBullet : Bullet
    {
        private Tex2D _beem;

        private float _thickness;

        public BlasterBullet(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this._thickness = type.bulletThickness;
            this._beem = Content.Load<Tex2D> (GetPath("Ammo/blasterBeam.png"));
        }

        public override void Draw()
        {
            if (this._tracer)
            {
                return;
            }
            if (this._bulletDistance > 0.1f)
            {
                float length = (this.drawStart - this.drawEnd).length;
                float num = 0f;
                float num2 = 1f / (length / 8f);
                float num3 = 0f;
                float num4 = 8f;
                while (true)
                {
                    bool flag = false;
                    if (num + num4 > length)
                    {
                        num4 = length - Maths.Clamp(num, 0f, 99f);
                        flag = true;
                    }
                    num3 += num2;
                    Graphics.DrawTexturedLine(this._beem, this.drawStart + this.travelDirNormalized * num, this.drawStart + this.travelDirNormalized * (num + num4), Color.White * num3, this._thickness, 0.6f);
                    if (flag)
                    {
                        break;
                    }
                    num += 8f;
                }
                return;
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            Bullet.isRebound = true;
            BlasterBullet BlasterBullet = new BlasterBullet(pos.x, pos.y, this.ammo, dir, null, this.rebound, rng, false, false);
            Bullet.isRebound = false;
            BlasterBullet._teleporter = this._teleporter;
            BlasterBullet.firedFrom = base.firedFrom;
            Level.current.AddThing(BlasterBullet);
            Level.current.AddThing(new LaserRebound(pos.x, pos.y));
        }
    }
}