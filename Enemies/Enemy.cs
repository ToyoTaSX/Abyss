using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private int framesFromDamage;
        protected Vector2 target;

        public override bool IsExpired()
        {
            return Health <= 0;
        }
        public override void OnDamage(Bullet bullet)
        {
            if (framesFromDamage < 10)
                return;
            Health -= bullet.Damage;
            framesFromDamage = 0;
        }

        public override void Update(GameModel game)
        {
            framesFromDamage++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, Orientation, Vector2.Zero, 1, 0, 0f);
        }
    }
}
