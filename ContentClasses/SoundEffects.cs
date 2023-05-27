using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class SoundEffects
    {
        //private static string MapGenerator = "MapGenerator";
        public static SoundEffect ButtonOnActive;
        public static SoundEffect ButtonOnClick;
        public static void Load(ContentManager content)
        {
            ButtonOnActive = content.Load<SoundEffect>("ButtonOnActive");
            ButtonOnClick = content.Load<SoundEffect>("ButtonOnClick");
        }
    }
}
