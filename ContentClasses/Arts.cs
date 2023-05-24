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
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;

namespace Abyss.ContentClasses
{
    public static class Arts
    {
        public static Texture2D Player;
        public static Texture2D Grass;
        public static Texture2D Bullet;
        public static Texture2D Reider;
        public static Texture2D Money;
        public static Texture2D Heart;
        public static Texture2D Trophy;
        public static Texture2D Health;
        public static Texture2D HudBackground;

        public static SpriteFont Font;

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("player");
            Grass = content.Load<Texture2D>("grass");
            Bullet = content.Load<Texture2D>("Bullet");
            Reider = content.Load<Texture2D>("zombie");
            Money = content.Load<Texture2D>("Money");
            Heart = content.Load<Texture2D>("Heart");
            Trophy = content.Load<Texture2D>("Trophy");
            Health = content.Load<Texture2D>("Health");
            Font = content.Load<SpriteFont>("font");
            HudBackground = content.Load<Texture2D>("Background");
        }
    }
}
