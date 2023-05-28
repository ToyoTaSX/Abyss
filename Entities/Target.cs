using Abyss.Architecture;
using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Entities
{
    public class Target : Entity
    {
        private int _progress;
        public bool IsCollected { get => _progress >= 100; set => _progress = value ? 100 : 0; }
        
        public Target(Vector2 position)
        {
            Position = position;
            image = Arts.TargetBackgroundRed;
        }
        public override void Update(GameModel game)
        {
            if (IsCollected)
            {
                image = Arts.TargetBackgroundGreen;
                return;
            }
            if (game.Player.IsColliding(this))
                _progress++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_progress != 100 && _progress != 0)
                spriteBatch.DrawString(Arts.IngameFont, _progress.ToString(), CenterPosition - new Vector2(16, 16), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
