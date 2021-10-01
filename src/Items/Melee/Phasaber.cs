using System;
namespace DuckGame.GatlingGuns
{
	[BaggedProperty("isInDemo", true)]
    [EditorGroup("Equipment|Gatling Guns|Melee")]
    public class Phasaber : Sword{
        float angela;
        Vec2 plant;
        bool str = false;
        bool press = false;
        bool boo = false;
        bool spin = false;
        bool first = true;
        public byte blocked;
        bool op = false;
        float pAngl = 0;
        bool off = true;
        SpriteMap sprut;
        Sprite ss;
        protected SpriteMap sprite;
        Vec2 sucho;
        public Phasaber(float xval, float yval) : base(xval, yval){
            this._editorName = "Phasaber";
			this.editorTooltip = "Phaser refolding 6000 times per milisecond. Hold STRAFE to parry bullets.";
			sprite = new SpriteMap(GetPath("Items/Melee/phasaberUpSide"), 8, 23, true);
            this.ammoType.range = 16;
            this.ammoType.penetration = 6f;
            ss = new Sprite(GetPath("Items/Melee/phasaberHandle"));
            this.collisionSize = new Vec2(8, 7);
            this.ammoType.bulletThickness = 0F;
            this.depth = 0.01f;
            
            this.sprite.AddAnimation("anim1", 0.1f, true, new int[]
            {
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                12,
                11,
                10,
                9,
                8
            });
            this.sprite.AddAnimation("anim2", 0.1f, true, new int[]
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                5,
                4,
                3,
                2,
                1
            });
            this.sprite.AddAnimation("anim3", 1, true, new int[] { 14 });
            base.graphic = (Sprite)this.sprite;
            // this.sprite.SetAnimation("anim2");
            this.sprite.SetAnimation("anim3");
            this.graphic = ss;
            this.center = new Vec2(4f, 4);
            this.collisionSize = new Vec2(8, 7);
            this.collisionOffset = new Vec2(-4, -4);
        }
       
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (base.duck != null)
            {


                op = !op;
                
                if (this.blocked == 0)
                {
                    base.duck.AddCoolness(1);
                  
                }
                else
                {
                    this.blocked += 1;
                    if (this.blocked > 4)
                    {
                        this.blocked = 1;
                        base.duck.AddCoolness(1);
                    }
                }
                SFX.Play(GetPath("Items/Melee/phasaberHit.wav"), 1f, 0f, 0f, false);
                return base.Hit(bullet, hitPos);
            }
            return false;
        }
        public override void Update()
        {
            if(duck == null)
            {
               Off();
            }
            if (duck != null)
            {
                angela = base.angleDegrees;
                if (this.offDir < 0)
                {
                    angela += 180f;
                    angela -= this._ammoType.barrelAngleDegrees;
                    angela += 90f;
                }
                else
                {
                    angela += this._ammoType.barrelAngleDegrees;
                    angela -= 90f;

                }
                if (this.duck.inputProfile.Down(Triggers.Strafe))
                {
                    this._crouchStance = true;
                    //  Level.Add(new Crate(base.x,this.position.y));
                    //this.handOffset = new Vec2(10, 10);
                }
                sucho.x = barrelOffset.x;
                sucho.y = barrelOffset.y + 16;
                Vec2 pos = this.Offset(this.sucho);
                Bullet bullet = this._ammoType.FireBullet(this.Offset(this.sucho), this.owner, angela, this);
                bullet.firedFrom = this;
                this.firedBullets.Add(bullet);
                Level.Add(bullet);
                if (this.duck == null)
                {
                    boo = false;
                    this.Off();
                }
                if (this.duck != null && !boo)
                {
                    boo = true;
                    this.On();
                }
                if (this.duck != null)
                {

                }
                else
                {
                    op = false;
                }
            }
            if (!str)
            {
                base.Update();
            }
            str = false;

            if (this.duck != null)
                {

                    if (this.duck.inputProfile.Down(Triggers.Strafe) && !spin && !press && !this._pullBack &&  !this._swinging && !this._swingPress && !this._swung)
                    {
                    str = true;
                        this._pullBack = false;
                        this.duck.CancelFlapping();
                        this._pullBack = false;
                        this.duck.hSpeed *= 0.6f;
                        this.duck.vSpeed *= 1;
                        this._hold = 0f;
                        //    this.handAngle = 90f;
                        //    this._holdOffset = new Vec2(0f + this._addOffsetX, 4f + this._addOffsetY);
                        if (!op)
                        {
                            sprite.SetAnimation("anim1");

                            if (this.offDir == 1)
                                this.handAngle = -0.5f;
                            else
                                this.handAngle = 0.5f;
                            this.graphic = sprite;


                            this._holdOffset = new Vec2(0f + this._addOffsetX, 15f + this._addOffsetY);
                            this.handOffset = new Vec2(3f + this._addOffsetX, -5 + this._addOffsetY);
                        }
                        else
                        {
                        // sprite.flipV = false;
                            sprite.SetAnimation("anim2");
                            this.graphic = sprite;
                            if (this.offDir == 1)
                                this.handAngle = 0.5f;
                            else
                                this.handAngle = -0.5f;
                            this._holdOffset = new Vec2(0f + this._addOffsetX, 4f + this._addOffsetY);
                            this.handOffset = new Vec2(3f + this._addOffsetX, this._addOffsetY);
                        }
                    //   this.handOffset = new Vec2(3f + this._addOffsetX, this._addOffsetY);
                    //    sprite.frame = 1;
                    //    this.graphic = sprite;
                    /* if(!op)
                     {
                         this._hold = 0f;
                         this._holdOffset = new Vec2(0f + this._addOffsetX, 4f + this._addOffsetY);
                         this.handOffset = new Vec2(3f + this._addOffsetX, this._addOffsetY);
                         if (offDir == 1)
                         {
                             this._hold = 1f; 
                         }
                        else
                         {
                             this._hold = 1f;

                         }
                     }
                     */
                    //  Level.Add(new Crate(base.x,this.position.y));
                    //this.handOffset = new Vec2(10, 10);
                    this._hold = 0f;

                }
                else
                    {
                        //sprite.flipV = false;
                        sprite.SetAnimation("anim2");

                        this.graphic = sprite;

                        this.handAngle = 0;
                    }
                    if (this.duck.inputProfile.Down(Triggers.Quack))
                    {
                        pAngl += 0.22f;
                        spin = true;
                        this._hold = pAngl;
                    }
                    else
                    {
                        pAngl = 0;
                        spin = false;
                    }
                }


        }
        public override void Thrown()
        {
            str = false;
            base.Thrown();
        }
        public override void OnPressAction()
        {
            if (!this.duck.inputProfile.Down(Triggers.Strafe))
            {
                press = true;
                if ((this._crouchStance && this._jabStance && !this._swinging) || (!this._crouchStance && !this._swinging && this._swing < 0.1f))
                {
                    this._pullBack = true;
                    this._swung = true;
                    this._shing = false;
                    this._swinging = true;
                    SFX.Play(GetPath("Items/Melee/phasaberSwing"), Rando.Float(0.8f, 1f), Rando.Float(-0.1f, 0.1f), 0f, false);
                }
                else
                {
                    if (!this._crouchStance || this._jabStance || (this.duck == null || this.duck.grounded))
                        return;
                    this._slamStance = true;
                }
            }
        }

        public override void Fire()
        {
         
        }
        public override void OnReleaseAction()
        {
            press = false;
            base.OnReleaseAction();
        }
        public void Off()
        {
            Hold h = new Hold(this.position.x, this.position.y);
            h.vSpeed = this.vSpeed;
            h.hSpeed = this.hSpeed;
            Level.Add(h);
            Level.Remove(this);
            off = true;
            this.sprite.SetAnimation("anim3");
            this.graphic = sprite;
            /* this.center = new Vec2(4f, 20f);
             this.collisionSize = new Vec2(8, 7);
            this.collisionOffset = new Vec2(-4, 4);
            */
        }
        public void On()
        {
            off = false;
            this.sprite.SetAnimation("anim2");
            this.graphic = sprite;
            this.collisionSize = new Vec2(4, 23);
            this.center = new Vec2(4f, 21f);
            this.collisionOffset = new Vec2(-2f, -16f);
        }
    }
	public class PhasaberBeam : Gun{
        private float _charge;
        private int _chargeLevel;
        private float _chargeFade;
        private SinWave _chargeWaver = 0.4f;
        private SpriteMap _phaserCharge;
        public PhasaberBeam(float xval, float yval) : base(xval, yval){
            this.ammo = 99;
            this._ammoType = new ATPhaser();
            this._ammoType.range = 40;
            this._type = "gun";
            this.loseAccuracy = 0f;
            this.ammoType.accuracy = 0;
            this.graphic = new Sprite("phaser", 0f, 0f);
            this.center = new Vec2(7f, 4f);
            this.collisionOffset = new Vec2(-7f, -4f);
            this.collisionSize = new Vec2(15f, 9f);
            this._barrelOffsetTL = new Vec2(14f, 3f);
            this._fireSound = null;
            this._fullAuto = false;
            this._fireWait = 0f;
            this._kickForce = 0f;
            this._holdOffset = new Vec2(0f, 0f);
            this._flare = new SpriteMap("laserFlare", 16, 16, false);
            this._flare.center = new Vec2(0f, 8f);
            this._phaserCharge = new SpriteMap("phaserCharge", 8, 8, false);
            this._phaserCharge.frame = 1;
        }
        public override void Update(){
            this.ammo = 99;
            if(offDir == 1){
                this.handAngle = 55;
            }
            if(offDir == -1){
                this.handAngle = -55;
            }
            if(duck != null)
            base.Fire();
            base.Update();
        }
        public override void Draw(){
            base.Draw();
            if (this._chargeFade > 0.01f){
                float a = base.alpha;
                base.alpha = (this._chargeFade * 0.6f + this._chargeFade * this._chargeWaver.normalized * 0.4f) * 0.8f;
                base.Draw(this._phaserCharge, new Vec2(3f + this._chargeFade * this._chargeWaver * 0.5f, -4f), -1);
                base.alpha = a;
            }
        }
        public override void OnReleaseAction(){
            if (this.ammo <= 0){return;}
            if (this.owner != null){
                this._ammoType.bulletThickness = 0.8f;
                this._ammoType.penetration = 2;
                this._ammoType.accuracy = 0;
                this.Fire();
                this._charge = 0f;
                this._chargeLevel = 0;
            }
            base.OnReleaseAction();
        }
    }
	public class Hold : Holdable{
        Phasaber sok;
        public Hold(float xval, float yval) : base(xval, yval){
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Items/Melee/phasaberHandle"), 0f, 0f);
            this.center = new Vec2(0, 0);
            this.weight = 0.001f;
            this.collisionOffset = new Vec2(-0, -0);
            this.collisionSize = new Vec2(8f, 7f);
        }
        public override void Update(){
            base.Update();
            if (duck != null){
                if (this.sok != null || Network.isActive && !this.isServerForObject)
                return;
                this.sok = new Phasaber(duck.position.x, duck.position.y);
                Level.Add((Thing)this.sok);
                duck.GiveHoldable((Holdable)this.sok);
                Level.Remove(this);
            }
        }
    }
}
