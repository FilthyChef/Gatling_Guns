using System;

namespace DuckGame.GatlingGuns
{
    [BaggedProperty("isSuperWeapon", true), EditorGroup("Equipment|Gatling Guns|Melee")]
    public class SawedOffChainsaw : Gun
    {
        bool on = false;
        Duck ducky;
        SpriteMap spritee;
        public SawedOffChainsaw(float xval, float yval) : base(xval, yval)
        {
            this._editorName = "Sawed-Off Chainsaw";
			this.editorTooltip = "Throwable Chainsaw";
			this.ammo = 30;
            this._ammoType = new AT9mm();
            this._ammoType.range = 200f;
            this._ammoType.accuracy = 0.85f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            spritee = new SpriteMap(GetPath("Items/Melee/sawedOffChainsaw"), 21,10);
            this.graphic = spritee;
            this.spritee.AddAnimation("sawedOffChainsawAnimation", 0.5f, true, new int[] {0,1,2,3}); 
            this.center = new Vec2(10, 3);
            this.collisionOffset = new Vec2(-10f, -3f);
            this.collisionSize = new Vec2(21f, 10f);
            this._barrelOffsetTL = new Vec2(8, 5f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 1.2f;
            this.bouncy = 0.5f;
            this._kickForce = 3.5f;
            this.loseAccuracy = 0.2f;
            this.maxAccuracyLost = 0.8f;
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (with is Block)
            {
                this.Shing(with);
                
            }
            /*   if (on && duck == null)
               {
                   Level.Add(Spark.New(position.x, position.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f)), 0.02f));


                   this.hSpeed = Rando.Float(-5, 5);
                   this.vSpeed = Rando.Float(-4, 0);
               }
               */
        }
        public override void Touch(MaterialThing with)
        {
            if (on)
            {
                if (with is Duck)
                {
                    ducky = with as Duck;
                    ducky.Kill(new DTCrush(this));
                }
                if (with is RagdollPart)
                {
                    RagdollPart raggo = with as RagdollPart;
                    raggo._doll._duck.Kill(new DTCrush(this));
                }
            }
            base.Touch(with);
        }
        public void Shing(Thing wall)
        {
      
                    SFX.Play("chainsawClash", Rando.Float(0.4f, 0.55f), Rando.Float(-0.2f, 0.2f), Rando.Float(-0.1f, 0.1f), false);
             
                Vec2 vec = (this.position - base.barrelPosition).normalized;
                Vec2 start = base.barrelPosition;
                for (int i = 0; i < 6; i++)
                {
                    Level.Add(Spark.New(start.x, start.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f)), 0.02f));
                    start += vec * 4f;
                }
                if (Recorder.currentRecording != null)
                {
                    Recorder.currentRecording.LogAction(7);
                }
           
                    if (wall.bottom < this.top)
                    {
                        this.vSpeed += 2f;
                        return;
                    }
                
                    if (wall.x > this.x)
                    {
                        this.hSpeed -= 5f;
                    }
                    else
                    {
                        this.hSpeed += 5f;
                    }
                    this.vSpeed -= 2f;
                
            
        }
        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnImpact(with, from);
            if (on && duck == null && with is Block)
            {
                Shing(with);
                Level.Add(Spark.New(position.x, position.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f)), 0.02f));


               // this.hSpeed = Rando.Float(-5, 5);
               // this.vSpeed = Rando.Float(-5, 0);
            }
        }
        public override void Fire()
        {
            
        }
        public override void Update()
        {
            if(on)
            {
                if (offDir == 1)
                    this.handAngle = -0.2f;
                else
                    this.handAngle = 0.2f;
            }
            if (on && grounded && this.duck == null)
            {
                Level.Add(Spark.New(position.x, position.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f)), 0.02f));


               this.hSpeed = Rando.Float(-2, 2);
                this.vSpeed = Rando.Float(-3, 0);
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            base.OnPressAction();
            this.canPickUp = false;
            if(!on)
            {
                this.spritee.SetAnimation("sawedOffChainsawAnimation");
                this.graphic = this.spritee;
            }
            on = true;
        }
    }
}
