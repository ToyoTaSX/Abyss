using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class MapImages
    {
        private static string MapGenerator = "MapGenerator";
        public static Texture2D Map1;
        public static void Load(ContentManager content)
        {
            Map1 = content.Load<Texture2D>(Path.Combine(MapGenerator, "GrassMap"));
        }
    }
}
