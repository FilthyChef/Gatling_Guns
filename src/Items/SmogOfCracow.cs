using System;

namespace DuckGame.GatlingGuns
{
    [BaggedProperty("isSuperWeapon", true), EditorGroup("Equipment|Gatling Guns")]
    public class SmogOfCracow : Gun{
        public StateBinding fok = new StateBinding("f");
        double f=0;
        bool first = true;
        bool second = true;
        public EditorProperty<float> Speed;
        public EditorProperty<int> Time;
        double stop;
        double start;
        Vec2 startPos;

        public SmogOfCracow(float xval, float yval) : base(xval, yval){
			this.editorTooltip = "A wall of deadly exhaust fume speeding in selected direction, used to create Battle Royale scenarios.";
            this.canPickUp = false;
            this.ammo = 1;
            Speed = new EditorProperty<float>(1f, this, 0.1f, 3f, 0.1f);
            Time = new EditorProperty<int>(1, this, 0f, 600f, 5f);
            this.canPickUp = false;
            this._ammoType = new AT9mm();
            this._ammoType.range = 200f;
            this._ammoType.accuracy = 0.85f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("smogOfCracow.png"), 0f, 0f);
            this.center = new Vec2(16f, 15f);
            this.collisionOffset = new Vec2(-8f, -3f);
            this.collisionSize = new Vec2(18f, 10f);
            this._barrelOffsetTL = new Vec2(32f, 14f);
        }
        public override void Initialize()
        {
            startPos = this.position;
            base.Initialize();
        }
        public override void Update()
        {
            this.ammo = 3;
            this.position = startPos;
            if (!(Level.current is Editor))
            {
                this.visible = false;
            }
            if (first)
            {
                stop = Environment.TickCount & Int32.MaxValue;
                first = false;
            }
            start = Environment.TickCount & Int32.MaxValue;
            f = start - stop;
            if (f/1000 >= Time && second)
            {
                second = false;
                Level.Add(new WallOfSmog(this.position.x + barrelOffset.x, this.position.y + barrelOffset.y - 250, offDir, Speed));
                SFX.Play(GetPath("smogOfCracow.wav"), 1f, 0.0f, 0.0f, false);
            }
            base.Update();
        }
    }
}
