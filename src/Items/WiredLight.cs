using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns")]
    class WiredLight : Block,IWirePeripheral
    {
        public SpriteMap _sprite;
        public EditorProperty<bool> on = new EditorProperty<bool>(true);
        public EditorProperty<float> power = new EditorProperty<float>(8,null,1,100,1);
        static WiredLight()
        {
            DarkLevels.AddSource(typeof(WiredLight), (thing) => (thing as WiredLight).on ? (thing as WiredLight).power*2 : 0);
        }

        public WiredLight(float x,float y) : base(x,y)
        {
            _editorName = "Wired Light Source";
			this.editorTooltip = "Customizable Light Source that reacts to wire signal";
			_sprite = new SpriteMap(GetPath("Effects/wiredLight"),16,16);
            layer = Layer.Foreground;
            depth = -0.5f;
            graphic = _sprite;
            collisionSize = new Vec2(16);
            center = collisionSize / 2;
            collisionOffset = -center;
        }

        public void Pulse(int type,WireTileset tileset)
        {
            on = !on;
        }

        public override void Draw()
        {
            _sprite.frame = on ? 0 : 1;
            base.Draw();
        }
    }
}
