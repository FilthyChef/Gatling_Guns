using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace DuckGame.GatlingGuns
{
    public class Joyed : Thing
    {
        public static Dictionary<Duck, bool> ToxicDucks = new Dictionary<Duck, bool>();
        public float Timer
        {
            get;
            set;
        }
        Duck duck;

        public Joyed(Duck duck, float stayTime = 4f) : base(0f, 0f)
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
            else
            {

                if(Timer > 0)
                {
                    Timer -= 0.01f;
                }
				}
			}
		}
}
