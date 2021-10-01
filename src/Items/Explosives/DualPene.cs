using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns{
[EditorGroup("Equipment|Gatling Guns|Explosives")]
	public class DualPene : GrenadeMain{
		public DualPene(float xval, float yval) : base(xval, yval){
			_editorName = "Dual-Pene";
			this.editorTooltip = "Grenade enchanced to penetrate metal doors.";
			_bio = "Grenade enchanced to penetrate metal doors";
			sprite = new SpriteMap(GetPath("Items/Explosives/dualPene.png"), 16, 16, false);
			graphic = sprite;
			center = new Vec2(7f, 8f);
			collisionOffset = new Vec2(-4f, -5f);
			collisionSize = new Vec2(8f, 10f);
		}
		public virtual void CreateLasers(){
			for(int index = 0; index < 20; ++index){
				float num2 = (float)((double)index * 18.0 - 5.0) + Rando.Float(10f);

				ATPhaser atPhaser = new ATPhaser();
				atPhaser.range = 80f + Rando.Float(18f);
				atPhaser.penetration = 3f;

				LaserBullet laserBullet = new LaserBullet(x + (float)(Math.Cos((double)Maths.DegToRad(num2)) * 6.0), (y - 2f) - (float)(Math.Sin((double)Maths.DegToRad(num2)) * 6.0), atPhaser, num2, null, false, -1f, false, true);
				laserBullet.firedFrom = this;
				Level.Add(laserBullet);

				firedBullets.Add(laserBullet);
			}
		}
		public override void Initialize(){
			base.Initialize();
		}
		public override void Explode(){
			CreateLasers();
			SFX.Play("phaserLarge", 1f, 0.0f, 0.0f, false);
			Level.Remove(this);
			base.Explode();
		}
	}
}