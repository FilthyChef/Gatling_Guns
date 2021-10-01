namespace DuckGame.GatlingGuns{
	[skinHat("Real Log","ReSkins/RealLog/hatSelector")]
	class real_log : skinDuck {
		public real_log(){}
		public override void loadTextures(){
			ReplaceSprite = sprite("ReSkins/RealLog/duck");ReplaceQuack = sprite("ReSkins/RealLog/quackDuck");ReplaceArm = armsprite("ReSkins/RealLog/duckArms");ReplaceControlled = sprite("ReSkins/RealLog/controlledDuck");ReplaceFeather = feathersprite("ReSkins/RealLog/featherTexture");
		}
		public override void ApplyReplaceAnimation(DuckPersona persona){
		ReplaceSprite.ClearAnimations();
		ReplaceSprite.AddAnimation("idle", 1f, true, 0);
		ReplaceSprite.AddAnimation("run", 1f, true, 1, 2, 3, 4, 5, 6);
		ReplaceSprite.AddAnimation("jump", 1f, true, 7, 8, 9);
		ReplaceSprite.AddAnimation("slide", 1f, true, 10);
		ReplaceSprite.AddAnimation("crouch", 1f, true, 11);
		ReplaceSprite.AddAnimation("groundSlide", 1f, true, 12);
		ReplaceSprite.AddAnimation("dead", 1f, true, 13);
		ReplaceSprite.AddAnimation("netted", 1f, true, 14);
		ReplaceSprite.AddAnimation("listening", 1f, true, 16);
		ReplaceSprite.SetAnimation("idle");
		}
	}
}
