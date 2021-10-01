using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
	[EditorGroup("Equipment|Gatling Guns|Equipment")]
	public class TacticalMask : Hat
	{
		public TacticalMask(float xpos, float ypos) : base(xpos, ypos)
		{
			this._editorName = "Tactical Mask";
			this.editorTooltip = "Grants invulnerability to smoke effects.";
            this._pickupSprite = new Sprite(this.GetPath("Items/Equipment/tacticalMaskPickup.png"), 0f, 0f);
            this._sprite = new SpriteMap(this.GetPath("Items/Equipment/tacticalMask.png"), 32, 32, false);
            this.graphic = this._pickupSprite;
            this.center = new Vec2(4f, 4f);
            this.collisionOffset = new Vec2(-3f, -3f);
            this.collisionSize = new Vec2(7f, 10f);
            this._sprite.CenterOrigin();
            this._equippedThickness = 0f;
			this.physicsMaterial = PhysicsMaterial.Metal;
		}
		public override void Update()
		{
			base.Update();
		}
	}
}
