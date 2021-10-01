using System;
using DuckGame;
namespace DuckGame.GatlingGuns
{
	public class ATMilitary : AmmoType
	{
		public ATMilitary()
		{
			this.accuracy = 0.85f;
			this.range = 250f;
			this.penetration = 1f;
			this.combustable = true;
			this.bulletSpeed = 48f;
		}

		public override void PopShell(float x, float y, int dir)
		{
			new MilitaryShell(x, y);
			{
				MilitaryShell shell = new MilitaryShell(x, y);
				shell.hSpeed = (float)((float)dir * (1.5 + Rando.Float(1f)));
				Level.Add(shell);
			}
		}
	}
}
