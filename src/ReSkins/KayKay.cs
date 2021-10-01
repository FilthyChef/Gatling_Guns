namespace DuckGame.GatlingGuns{
	[skinHat("KayKay","ReSkins/KayKay/hatSelector")]
	class kaykay : skinDuck {
		public kaykay(){}
		public override void loadTextures(){
			ReplaceSprite = sprite("ReSkins/KayKay/duck");ReplaceQuack = sprite("ReSkins/KayKay/quackDuck");ReplaceArm = armsprite("ReSkins/KayKay/duckArms");ReplaceControlled = sprite("ReSkins/KayKay/controlledDuck");ReplaceFeather = feathersprite("ReSkins/KayKay/featherTexture");
		}
		public override void ApplyReplaceAnimation(DuckPersona persona){
			ReplaceSprite.ClearAnimations();
			ReplaceSprite.AddAnimation("idle", 1f, true, 0/*, 23, 24, 25, 26*/);
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
