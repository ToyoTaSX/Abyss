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

namespace Abyss.Hud
{
    public class GameHud
    {
        public readonly int Width;
        public readonly int Heigth;

        private int health;
        private int money;
        private int countOfTargets;
        private int alreadyCollectedCount;
        private int medecine;
        private Weapon Weapon;
        private int framesMedicine;
        private int framesToStart;

        public GameHud(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }
        public void Update(GameModel game)
        {
            health = game.Player.Health;
            money = game.Player.Money;
            countOfTargets = game.CurrentLevel.Targets.Count;
            alreadyCollectedCount = game.CurrentLevel.CollectedTargetsCount;
            Weapon = game.Player.Weapon;
            medecine = game.Player.MedecineCount;
            framesMedicine = game.Player.FramesFromMedicine > 100 ? 100 : game.Player.FramesFromMedicine;
            framesToStart = game.FramesToStart;
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
            spriteBatch.DrawString(Arts.Font, $"{health}", heartPos + textDelta, Color.WhiteSmoke);

            // Монетка
            var coinPos = heartPos + paramDelta;
            spriteBatch.Draw(Arts.Money, coinPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{money}", coinPos + textDelta, Color.WhiteSmoke);

            // Аптечки
            var healthPos = coinPos + paramDelta;
            spriteBatch.Draw(Arts.Health, healthPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            var str = framesMedicine == 100 ? "ready" : framesMedicine.ToString();
            if (medecine > 0)
                str = '(' + str + ")";
            else
                str = "";
            spriteBatch.DrawString(Arts.Font, $"{medecine} {str}", healthPos + textDelta, Color.WhiteSmoke);

            // Звездочка
            var trophyPos = new Vector2(20, 50);
            spriteBatch.Draw(Arts.Trophy, trophyPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{alreadyCollectedCount}/{countOfTargets}", trophyPos + textDelta, Color.Black);

            // Оружие
            if (Weapon == null)
                return;
            var scale = 80 / Weapon.Image.Height;
            var imagePos = new Vector2(Width / 2 - Weapon.Image.Width * scale / 2, Heigth - 80);
            spriteBatch.Draw(Weapon.Image, imagePos, null, Color.White, 0, Vector2.Zero, scale, 0, 0);
            var textPos = new Vector2(imagePos.X + Weapon.Image.Width * scale + 100, (healthPos + textDelta).Y);
            spriteBatch.DrawString(Arts.Font, $"{Weapon.Name}   [ DMG:{Weapon.Damage}    SPD:{Weapon.BulletSpeed} ]", textPos, Color.Gray);

            //Обратный отсчет
            if (framesToStart > 0)
            {
                var text = ((framesToStart + 20) / 20).ToString();
                var startScale = 3;
                var v = Arts.Font.MeasureString(text) * startScale;
                var pos = new Vector2(1920, 1080) / 2 - v / 2;
                spriteBatch.DrawString(Arts.Font, text, pos, Color.White, 0, Vector2.Zero, startScale, 0, 0);
            }
        }
    }
}
