using DuckGame;

namespace DuckGame.GatlingGuns
	{
		public class MilitaryShell : EjectedMilitaryShell
		{
			public MilitaryShell(float xpos, float ypos) : base(xpos, ypos, "Ammo/militaryShell.png", "Ammo/militaryShellBounce.wav")
			{
			}
		}
		public class HighVelocityShell : EjectedMilitaryShell
		{
			public HighVelocityShell(float xpos, float ypos) : base(xpos, ypos, "highVelocityShellShell.png", "highVelocityShellBounce.wav")
			{
			}
		}
		public class BuckshotShell : EjectedMilitaryShell
		{
			public BuckshotShell(float xpos, float ypos) : base(xpos, ypos, "buckshotShell.png", "buckshotShellBounce.wav")
			{
			}
		}
        public  class EjectedMilitaryShell : PhysicsParticle
        {
            ShellPhysics shell;
            public bool iso = false;
            int f;


        protected EjectedMilitaryShell(float xpos, float ypos, string shellSprite, string bounceSound = "Ammo/militaryShellBounce.wav") : base(xpos, ypos)
        {
            this.hSpeed = -4f - Rando.Float(3f);
            this.vSpeed = -(Rando.Float(1.5f) + 1f);
            this.graphic = new Sprite(GetPath(shellSprite));
            this.center = new Vec2(8f, 8f);
            this._bounceSound = GetPath(bounceSound);
            base.depth = 0.3f + Rando.Float(0f, 0.1f);
        }
        public override void Removed()
        {
            if (shell != null)
                Level.Remove(shell);
            base.Removed();
        }
        public override void Update()
        {
            base.Update();
			foreach (Duck cor in Level.CheckCircleAll<Duck>(new Vec2(base.x, base.y), this.graphic.height + 4))
            {
                f++;
            }
            foreach (RagdollPart cor in Level.CheckCircleAll<RagdollPart>(new Vec2(base.x, base.y), this.graphic.height + 4))
            {
                f++;
            }
            if (f != 0)
            {
                if (iso)
                {
                    Level.Remove(shell);
                    iso = false;
                }
            }
            else if(!iso)
            {
                shell = new ShellPhysics(base.x,base.y,graphic,this);
                iso = true;
                Level.Add(shell);
            }
            f = 0;
            /*if(vSpeed == 0 && mus == null)
             {
                 mus = new Casing(base.x,base.y);
                 mus.vSpeed = -2f;
                 mus._angle = base._angle;
                 Level.Add(mus);
             }
             if(vSpeed != 0 && mus != null)
             {
                 Level.Remove(mus);
             }
             */
             if(!_grounded)
            this._angle += 0.1f;
        }
    }
}
	
    