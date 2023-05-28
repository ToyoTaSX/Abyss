using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Abyss.Weapons
{
    public class Weapon
    {
        public readonly List<Type> WhoCenBeDamaged;
        private Random _random = new Random();
        public readonly Texture2D Image;
        private SoundEffect _sound;

        public WeaponName Name { get; private set; }
        public int PunchToExpire { get; set; }
        public int Damage { get; private set; }
        public int BulletSpeed { get; private set; }
        public int FramesCooldown { get; private set; }

        private int _cooldown;
        public int Cooldown
        {
            get => _cooldown;
            set => _cooldown = value <= 0 ? 0 : value % FramesCooldown;
        }

        public Weapon(WeaponName name, List<Type> damageList, int punchToExpire, int damage, int speed, int coolDown, Texture2D image, SoundEffect sound)
        {
            Name = name;
            WhoCenBeDamaged = damageList;
            PunchToExpire = punchToExpire;
            Damage = damage;
            BulletSpeed = speed;
            FramesCooldown = coolDown;
            Image = image;
            this._sound = sound; 
        }

        public virtual Bullet CreateBullet(Vector2 startPos, Vector2 direction)
        {
            var randomDir = _random.NextDouble() * _random.Next(-1, 2) * Math.PI / 32;
            direction += new Vector2((float)randomDir);
            direction.Normalize();
            var bullet = new Bullet(direction, BulletSpeed, PunchToExpire, startPos, Damage, WhoCenBeDamaged);
            bullet.Position -= bullet.Size / 2f;
            Cooldown = FramesCooldown - 1;
            _sound.Play();
            return bullet;
        }
    }
}
