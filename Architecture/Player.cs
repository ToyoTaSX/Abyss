using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    internal class Player : Entity
    {
        public Player() 
        {
            image = Arts.Player;
            Speed = 5;
        }
        public override void Update(Input input)
        {
            Velocity = input.GetMovementDirection();
            Position += Velocity * Speed;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }
    }
}
