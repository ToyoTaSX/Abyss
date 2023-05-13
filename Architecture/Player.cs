using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace Abyss
{
    internal class Player : Entity
    {
        public Player() 
        {
            image = Arts.Player;
            Speed = 5;
            IsHaveCollision = true;
        }

        public override void Update(GameModel game)
        {
           Move(game);
        }

        private void Move(GameModel game)
        {
            var oldPos = Position;
            Velocity += game.GameInput.GetMovementDirection() * 0.5f * Speed;

            Position.X += Velocity.X;
            foreach (var entity in game.CurrentLevel.Entities)
                if (entity != this && entity.IsColliding(this))
                {
                    Position.X = oldPos.X;
                    break;
                }

            Position.Y += Velocity.Y;
            foreach (var entity in game.CurrentLevel.Entities)
                if (entity != this && entity.IsColliding(this))
                {
                    Position.Y = oldPos.Y;
                    break;
                }

            //var delta = Position - oldPos;
            //if (delta.LengthSquared() != 0)
            //    Orientation = (float)Math.Atan2(delta.Y, delta.X);
            Velocity *= 0.6f;
        }
    }
}
