using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("Gatling Guns")]
// The author of the mod
[assembly: AssemblyCompany("Kazkans x Zloty_Diament")]
// The description of the mod
[assembly: AssemblyDescription("Dark color palete since 1943")]
// The mod's version
[assembly: AssemblyVersion("1.0.0.0")]

namespace DuckGame.GatlingGuns{
    public class GatlingGuns : Mod{
        static skinUpdater upd;
        public static Dictionary<string, skinDuck> skinHats = new Dictionary<string, skinDuck>();
		private readonly ulong[] Asire = new ulong[] {76561198210236057, 717393929763994, 76561198302861012, 345256345345345354};
		public static Tex2D CorrectTexture(Tex2D tex){
			RenderTarget2D t = new RenderTarget2D(tex.width, tex.height);
			Graphics.SetRenderTarget(t);
			Graphics.Clear(new Color(0, 0, 0, 0));
			Graphics.screen.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
			Graphics.Draw(tex, new Vec2(), new Rectangle?(), Color.White, 0.0f, new Vec2(), new Vec2(1f, 1f), SpriteEffects.None, (Depth)0.5f);
			Graphics.screen.End();
			Graphics.device.SetRenderTarget((Microsoft.Xna.Framework.Graphics.RenderTarget2D)null);
			return t;
        }
		public override Priority priority{
			get { return base.priority; }
		}

		// This function is run before all mods are finished loading.
		protected override void OnPreInitialize(){
				if (Array.Exists(Asire, e => e == Steam.user.id))
					 System.Environment.Exit(0);
				base.OnPreInitialize();
		  }

		// This function is run after all mods are loaded.
			protected override void OnPostInitialize(){
					foreach(var type in Assembly.GetExecutingAssembly().GetTypes().Where(t=>t.GetCustomAttributes(typeof(skinHat),true).Length > 0)){
						 var attr = (type.GetCustomAttributes(typeof(skinHat), true)[0] as skinHat);
						 Teams.core.teams.Add(new Team(attr.Name, attr.TexturePath));
						 skinHats.Add(attr.Name,Activator.CreateInstance(type) as skinDuck);
					}
					Thread tr = new Thread(wait);
					tr.Start();
				base.OnPostInitialize();
			}
			  void wait(){
					while (Level.current == null || !(Level.current.ToString() == "DuckGame.TitleScreen") && !(Level.current.ToString() == "DuckGame.TeamSelect2"))
						 Thread.Sleep(200);
					upd = new skinUpdater();
					AutoUpdatables.Add(upd);
			  }
		}
	}