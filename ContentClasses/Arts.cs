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
using System.Reflection.Metadata;

namespace Abyss.ContentClasses
{
    public static class Arts
    {
        private static Random random = new Random();

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

        public static Texture2D Box;
        public static Texture2D Cement;
        public static Texture2D WoodPlanks;
        public static Texture2D Water;

        //Bricks
        private static int bricksCount = 5;
        private static List<Texture2D> bricksTextures;
        public static Texture2D Bricks { get => bricksTextures[random.Next(bricksCount)]; }

        //Dirt
        private static int dirtsCount = 5;
        private static List<Texture2D> dirtTextures;
        public static Texture2D Dirt { get => dirtTextures[random.Next(dirtsCount)]; }

        //Grass
        private static int grassCount = 3;
        private static List<Texture2D> grassTextures;
        public static Texture2D Grass { get => grassTextures[random.Next(grassCount)]; }

        //Sand
        private static int sandCount = 2;
        private static List<Texture2D> sandTextures;
        public static Texture2D Sand { get => sandTextures[random.Next(sandCount)]; }

        //Stone
        private static int stoneCount = 3;
        private static List<Texture2D> stoneTextures ;
        public static Texture2D Stone{ get => stoneTextures[random.Next(stoneCount)]; }

        //Tiles
        private static int tilesCount = 2;
        private static List<Texture2D> tilesTextures;
        public static Texture2D Tiles { get => tilesTextures[random.Next(tilesCount)]; }


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
            Box = content.Load<Texture2D>(Path.Combine(MapTextures, "Box"));
            Cement = content.Load<Texture2D>(Path.Combine(MapTextures, "Cement"));
            WoodPlanks = content.Load<Texture2D>(Path.Combine(MapTextures, "WoodPlanks"));
            Water = content.Load<Texture2D>(Path.Combine(MapTextures, "Water"));

            bricksTextures = LoadMapTextures(content, "Bricks", bricksCount);
            dirtTextures= LoadMapTextures(content, "Dirt", dirtsCount);
            grassTextures= LoadMapTextures(content, "Grass", grassCount);
            sandTextures = LoadMapTextures(content, "Sand", sandCount);
            stoneTextures = LoadMapTextures(content, "Stone", stoneCount);
            tilesTextures = LoadMapTextures(content, "Tiles", tilesCount);



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

        private static List<Texture2D> LoadMapTextures(ContentManager content, string name, int count)
        {
            var list = new List<Texture2D>();
            var path = Path.Combine(MapTextures, name);
            for (int i = 1; i <= count; i++)
                list.Add(content.Load<Texture2D>(path + i.ToString()));
            return list;
        }
    }
}
