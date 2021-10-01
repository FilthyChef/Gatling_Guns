using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace DuckGame.GatlingGuns
{
    class skinUpdater : IAutoUpdate
    {
        List<Duck> retexturedDucks = new List<Duck>();
        void resetTextures(Duck p)
        {
            var pers = new DuckPersona(p.persona.color);
            p.persona.armSprite = pers.armSprite;
            p.persona.sprite = pers.sprite;
            p.persona.quackSprite = pers.quackSprite;
            p.persona.controlledSprite = pers.controlledSprite;
            p.persona.featherSprite = pers.featherSprite;
            p.scale = new Vec2(1);
            p.InitProfile();
        }
        public void Update()
        {
            if(Level.current is RockScoreboard)
            {
                foreach(var t in Level.current.things[typeof(Duck)])
                {
                    var duck = t as Duck;
                    var hat = duck.GetEquipment(typeof(TeamHat)) as TeamHat;
                    if (hat != null)
                        if (GatlingGuns.skinHats.ContainsKey(hat.team.name))
                        {
                            duck.Unequip(hat);
                            Level.Remove(hat);
                        }
                }
            }
            if (!(Level.current is Editor))
            foreach (var t in Level.current.things[typeof(TeamHat)])
            {
                    try {
                        var hat = t as TeamHat;
                        if (GatlingGuns.skinHats.ContainsKey(hat.team.name))
                        {
                            var duck = hat.equippedDuck;

                            if ((Level.current is GameLevel) || (Level.current is TeamSelect2) || (Level.current is Arcade))
                                if (duck != null)
                                {
                                    if (retexturedDucks.Contains(duck))
                                    {
                                        retexturedDucks.Remove(duck);
                                        resetTextures(duck);
                                    }
                                        retexturedDucks.Add(duck);
                                    GatlingGuns.skinHats[hat.team.name].Equip(duck);
                                }
                            hat.UnEquip();
                            Level.Remove(hat);

                        }
                    }
                    catch { }
             }
            

            foreach(var duck in retexturedDucks.ToArray())
            {
                try {
                    if (!GatlingGuns.skinHats.ContainsKey(duck.team.name))
                    {
                        retexturedDucks.Remove(duck);
                        resetTextures(duck);
                    }
                    else
                    {
                        var hat = duck.GetEquipment(typeof(TeamHat));
                        if (hat != null)
                        {
                            duck._equipment.Remove(hat);
                            Level.Remove(hat);
                            GatlingGuns.skinHats[duck.team.name].Equip(duck);
                        }
                    }
                }
                catch { }
            }

        }
    }
}
