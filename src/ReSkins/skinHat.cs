using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.GatlingGuns
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    class skinHat : Attribute
    {
        public string Name;
        public string TexturePath;
        public skinHat(string name,string hatTexture)
        {
            Name = name;
            TexturePath = Mod.GetPath<GatlingGuns>(hatTexture);
        }
    }
}
