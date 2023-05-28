using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Maps;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Abyss.Enemies
{
    public abstract class Enemy : Entity
    {
        public int Health;
        public Weapon Weapon;
        private int _framesFromDamage;
        protected Vector2 _nextTarget;
        protected bool _isActive;

        public override bool IsExpired()
        {
            return Health <= 0;
        }

        public override void OnDamage(Bullet bullet)
        {
            if (_framesFromDamage < 2)
                return;
            Health -= bullet.Damage;
            _framesFromDamage = 0;
            SoundEffects.EnemyDamage.Play();
        }

        public override void Update(GameModel game)
        {
            UpdateWeapon(Weapon);
            _framesFromDamage++;
        }

        protected void MoveToPlayer(GameModel game)
        {
            var oldPos = Position;

            if (Position.AlmostEqual(_nextTarget))
            {
                var nextPos = game.CurrentLevel.LevelMap.GetNextPosition(_nextTarget, game.Player.CenterPosition);
                _nextTarget = nextPos;
            }

            if (Speed * Speed > Position.DistanceSquared(_nextTarget))
            {
                Position = _nextTarget;
                return;
            }
            var direction = _nextTarget - Position;
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
            var aimDir = (game.Player.CenterPosition + game.Player.Velocity * game.Player.Speed) - CenterPosition;
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
                if (!Map.EmtyStates.Contains(map[point.X, point.Y]))
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
