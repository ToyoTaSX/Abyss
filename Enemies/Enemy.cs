using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss.Architecture;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Enemies
{
    abstract class Enemy : Entity
    {
        public int Health;
        public Weapon Weapon;

        public override bool IsExpired()
        {
            return Health <= 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, Orientation, Size / 2f, (8f / 7f), 0, 0f);
        }
    }
}
