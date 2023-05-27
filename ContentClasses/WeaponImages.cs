using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class WeaponImages
    {
        private static string Weapons = "Weapons";
        public static Texture2D DP28;
        public static Texture2D Colt;
        public static Texture2D BAR;
        public static Texture2D Gewehr43;
        public static Texture2D Kar98K;
        public static Texture2D Mosin;
        public static Texture2D MP40;
        public static Texture2D PPSH;
        public static Texture2D Revolver;
        public static Texture2D Stg44;
        public static Texture2D Thompson;
        public static Texture2D LMG99;

        public static void Load(ContentManager content)
        {
            DP28 = content.Load<Texture2D>(Path.Combine(Weapons, "DP28"));
            Colt = content.Load<Texture2D>(Path.Combine(Weapons, "Colt"));
            BAR = content.Load<Texture2D>(Path.Combine(Weapons, "BAR"));
            Gewehr43 = content.Load<Texture2D>(Path.Combine(Weapons, "Gewehr43"));
            Kar98K = content.Load<Texture2D>(Path.Combine(Weapons, "Kar98K"));
            Mosin = content.Load<Texture2D>(Path.Combine(Weapons, "Mosin"));
            MP40 = content.Load<Texture2D>(Path.Combine(Weapons, "MP40"));
            PPSH = content.Load<Texture2D>(Path.Combine(Weapons, "PPSH"));
            Revolver = content.Load<Texture2D>(Path.Combine(Weapons, "Revolver"));
            Stg44 = content.Load<Texture2D>(Path.Combine(Weapons, "Stg44"));
            Thompson = content.Load<Texture2D>(Path.Combine(Weapons, "Thompson"));
            LMG99 = content.Load<Texture2D>(Path.Combine(Weapons, "LMG99"));
        }
    }
}
