using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Abyss.ContentClasses
{
    public static class MapImages
    {
        private static string MapGenerator = "MapGenerator";
        public static Texture2D Map1;
        public static Texture2D Map2;
        public static Texture2D Map3;
        public static Texture2D Map4;
        public static Texture2D Map5;
        public static void Load(ContentManager content)
        {
            Map1 = content.Load<Texture2D>(Path.Combine(MapGenerator, "Map1"));
            Map2 = content.Load<Texture2D>(Path.Combine(MapGenerator, "Map2"));
            Map3 = content.Load<Texture2D>(Path.Combine(MapGenerator, "Map3"));
            Map4 = content.Load<Texture2D>(Path.Combine(MapGenerator, "Map4"));
            Map5 = content.Load<Texture2D>(Path.Combine(MapGenerator, "Map5"));
        }
    }
}
