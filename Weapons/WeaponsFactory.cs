using System;
using System.Collections.Generic;
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
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Weapons
{
    public static class WeaponsFactory
    {
        private static readonly Dictionary<WeaponName, WeaponData> WeaponsData = new Dictionary<WeaponName, WeaponData>
        {
            //Name : Name, speed, damage, punches, cooldown, image
            { WeaponName.DP28, new WeaponData(WeaponName.DP28, 5, 5, 1, 5, WeaponImages.DP28)},
            { WeaponName.Colt, new WeaponData(WeaponName.Colt, 10, 10, 1, 15, WeaponImages.Colt)},
            { WeaponName.BAR, new WeaponData(WeaponName.BAR, 10, 10, 2, 5, WeaponImages.BAR)},
            { WeaponName.Gewehr43, new WeaponData(WeaponName.Gewehr43, 20, 35, 3, 25, WeaponImages.Gewehr43)},
            { WeaponName.Kar98K, new WeaponData(WeaponName.Kar98K, 30, 95, 4, 50, WeaponImages.Kar98K)},
            { WeaponName.Mosin, new WeaponData(WeaponName.Mosin, 30, 95, 4, 50, WeaponImages.Mosin)},
            { WeaponName.MP40, new WeaponData(WeaponName.MP40, 10, 5, 1, 5, WeaponImages.MP40)},
            { WeaponName.PPSH, new WeaponData(WeaponName.PPSH, 10, 15, 1, 15, WeaponImages.PPSH)},
            { WeaponName.Revolver, new WeaponData(WeaponName.Revolver, 30, 35, 1, 60, WeaponImages.Revolver)},
            { WeaponName.Stg44, new WeaponData(WeaponName.Stg44, 10, 5, 1, 7, WeaponImages.Stg44)},
            { WeaponName.Thompson, new WeaponData(WeaponName.Thompson, 10, 15, 1, 20, WeaponImages.Thompson)},
            { WeaponName.LMG99, new WeaponData(WeaponName.LMG99, 20, 15, 1, 2, WeaponImages.LMG99)},
        };
        

        public static Weapon CreateWeapon(WeaponName name, List<Type> whoCanBeDamage)
        {
            var wData = WeaponsData[name];
            var weapon = new Weapon(name, whoCanBeDamage, wData.PunchToExpire, wData.Damage, wData.BulletSpeed, wData.FramesCooldown, wData.Image);
            return weapon;
        }
    }

    class WeaponData
    {
        public readonly WeaponName Name;
        public readonly int BulletSpeed;
        public readonly int Damage;
        public readonly int PunchToExpire;
        public readonly int FramesCooldown;
        public readonly Texture2D Image;

        public WeaponData(WeaponName name, int bulletSpeed, int damage, int punches, int cooldown, Texture2D image)
        {
            Name = name;
            BulletSpeed = bulletSpeed;
            Damage = damage;
            PunchToExpire = punches;
            FramesCooldown = cooldown;
            Image = image;
        }
    }

    public enum WeaponName
    {
        DP28,
        Colt,
        BAR,
        Gewehr43,
        Kar98K,
        Mosin,
        MP40,
        PPSH,
        Revolver,
        Stg44,
        Thompson,
        LMG99,
    }
}
