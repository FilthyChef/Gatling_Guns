using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.GatlingGuns
{
    [EditorGroup("Equipment|Gatling Guns")]
    class DarkLevelsLightBulb : Thing
    {
        public EditorProperty<float> power = new EditorProperty<float>(8,null,2,100,1);
        static DarkLevelsLightBulb(){
            DarkLevels.AddSource(typeof(DarkLevelsLightBulb),(thing)=>(thing as DarkLevelsLightBulb).power*2);
        }

        public DarkLevelsLightBulb()
        {
            _editorName = "Light Source";
			this.editorTooltip = "Customizable Lighting Source";
            collisionSize = new Vec2(16, 16);
            center = collisionSize / 2;
            collisionOffset = -center;
            graphic = new Sprite(GetPath("Effects/darkLevelsLightBulb"));
        }

        public override void Initialize()
        {
            if (!(Level.current is Editor))
            {
                visible = false;
                collisionSize = Vec2.Zero;
            }
            base.Initialize();
        }
    }
}
