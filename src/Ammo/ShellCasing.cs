using System;

namespace DuckGame.GatlingGuns
    {
	[BaggedProperty("isInDemo", true)]
	[BaggedProperty("canSpawn", false)]
	public class ShellPhysics : PhysicsObject
		{
        EjectedMilitaryShell par;
        Vec2 prev = new Vec2(9999,999);
        int i = 0;
        public ShellPhysics(float xval, float yval, Sprite ShellSprite, EjectedMilitaryShell ShellPhysx) : base(xval, yval)
        {
            par = ShellPhysx;
            this.graphic = ShellSprite;
            this.visible = false;
            this.collisionSize = new Vec2(graphic.width, graphic.height);
            this.center = new Vec2(graphic.width, graphic.height);
            this.collisionOffset = new Vec2(-graphic.width, -graphic.height + 2);
        }
        public override void Removed()
        {
            base.Removed();
        }
        public override void Update()
        {
            foreach (Duck cor in Level.CheckCircleAll<Duck>(new Vec2(base.x, base.y), this.graphic.height + 4))
            {
                i++;
                par.iso = false;

            }
            foreach (RagdollPart cor in Level.CheckCircleAll<RagdollPart>(new Vec2(base.x, base.y), this.graphic.height + 4))
            {
                i++;
                par.iso = false;

            }
            if (i != 0)
            {
                Level.Remove(this);
            }
            i = 0;
            if (prev == par.position)
            {
                this.vSpeed = 0;
                this.hSpeed = 0;
            }
            else
            {
                this.hSpeed = par.hSpeed;
                this.vSpeed = par.vSpeed;
            }
            prev = par.position;
            //this.position = par.position;
         
				this.angle = par.angle;
				base.Update();
        }
    }
}