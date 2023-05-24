using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Weapons
{
    public class Weapon
    {
        public readonly List<Type> WhoCenBeDamaged;
        private Random _random = new Random();
        public readonly Texture2D Image;

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

        public Weapon(WeaponName name, List<Type> damageList, int punchToExpire, int damage, int speed, int coolDown, Texture2D image)
        {
            Name = name;
            WhoCenBeDamaged = damageList;
            PunchToExpire = punchToExpire;
            Damage = damage;
            BulletSpeed = speed;
            FramesCooldown = coolDown;
            Image = image;
        }

        public virtual Bullet CreateBullet(Vector2 startPos, Vector2 direction)
        {
            var randomDir = _random.NextDouble() * _random.Next(-1, 2) * Math.PI / 32;
            direction += new Vector2((float)randomDir);
            direction.Normalize();
            var bullet = new Bullet(direction, BulletSpeed, PunchToExpire, startPos, Damage, WhoCenBeDamaged);
            bullet.Position -= bullet.Size / 2f;
            Cooldown = FramesCooldown - 1;
            return bullet;
        }
    }
}
