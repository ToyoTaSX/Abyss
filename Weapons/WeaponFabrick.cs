using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Weapons
{
    static class WeaponFabrick
    {
        public static readonly List<string> WeaponsList = new List<string> 
        {
            "ak47",
            "awp",
            "deagle",
            "rickochet"
        };

        private static readonly Dictionary<string, WeaponData> WeaponsData = new Dictionary<string, WeaponData>
        {
            { "ak47", new WeaponData("ak47", 10, 15, 1, 15)},
            { "awp", new WeaponData("awp", 50, 120, 1, 120)},
            { "deagle", new WeaponData("deagle", 20, 60, 1, 60)},
            { "rickochet", new WeaponData("rickochet", 10, 10, 10, 15)},
        };
        

        public static Weapon CreateWeapon(string name, Entity owner)
        {
            var weapon = new Weapon();
            var wData = WeaponsData[name];
            weapon.Name = wData.Name;
            weapon.Owner = owner;
            weapon.PunchToExpire = wData.PunchToExpire;
            weapon.BulletSpeed = wData.BulletSpeed;
            weapon.Damage = wData.Damage;
            weapon.FramesCooldown = wData.FramesCooldown;
            return weapon;
        }
    }

    class WeaponData
    {
        public readonly string Name;
        public readonly int BulletSpeed;
        public readonly int Damage;
        public readonly int PunchToExpire;
        public readonly int FramesCooldown;  

        public WeaponData(string name, int bulletSpeed, int damage, int punches, int cooldown)
        {
            Name = name;
            BulletSpeed = bulletSpeed;
            Damage = damage;
            PunchToExpire = punches;
            FramesCooldown = cooldown;
        }
    }
}
