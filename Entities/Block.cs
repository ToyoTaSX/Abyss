using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Entities
{
    internal class Block : Entity
    {
        public Block(Vector2 position) 
        {
            this.image = Arts.Grass;
            Position = position;
            IsHaveCollision = true;
        }
        public override void Update(GameModel game)
        {
            return;
        }
    }
}
