using System.Text;
using System.Reflection;
using DuckGame;


namespace DuckGame.GatlingGuns
{
	[EditorGroup("Equipment|Gatling Guns|Equipment")]
	public class pill : Equipment{
		private SpriteMap sprite;
		private Sprite _pickupSprite;
		public pill(float xpos, float ypos) : base(xpos, ypos){
			this._editorName = "pill";
			this.editorTooltip = "Infamous drug called Joy, known for its pain numbing properties.";
			this._pickupSprite = new Sprite(Mod.GetPath<GatlingGuns>("Items/Equipment/pill.png"), 0f, 0f);
			this.sprite = new SpriteMap(Mod.GetPath<GatlingGuns>("blank.png"), 26, 26, false);
			base.graphic = this._pickupSprite;
			this.center = new Vec2(7f, 11f);
            this.collisionOffset = new Vec2(-6f, -10f);
            this.collisionSize = new Vec2(13f, 18f);
			this.sprite.CenterOrigin();
			this._equippedThickness = 10f;
		}
		public override void Update()
		{
			if (base.equippedDuck == null)
			{
				base.graphic = this._pickupSprite;
				this.center = new Vec2(6f, 5f);
			}
			else
			{
				if (this.sprite.frame <= 5)
				{
					this._equippedDepth = 12;
				}
				else
				{
					this._equippedDepth = -1;
				}
				base.graphic = this.sprite;
				this.center = new Vec2(13f, 13f);
				base.wearOffset = new Vec2(0f, 0f);
			}
			if (this.equippedDuck != null)
			{
				Level.Add(new Joyed(duck));
			}
			base.Update();
		}
	}
}