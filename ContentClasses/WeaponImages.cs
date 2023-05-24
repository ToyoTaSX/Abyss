using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class WeaponImages
    {
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
            DP28 = content.Load<Texture2D>("DP28");
            Colt = content.Load<Texture2D>("Colt");
            BAR = content.Load<Texture2D>("BAR");
            Gewehr43 = content.Load<Texture2D>("Gewehr43");
            Kar98K = content.Load<Texture2D>("Kar98K");
            Mosin = content.Load<Texture2D>("Mosin");
            MP40 = content.Load<Texture2D>("MP40");
            PPSH = content.Load<Texture2D>("PPSH");
            Revolver = content.Load<Texture2D>("Revolver");
            Stg44 = content.Load<Texture2D>("Stg44");
            Thompson = content.Load<Texture2D>("Thompson");
            LMG99 = content.Load<Texture2D>("LMG99");
        }
    }
}
