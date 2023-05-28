using Abyss.Architecture;
using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Entities
{
    public class StartPosition : Entity
    {
        private int _progress;
        public bool IsPassed { get => _progress >= 100; set => _progress = value ? 100 : 0; }

        public StartPosition(Vector2 position)
        {
            Position = position;
            image = Arts.StartPosition;
        }
        public override void Update(GameModel game)
        {
            if (game.Player.IsColliding(this) && game.CurrentLevel.IsAllTargetsCollected)
                _progress++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (progress != 100 && progress != 0)
            spriteBatch.DrawString(Arts.IngameFont, _progress.ToString(), CenterPosition - new Vector2(16, 16), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
