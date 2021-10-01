using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.GatlingGuns
{
public class FakeDuck: MaterialThing{
    private float life = 1f;

    private new SpriteMap _sprite;

    public FakeDuck(float xpos, float ypos, bool grounded, sbyte offDir) : base(xpos, ypos)
    {
      this._sprite = new SpriteMap("woodDuck", 32, 32, false);
      base.graphic = this._sprite;
      if (grounded)
      {
        this._sprite.frame = 0;
        this._collisionOffset = new Vec2(-6f, -24f);
        this.collisionSize = new Vec2(12f, 24f);
        base.hugWalls = WallHug.Floor;
      }
      else
      {
        this._sprite.frame = 4;
        this._collisionOffset = new Vec2(-8f, -24f);
        this.collisionSize = new Vec2(16f, 24f);
        base.hugWalls = (WallHug.Left | WallHug.Right);
      }
      if (offDir < 0)
      {
        this._sprite.flipH = true;
      }

      
      this.center = new Vec2(16f, 22f);
      base.depth = 0.5f;
      this.thickness = .5f;

      this.physicsMaterial = PhysicsMaterial.Wood;
      this._canHaveChance = false;
    }

    public override void Update()
    {
      life -= .013f;
      if (life <= 0f)
      {
        Level.Remove(this);
        for (int i = 0; i < 8; i++)
        {
          SmallSmoke musketSmoke = SmallSmoke.New(base.x + Rando.Float(4f), base.y - Rando.Float(4f));
          musketSmoke.depth = 0.9f + (float)i * 0.001f;
          Level.Add(musketSmoke);
        }
      }
    }
  }

[EditorGroup("Equipment|Gatling Guns|Melee")]
  public class Katana : Sword{
    protected SpriteMap sprite;
	
	/*	Causes build failures somehow.
	_editorName = "Katana";
	editorTooltip = "The preferred weapon of a ninja. Blocking gunshots will teleport you behind the assailant, allowing for an easy kill!";
	*/
	public Katana(float xval, float yval) : base(xval, yval)
    {
      sprite =  new SpriteMap(GetPath("Items/Melee/katana"), 8, 23, true);
      base.graphic = (Sprite)this.sprite;
    }
    public override bool Hit(Bullet bullet, Vec2 hitPos)
    {
      if (base.duck != null)
      {
        base.duck.AddCoolness(1);
        if (bullet.owner != null)
        {
          SFX.Play("death", 1f, 0f, 0f, false);
          for (int i = 0; i < 8; i++)
          {
            SmallSmoke musketSmoke = SmallSmoke.New(base.duck.x + Rando.Float(4f), base.duck.y - Rando.Float(4f));
            musketSmoke.depth = 0.9f + (float)i * 0.001f;
            Level.Add(musketSmoke);
          }
          Level.Add(new FakeDuck(base.duck.x, base.duck.y + 6f, base.duck.grounded, base.offDir));
          Duck assailant = bullet.owner as Duck;
          this.owner.x = assailant.x - (14 * assailant.offDir);
          base.duck.offDir = assailant.offDir;
          for (int i = 0; i < 8; i++)
          {
            SmallSmoke musketSmoke = SmallSmoke.New(base.duck.x + Rando.Float(4f), base.duck.y - Rando.Float(4f));
            musketSmoke.depth = 0.9f + (float)i * 0.001f;
            Level.Add(musketSmoke);
          }
        }
        else
        {
          SFX.Play("ting", 1f, 0f, 0f, false);
        }
        return base.Hit(bullet, hitPos);
      }
      return false;
    }
    public override void OnPressAction()
    {
      if ((this._crouchStance && this._jabStance && !this._swinging) || (!this._crouchStance && !this._swinging && this._swing < 0.1f))
      {
        this._pullBack = true;
        this._swung = true;
        this._shing = false;
        SFX.Play("swipe", Rando.Float(0.8f, 1f), Rando.Float(-0.1f, 0.1f), 0f, false);
      }
    }
  }
}