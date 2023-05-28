using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Hud
{
    public class GameHud
    {
        public readonly int Width;
        public readonly int Heigth;

        private int _health;
        private int _money;
        private int _countOfTargets;
        private int _alreadyCollectedCount;
        private int _medecine;
        private Weapon _weapon;
        private int _framesMedicine;
        private int _framesToStart;

        public GameHud(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }
        public void Update(GameModel game)
        {
            _health = game.Player.Health;
            _money = game.Player.Money;
            _countOfTargets = game.CurrentLevel.Targets.Count;
            _alreadyCollectedCount = game.CurrentLevel.CollectedTargetsCount;
            _weapon = game.Player.Weapon;
            _medecine = game.Player.MedecineCount;
            _framesMedicine = game.Player.FramesFromMedicine > 100 ? 100 : game.Player.FramesFromMedicine;
            _framesToStart = game.FramesToStart;
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
            spriteBatch.DrawString(Arts.Font, $"{_health}", heartPos + textDelta, Color.WhiteSmoke);

            // Монетка
            var coinPos = heartPos + paramDelta;
            spriteBatch.Draw(Arts.Money, coinPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{_money}", coinPos + textDelta, Color.WhiteSmoke);

            // Аптечки
            var healthPos = coinPos + paramDelta;
            spriteBatch.Draw(Arts.Health, healthPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            var str = _framesMedicine == 100 ? "готов" : _framesMedicine.ToString();
            if (_medecine > 0)
                str = '(' + str + ")";
            else
                str = "";
            spriteBatch.DrawString(Arts.Font, $"{_medecine} {str}", healthPos + textDelta, Color.WhiteSmoke);

            // Звездочка
            var trophyPos = new Vector2(20, 50);
            spriteBatch.Draw(Arts.Trophy, trophyPos, null, Color.White, 0, Vector2.Zero, 2f, 0, 0);
            spriteBatch.DrawString(Arts.Font, $"{_alreadyCollectedCount}/{_countOfTargets}", trophyPos + textDelta, Color.Black);

            // Оружие
            if (_weapon == null)
                return;
            var scale = 80 / _weapon.Image.Height;
            var imagePos = new Vector2(Width / 2 - _weapon.Image.Width * scale / 2, Heigth - 80);
            spriteBatch.Draw(_weapon.Image, imagePos, null, Color.White, 0, Vector2.Zero, scale, 0, 0);
            var textPos = new Vector2(imagePos.X + _weapon.Image.Width * scale + 100, (healthPos + textDelta).Y);
            spriteBatch.DrawString(Arts.Font, $"{_weapon.Name}   [ УР:{_weapon.Damage}    СКОР:{_weapon.BulletSpeed} ]", textPos, Color.Gray);

            //Обратный отсчет
            if (_framesToStart > 0)
            {
                var text = ((_framesToStart + 20) / 20).ToString();
                var startScale = 3;
                var v = Arts.Font.MeasureString(text) * startScale;
                var pos = new Vector2(1920, 1080) / 2 - v / 2;
                spriteBatch.DrawString(Arts.Font, text, pos, Color.White, 0, Vector2.Zero, startScale, 0, 0);
            }
        }
    }
}
