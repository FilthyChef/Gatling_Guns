using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
    public class MustardGasOnDuck : Thing
    {
        public static Dictionary<Duck, bool> ToxicDucks = new Dictionary<Duck, bool>();
        public float Timer
        {
            get;
            set;
        }
        Duck duck;
        public MustardGasOnDuck(Duck duck, float stayTime = 2f) : base(0f, 0f)
        {
            this.duck = duck;
            Timer = stayTime;
            ToxicDucks[duck] = true;
        }

        public override void Update()
        {
            base.Update();

            if(duck.dead)
            {
                Level.Remove(this);
            }
			foreach (Equipment e in duck._equipment){
				if (e.editorName == "Gas Mask"){
					Level.Remove(this);
				}
				if (e.editorName == "Tactical Mask"){
					Level.Remove(this);
				}
			}
			/*
			if(duck.HasEquipment(typeof(TacticalMask)))
			if(duck.HasEquipment(typeof(TacticalMask)) || duck.HasEquipment(typeof(GCNItemsMod.GasMask)))
			{
				Level.Remove(this);
			}
			*/
					if(Timer > 0)
					{
						Timer -= 0.01f;
					}
					else
					{
						KillDuck();
					}
					duck.velocity = new Vec2(duck.velocity.x * 0.5f, duck.velocity.y < 0 ? duck.velocity.y * 0.8f : duck.velocity.y);
        }

        public virtual void KillDuck()
        {
            if(!duck.dead)
            {
                duck.Scream();
                duck.Kill(new MustardGas(this));
            }
        }
		
		public class MustardGas : DestroyType
		{
        public MustardGas(Thing toxic) : base(toxic)
			{
			}
		}
    }
}
