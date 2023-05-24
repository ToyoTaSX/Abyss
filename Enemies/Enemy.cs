using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Abyss.Maps;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;

namespace Abyss.Enemies
{
    public abstract class Enemy : Entity
    {
        public int Health;
        public Weapon Weapon;
        private int framesFromDamage;
        protected Vector2 target;
        protected bool IsActive;

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
            UpdateWeapon(Weapon);
            framesFromDamage++;
        }

        protected void MoveToPlayer(GameModel game)
        {
            var oldPos = Position;

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

            var delta = Position - oldPos;
            Orientation = delta != Vector2.Zero ? (float)Math.Atan2(delta.Y, delta.X) : Orientation;
            Position += direction * Speed;
        }

        protected void UpdateWeapon(Weapon weapon)
        {
            if (Weapon == null)
                return;
            if (Weapon.Cooldown > 0)
                Weapon.Cooldown -= 1;
        }

        protected void Shoot(GameModel game)
        {
            if (Weapon.Cooldown > 0)
                return;
            var aimDir = game.Player.CenterPosition - CenterPosition;
            aimDir.Normalize();
            game.CurrentLevel.BullAddList.Add(Weapon.CreateBullet(CenterPosition, aimDir));
        }

        protected bool IsCanShootToPlayer(GameModel game)
        {
            var map = game.CurrentLevel.LevelMap;
            if ((game.Player.Position - this.Position).LengthSquared() > 32 * 15 * 32 * 15)
                return false;
            foreach (var vector in CenterPosition.GetPointsBetween(game.Player.CenterPosition))
            {
                var point = Map.ToMapPosition(vector);
                if (map[point.X, point.Y] != CellState.Empty)
                    return false;
            }
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position + Size / 2, null, Color.White, Orientation, Size / 2, 1, 0, 0f);
        }
    }
}
