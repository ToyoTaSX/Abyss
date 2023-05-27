using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Hud
{
    public class LoadingScreen
    {
        private int _dotsCount;
        private int _framesFromUpdateCount;

        public void Draw(SpriteBatch spriteBatch, int width, int heigth)
        {
            var rect = new Rectangle(0, 0, width, heigth);
            spriteBatch.DrawRectangle(rect, Color.Black);
            var s = "Loading" + new string('.', _dotsCount);
            var size = Arts.Font.MeasureString(s);
            spriteBatch.DrawString(Arts.Font, s, new Vector2(width  / 2, heigth / 2) - size / 2, Color.White);
            if (_framesFromUpdateCount < 20)
                _framesFromUpdateCount++;
            else
            {
                _framesFromUpdateCount = 0;
                _dotsCount = (_dotsCount + 1) % 4;
            }
        }
    }
}
