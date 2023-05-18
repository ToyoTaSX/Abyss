﻿using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    static class CreatedMaps
    {
        //Строка 40x23
        public static string mapString1 = "   G      \n   G      \n   G      \n   G      \nGGGG GGGGG\nGGGG GGGGG\nGGGG GGGGG\n          \nGGGG GGGGG\nGGGG GGGGG";
        public static Map Map1 { get => MapGenerator.CreateMapFromString(mapString1); }

        public static Map RandomMap 
        {
            get
            {
                var rnd = new Random();
                var symbols = new[] { ' ', 'G' };
                var str = "";
                for (int y = 0; y <= 23; y++)
                {
                    for (int x = 0; x <= 40; x++)
                    {
                        var i = rnd.Next(symbols.Length);
                        str += symbols[i];
                    }
                    str += '\n';
                }
                str = str.Substring(0, str.Length - 1);
                return MapGenerator.CreateMapFromString(str);
            }
        }
    }
}
