using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Entities;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Abyss.Enemies
{
    public class Reider : Enemy
    {
        public Reider(Vector2 position, WeaponName weapon)
        {
            image = Arts.Reider;
            Health = 100;
            Position = position;
            Speed = 2;
            _nextTarget = position;
            Weapon = WeaponsFactory.CreateWeapon(weapon, new List<Type>() { typeof(Player), typeof(Bullet) });
        }

        public override void OnExpire(GameModel game)
        {
            game.Player.Money += 100;
            base.OnExpire(game);
        }

        public override void Update(GameModel game)
        {
            if ((game.Player.Position - Position).LengthSquared() > 32 * 32 * 20 * 20)
            {
                _isActive = false;
                return;
            }
            var oldPos = Position;
            var canShoot = IsCanShootToPlayer(game);
            if (canShoot || _isActive)
                _isActive = true;
            if (!_isActive)
                return;

            if (canShoot)
                Shoot(game);
            if (!canShoot || (canShoot && CenterPosition.DistanceSquared(game.Player.CenterPosition) > 3 * 32 * 3 * 32))
                MoveToPlayer(game);

            var delta = Position - oldPos;
            Orientation = delta != Vector2.Zero ? (float)Math.Atan2(delta.Y, delta.X) : Orientation;
            base.Update(game);
        }
    }
}
