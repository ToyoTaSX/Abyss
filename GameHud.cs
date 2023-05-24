using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    public class GameHud
    {
        public readonly int Width;
        public readonly int Heigth;

        private int Health;
        private int Money;
        private int EnemiesCount;
        private int CountOfTargets;
        private int AlreadyCollectedCount;
        private int CurrentLevel;
        private Weapon Weapon;

        public GameHud(int width, int heigth) 
        {
            Width = width;
            Heigth = heigth;
        }
        public void Update(GameModel game)
        {
            Health = game.Player.Health;
            Money = game.Player.Money;
            EnemiesCount = game.CurrentLevel.Entities.Count(e => e is Enemy);
            CountOfTargets = 5;
            AlreadyCollectedCount = 2;
            Weapon = game.Player.weapon;
            CurrentLevel = game.Levels.IndexOf(game.CurrentLevel) + 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var hudTopLeft = new Vector2(0, Heigth - 88);
            spriteBatch.Draw(Arts.HudBackground, hudTopLeft - new Vector2(10, 0), null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            var textDelta = new Vector2(80, 20);
            var paramDelta = new Vector2(200, 0);
            // Сердечко
            var heartPos = hudTopLeft + new Vector2(20, 15);
            spriteBatch.Draw(Arts.Heart, heartPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{Health}", heartPos + textDelta, Color.WhiteSmoke);

            // Монетка
            var coinPos = heartPos + paramDelta;
            spriteBatch.Draw(Arts.Money, coinPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{Money}", coinPos + textDelta, Color.WhiteSmoke);

            // Аптечки
            var healthPos = coinPos + paramDelta;
            spriteBatch.Draw(Arts.Health, healthPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"Xulku", healthPos + textDelta, Color.WhiteSmoke);

            // Рейдер
            spriteBatch.DrawString(Arts.Font, $"Enemies {EnemiesCount}", new Vector2(20, 220), Color.Black);

            // Звездочка
            var trophyPos = new Vector2(20, 50);
            spriteBatch.Draw(Arts.Trophy, trophyPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{AlreadyCollectedCount}/{CountOfTargets}", trophyPos + textDelta, Color.Black);

            // Оружие
            if (Weapon == null)
                return;
            var scale = 80 / Weapon.Image.Height;
            var imagePos = new Vector2(Width / 2 - (Weapon.Image.Width * scale / 2), Heigth - 80);
            spriteBatch.Draw(Weapon.Image, imagePos, null, Color.White, 0, Vector2.Zero, scale, 0, 0);
            var textPos = new Vector2(imagePos.X + (Weapon.Image.Width * scale) + 100, (healthPos + textDelta).Y);
            spriteBatch.DrawString(Arts.Font, $"{Weapon.Name}   [ DMG:{Weapon.Damage}    SPD:{Weapon.BulletSpeed} ]", textPos, Color.Gray);
        }
    }
}
