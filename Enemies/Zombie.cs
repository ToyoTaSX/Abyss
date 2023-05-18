using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Enemies
{
    class Zombie : Enemy
    {
        public Zombie()
        {
            image = Arts.Zombie;
            Health = 100;
            Velocity = new Vector2(0, 0);
            Position = new Vector2(1000, 500);
        }

        public override void Update(GameModel game)
        {
            Position += Velocity;
        }
    }
}
