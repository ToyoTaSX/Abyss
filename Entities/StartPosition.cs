using Abyss.Architecture;
using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Entities
{
    public class StartPosition : Entity
    {
        private int progress;
        public bool IsPassed { get => progress >= 100; set => progress = value ? 100 : 0; }

        public StartPosition(Vector2 position)
        {
            Position = position;
            image = Arts.StartPosition;
        }
        public override void Update(GameModel game)
        {
            if (game.Player.IsColliding(this) && game.CurrentLevel.IsAllTargetsCollected)
                progress++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (progress != 100 && progress != 0)
                spriteBatch.DrawString(Arts.IngameFont, progress.ToString(), CenterPosition - new Vector2(16, 16), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
