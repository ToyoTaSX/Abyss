using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class Audios
    {
        //private static string MapGenerator = "MapGenerator";
        public static Song TradeMenu;
        public static Song MainMenu;
        public static Song MainTheme;
        public static void Load(ContentManager content)
        {
            TradeMenu = content.Load<Song>("TradeMenu");
            MainMenu = content.Load<Song>("MainMenu");
            MainTheme = content.Load<Song>("MainTheme");
        }
    }
}
