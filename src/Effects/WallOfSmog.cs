using System;
using DuckGame;
namespace DuckGame.GatlingGuns{
    public class WallOfSmog : Thing{
        public float Timer{
            get;
            set;
        }
        Vec2 move;
        float angleIncrement;
        float scaleDecrement;
        float fastGrowTimer;
        int f;
        float speed = 1;
        SpriteMap WallOfSmogSprite;
        public WallOfSmog(float xval, float yval, sbyte ofd,float spe) : base(xval, yval){
            speed = spe;
            this.offDir = ofd;
            velocity = new Vec2(Rando.Float(-5, 5), Rando.Float(-5, 5));
            this.yscale = 1.8f;
            this.xscale = 2.2f;
            WallOfSmogSprite = new SpriteMap(GetPath("Effects/wallOfSmog.png"), 58, 500); // Half Polish Half English
            graphic = WallOfSmogSprite;
            center = new Vec2(0.0f, 0.0f);
            depth = 0.5f;
			//Do some up-scalling?
        }
        public override void Initialize(){base.Initialize();}
        public override void Update(){
            position += new Vec2(speed * offDir, 0);
				foreach (Duck LateDuck in Level.CheckRectAll<Duck>(new Vec2(base.x, base.y - 9999), new Vec2(base.x + 122, base.y + 9999))){	// The coordinates of NeverLand
                    LateDuck.Kill(new DTCrush(LateDuck));	//Replace with MustardGasOnDuck death type?
                }
				foreach (RagdollPart LateDuckRagdolled in Level.CheckRectAll<RagdollPart>(new Vec2(base.x, base.y - 9999), new Vec2(base.x + 122, base.y + 9999))){ 
					LateDuckRagdolled._doll._duck.Kill(new DTCrush(LateDuckRagdolled._doll._duck));
				}
            if (xscale < 0.100000001490116){Level.Remove(this);}
        }
        public override void Draw(){base.Draw();}
    }
}
