using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using SharpDX.MediaFoundation;

namespace Abyss
{
    public static class Arts
    {
        public static Texture2D Player;
        public static Texture2D Grass;

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Player");
            Grass = content.Load<Texture2D>("grass");
        }
    }
}
