using Abyss.Architecture;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abyss.Weapons
{
    class Weapon
    {
        public string Name;
        public Entity Owner;
        public int PunchToExpire;
        public int Damage { get; set; }
        public int BulletSpeed { get; set; }
        public int FramesCooldown { get; set; }
        private int _cooldown;
        public virtual int Cooldown
        {
            get => _cooldown;
            set => _cooldown = value <= 0 ? 0 : value % FramesCooldown;
        }
        public virtual Bullet CreateBullet(Vector2 startPos, Vector2 direction)
        {
            direction.Normalize();
            var bullet = new Bullet();
            bullet.Owner = Owner;
            bullet.PunchToExpire = PunchToExpire;
            bullet.Velocity = BulletSpeed * direction;
            bullet.Position = startPos;
            bullet.Damage = Damage;

            Cooldown = FramesCooldown - 1;
            return bullet;
        }
    }
}
