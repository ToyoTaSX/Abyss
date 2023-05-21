using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Abyss
{
    internal class Player : Entity
    {
        public Weapon weapon;
        public int Health { get; private set; }
        public int Money;
        // Пуля, сколько кадров прошло с попадания
        private int framesFromDamage = 10;
        public Player() 
        {
            image = Arts.Player;
            Speed = 5;
            weapon = WeaponFabrick.CreateWeapon("awp", this);
            Health = 100;
        }

        public override void OnDamage(Bullet bullet)
        {
            if (framesFromDamage < 10)
                return;
            Health -= bullet.Damage;
            framesFromDamage = 0;

        }

        public override bool IsExpired()
        {
            return Health <= 0;
        }

        public override void Update(GameModel game)
        {
            framesFromDamage++;
            Move(game);
            Shoot(game);
        }

        private void Shoot(GameModel game)
        {
            if (weapon == null)
                return;
            if (weapon.Cooldown > 0)
                weapon.Cooldown -= 1;
            else
            {
                var aimDir = game.GameInput.GetAimDirection();
                if (aimDir == Vector2.Zero || !game.GameInput.IsLmbDown)
                    return;

                game.CurrentLevel.AddList.Add(weapon.CreateBullet(Position, aimDir));
            }
        }
        private void Move(GameModel game)
        {
            var oldPos = Position;
            Velocity = game.GameInput.GetMovementDirection() * Speed;

            Position.X += Velocity.X;
            foreach (var obj in game.CurrentLevel.Objects)
                if (obj.IsColliding(this))
                {
                    Position.X = oldPos.X;
                    break;
                }

            Position.Y += Velocity.Y;
            foreach (var obj in game.CurrentLevel.Objects)
                if (obj.IsColliding(this))
                {
                    Position.Y = oldPos.Y;
                    break;
                }
            var delta = Position - oldPos;
            //Orientation = (float)Math.Atan2(delta.Y, delta.X);
            Velocity *= 0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Arts.Font, Map.ToMapPosition(Position).ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.Draw(image, Position, null, Color.White, Orientation, Vector2.Zero, 1, 0, 0f);
        }
    }
}
