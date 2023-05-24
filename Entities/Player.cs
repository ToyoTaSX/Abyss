﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Abyss.Objects;
using Abyss.Weapons;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Abyss.Enemies;
using Abyss.ContentClasses;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;

namespace Abyss.Entities
{
    public class Player : Entity
    {
        public Weapon weapon;
        public int Health { get; private set; }
        public int Money;
        private int framesFromDamage = 10;
        public Player()
        {
            image = Arts.Player;
            Speed = 5;
            weapon = WeaponsFactory.CreateWeapon(WeaponName.Mosin, new List<Type>() { typeof(Enemy), typeof(Bullet) });
            Health = 999;
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

                game.CurrentLevel.BullAddList.Add(weapon.CreateBullet(CenterPosition, aimDir));
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
            Orientation = delta != Vector2.Zero ? (float)Math.Atan2(delta.Y, delta.X) : Orientation;
            Velocity *= 0f;
        }
    }
}