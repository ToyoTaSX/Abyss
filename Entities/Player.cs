using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Abyss.Entities
{
    public class Player : Entity
    {
        public Weapon Weapon;
        public int Health { get; set; }
        public int Money { get; set; }
        public int MedecineCount { get; set; }
        public int FramesFromDamage = -25;
        public int FramesFromMedicine = 10;
        public Player()
        {
            image = Arts.Player;
            Speed = 5;
            Weapon = WeaponsFactory.CreateWeapon(WeaponName.Thompson, new List<Type>() { typeof(Enemy), typeof(Bullet) });
            Health = 999;
            MedecineCount = 2;
        }

        public override void OnDamage(Bullet bullet)
        {
            if (FramesFromDamage < 5)
                return;
            Health -= bullet.Damage;
            FramesFromDamage = 0;
            SoundEffects.PlayerDamage.Play();
        }

        public override bool IsExpired()
        {
            return Health <= 0;
        }

        public override void Update(GameModel game)
        {
            FramesFromDamage++;
            FramesFromMedicine++;
            Medicine(game);
            Move(game);
            Shoot(game);
            
        }

        private void Medicine(GameModel game)
        {
            if (FramesFromMedicine < 100)
                return;
            if (MedecineCount <= 0)
                return;
            if (!game.GameInput.WasKeyPressed(Keys.E))
                return;

            Health += 100;
            FramesFromMedicine = 0;
            MedecineCount--;
        }

        private void Shoot(GameModel game)
        {
            if (Weapon == null)
                return;
            var aimDir = game.GameInput.GetAimDirection();
            if (aimDir != Vector2.Zero && game.GameInput.IsLmbDown)
                Orientation = (float)Math.Atan2(aimDir.Y, aimDir.X);

            if (Weapon.Cooldown > 0)
                Weapon.Cooldown -= 1;
            else
            {
                if (aimDir == Vector2.Zero || !game.GameInput.IsLmbDown)
                    return;

                game.CurrentLevel.BullAddList.Add(Weapon.CreateBullet(CenterPosition, aimDir));
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
        }
    }
}
