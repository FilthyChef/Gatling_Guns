using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DuckGame;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns|Equipment")]
    [BaggedProperty("isSuperWeapon", true)]
    class EscapistShoes : Boots{
        private Boolean inAir;
        private AmmoType _ammoType;
        private float cd;

        public EscapistShoes(float xpos, float ypos) : base(xpos, ypos)
        {
            this._editorName = "Escapist's Shoes";
			this.editorTooltip = "Those prison shoes are ID-traceable by the Warden-Ninja-Sniper. You won't see where the shot came from.";
			this._hasEquippedCollision = false;
            this._equippedCollisionOffset = new Vec2(0, 0);
            this._equippedCollisionSize = new Vec2(0, 0);
            this._pickupSprite = new Sprite(GetPath("Items/Equipment/escapistShoes"), 0.0f, 0.0f);
            this._sprite = new SpriteMap(GetPath("Items/Equipment/escapistShoesMap"), 32, 32, false);
            this.graphic = this._pickupSprite;
            this.inAir = true;
            this._ammoType = new ATSniper();
			this._ammoType.penetration = 32f;
            this.cd = 0;
        }

        public override void Update()
        {
            base.Update();

            if (cd > 0f)
            {
                cd -= .001f;
                return;
            }

            if (this.owner == null) return;

            Duck duck = this.owner as Duck;

            if (duck.holdObject == this) return;

            if (!inAir && !duck.grounded)
            {
                inAir = true;

                if (duck.vSpeed < 0) {
                    Boolean left = true;

                    if (duck.offDir > 0)
                        left = false;

                    this._ammoType.FireBullet(new Vec2(x + (left ? 500 : -500), y + this.owner.height / 2),
                        this.owner, left ? 180 : 0, (Thing)this);
                    SFX.Play("sniper", 1f, Rando.Float(0.2f) - 0.1f, 0.0f, false);
                    cd = .15f;
                }
            }

            if (inAir && duck.grounded)
            {
                inAir = false;
            }
        }
    }

}
