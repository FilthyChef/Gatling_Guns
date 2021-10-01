using System;
using Microsoft.Xna.Framework.Graphics;
namespace DuckGame.GatlingGuns
{
    public class ATPhasaber : ATLaser
    {
        public ATPhasaber()
        {
            this.accuracy = 0.8f;
            this.range = 600f;
            this.penetration = 1f;
            this.bulletSpeed = 10f;
            this.bulletThickness = 0.3f;
            this.bulletLength = 40f;
            this.rangeVariation = 50f;
            this.bulletType = typeof(PhasaberBullet);
            this.angleShot = false;
        }
        public override void WriteAdditionalData(BitBuffer b)
        {
            base.WriteAdditionalData(b);
            b.Write(this.bulletThickness);
            b.Write(this.penetration);
        }
        public override void ReadAdditionalData(BitBuffer b)
        {
            base.ReadAdditionalData(b);
            this.bulletThickness = b.ReadFloat();
            this.penetration = b.ReadFloat();
        }
    }
	public class PhasaberBullet : Bullet
    {
        private Texture2D _beem;

        private float _thickness;

        public PhasaberBullet(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this._thickness = type.bulletThickness;
            this._beem = Content.Load<Texture2D>("laserBeam");
        }
        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            if(t is Door)
            {
                Level.Remove(t);
            }
            if(t is VerticalDoor)
            {
                Level.Remove(t);
            }
            base.OnCollide(pos, t, willBeStopped);
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
                float dist = 0f;
                float incs = 1f / (length / 8f);
                float alph = 0f;
                float drawLength = 8f;
                while (true)
                {
                    bool doBreak = false;
                    if (dist + drawLength > length)
                    {
                        drawLength = length - Maths.Clamp(dist, 0f, 99f);
                        doBreak = true;
                    }
                    alph += incs;
                    Graphics.DrawTexturedLine(this._beem, this.drawStart + this.travelDirNormalized * dist, this.drawStart + this.travelDirNormalized * (dist + drawLength), Color.White * alph, this._thickness, 0.6f);
                    if (doBreak)
                    {
                        break;
                    }
                    dist += 8f;
                }
                return;
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            Bullet.isRebound = true;
            PhasaberBullet bullet = new PhasaberBullet(pos.x, pos.y, this.ammo, dir, null, this.rebound, rng, false, false);
            Bullet.isRebound = false;
            bullet._teleporter = this._teleporter;
            bullet.firedFrom = base.firedFrom;
            Level.current.AddThing(bullet);
            Level.current.AddThing(new LaserRebound(pos.x, pos.y));
        }
    }
}
