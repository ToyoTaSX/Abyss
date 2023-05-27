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
using System.Drawing;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;
using System.IO;

namespace Abyss.ContentClasses
{
    public static class Arts
    {
        //GUI
        private static string GUI = "GUI";

        public static Texture2D Money;
        public static Texture2D Heart;
        public static Texture2D Trophy;
        public static Texture2D Health;
        public static Texture2D HudBackground;

        public static Texture2D ButtonActive;
        public static Texture2D ButtonInactive;
        public static Texture2D ButtonClick;
        public static Texture2D ButtonDisabled;

        public static Texture2D WeaponBuyActive;
        public static Texture2D WeaponBuyInactive;

        public static Texture2D MainBackground;
        public static Texture2D LoadBackground;
        public static Texture2D SaveBacground;

        // MapTextures
        private static string MapTextures = "MapTextures";

        public static Texture2D TargetBackgroundGreen;
        public static Texture2D TargetBackgroundRed;
        public static Texture2D StartPosition;

        public static Texture2D Grass;
        public static Texture2D Water;
        public static Texture2D Sandstone;
        public static Texture2D Box;
        public static Texture2D Gravel;
        public static Texture2D Dirt;
        public static Texture2D Bricks;


        //Без папки
        public static SpriteFont Font;
        public static SpriteFont IngameFont;

        public static Texture2D Player;
        public static Texture2D Bullet;
        public static Texture2D Reider;

        public static void Load(ContentManager content)
        {
            //Entities
            Player = content.Load<Texture2D>("player");
            Bullet = content.Load<Texture2D>("Bullet");
            Reider = content.Load<Texture2D>("zombie");

            Font = content.Load<SpriteFont>("font");
            IngameFont = content.Load<SpriteFont>("IngameFont");

            //Objects
            Grass = content.Load<Texture2D>(Path.Combine(MapTextures,"Grass"));
            Water = content.Load<Texture2D>(Path.Combine(MapTextures, "Water"));
            Sandstone = content.Load<Texture2D>(Path.Combine(MapTextures, "Sandstone"));
            Box = content.Load<Texture2D>(Path.Combine(MapTextures, "Box"));
            Gravel = content.Load<Texture2D>(Path.Combine(MapTextures, "Gravel"));
            Dirt = content.Load<Texture2D>(Path.Combine(MapTextures, "Dirt"));
            Bricks = content.Load<Texture2D>(Path.Combine(MapTextures, "Bricks"));

            TargetBackgroundGreen = content.Load<Texture2D>(Path.Combine(MapTextures, "TargetBackgroundGreen"));
            TargetBackgroundRed = content.Load<Texture2D>(Path.Combine(MapTextures, "TargetBackgroundRed"));
            StartPosition = content.Load<Texture2D>(Path.Combine(MapTextures, "StartPosition"));



            //HUD
            HudBackground = content.Load<Texture2D>(Path.Combine(GUI, "Background"));
            Money = content.Load<Texture2D>(Path.Combine(GUI, "Money"));
            Heart = content.Load<Texture2D>(Path.Combine(GUI, "Heart"));
            Trophy = content.Load<Texture2D>(Path.Combine(GUI, "Trophy"));
            Health = content.Load<Texture2D>(Path.Combine(GUI, "Health"));

            ButtonActive = content.Load<Texture2D>(Path.Combine(GUI, "ButtonBackgroundActive"));
            ButtonInactive = content.Load<Texture2D>(Path.Combine(GUI, "ButtonBackgroundInactive"));
            ButtonClick = content.Load<Texture2D>(Path.Combine(GUI, "ButtonBackgroundClick"));
            ButtonDisabled = content.Load<Texture2D>(Path.Combine(GUI, "ButtonBackgroundDisabled"));

            WeaponBuyActive = content.Load<Texture2D>(Path.Combine(GUI, "WeaponBuyInactive"));
            WeaponBuyInactive = content.Load<Texture2D>(Path.Combine(GUI, "WeaponBuyActive"));

            MainBackground = content.Load<Texture2D>(Path.Combine(GUI, "MainBackground"));
            SaveBacground = content.Load<Texture2D>(Path.Combine(GUI, "SaveBacground"));
            LoadBackground = content.Load<Texture2D>(Path.Combine(GUI, "LoadBackground"));
        }
    }
}
