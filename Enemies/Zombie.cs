using Abyss.Architecture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Enemies
{
    class Zombie : Enemy
    {
        public Zombie(Vector2 position)
        {
            image = Arts.Zombie;
            Health = 100;
            Position = position;
            Speed = 3;
            target = position;
        }

        public override void OnExpire(GameModel game)
        {
            game.Player.Money += 100;
            base.OnExpire(game);
        }

        public override void Update(GameModel game)
        {
            if (Position.AlmostEqual(target))
            {
                var nextPos = game.CurrentLevel.LevelMap.GetNextPosition(target, game.Player.CenterPosition);
                target = nextPos;
            }

            if (Speed * Speed > Position.DistanceSquared(target))
            {
                Position = target;
                return;
            }
            var direction = target - Position;
            if (direction != Vector2.Zero)
                direction.Normalize();

            var oldPos = Position;
            Position += direction * Speed;
            base.Update(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Arts.Font, Map.ToMapPosition(Position).ToString(), new Vector2(100, 200), Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
