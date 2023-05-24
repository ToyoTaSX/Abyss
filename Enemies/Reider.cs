using Abyss.ContentClasses;
using Abyss.Entities;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class Reider : Enemy
    {
        public Reider(Vector2 position)
        {
            image = Arts.Reider;
            Health = 100;
            Position = position;
            Speed = 2;
            target = position;
            Weapon = WeaponsFactory.CreateWeapon(WeaponName.Thompson, new List<Type>() { typeof(Player), typeof(Bullet) });
        }

        public override void OnExpire(GameModel game)
        {
            game.Player.Money += 100;
            base.OnExpire(game);
        }

        public override void Update(GameModel game)
        {
            var oldPos = Position;
            var canShoot = IsCanShootToPlayer(game);
            if (canShoot || IsActive)
                IsActive = true;
            if (!IsActive)
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
