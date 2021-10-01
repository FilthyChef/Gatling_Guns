using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
    public abstract class GrenadeMain : Gun
    {
        public StateBinding TimerBinding {
            get;
            set;
        }
        public StateBinding HasPinBinding
        {
            get;
            set;
        }
        public bool HasPin
        {
            get
            {
                return hasPin;
            }
            set
            {
                if(HasPin && !value)
                {
                    CreatePinParticle();
                }
			
                hasPin = value;
            }
        }
        public float Timer
        {
            get;
            set;
        }
        public bool HasExploded {
            get;
            protected set;
        }
        public bool pullOnImpact
        {
            get;
            set;
        }
        protected SpriteMap sprite;
        bool hasPin;

        public GrenadeMain(float xval, float yval) : base(xval, yval){
            TimerBinding = new StateBinding("Timer", -1, false);
            HasPinBinding = new StateBinding("HasPin", -1, false);
            HasPin = true;
            Timer = 1.2f;
            _editorName = "GrenadeMain";
            _bio = "";
            _type = "gun";
            bouncy = 0.4f;
            friction = 0.05f;
            ammo = 1;
        }
        public override void Initialize(){
            base.Initialize();
        }
        public virtual void QuickFlash(float flashLength = 0.9f, float darkenLength = 0.3f){
            Graphics.flashAdd = flashLength;
            Layer.Game.darken = darkenLength;
        }
        public virtual void Explode()
        {
            HasExploded = true;
        }
        public virtual void DestroyWindowsInRadius(float radius)
        {
            foreach(Window window in Level.CheckCircleAll<Window>(position, radius))
            {
                if(Level.CheckLine<Block>(position, window.position, window) == null)
                {
                    window.Destroy(new DTImpact(this));
                }
            }
        }
        protected virtual void CreatePinParticle()
        {
            GrenadePin grenadePin = new GrenadePin(x, y);
            grenadePin.hSpeed = -offDir * Rando.Float(1.5f, 2f);
            grenadePin.vSpeed = -2f;
            Level.Add(grenadePin);

            SFX.Play("pullPin", 1f, 0.0f, 0.0f, false);
        }

        protected virtual void UpdateTimer()
        {
            if(!HasPin)
            {
                if(Timer > 0)
                {
                    Timer -= 0.01f;
                }
                else
                {
                    if(!HasExploded)
                    {
                        Explode();
                    }
                }
            }
        }

        public override void Update()
        {
            base.Update();
            
            UpdateTimer();
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if(pullOnImpact)
            {
                OnPressAction();
            }

            base.OnSolidImpact(with, from);
        }

        public override void OnPressAction(){
            if(HasPin){
                HasPin = false;
            }
        }
    }
}
