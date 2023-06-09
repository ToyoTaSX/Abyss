﻿using Abyss.ContentClasses;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Abyss.Weapons
{
    public static class WeaponsFactory
    {
        private static readonly Dictionary<WeaponName, WeaponData> WeaponsData = new Dictionary<WeaponName, WeaponData>
        {
            //Name : Name, speed, damage, punches, cooldown, image

            // Рейтинг
            // 5
            { WeaponName.DP28, new WeaponData(WeaponName.DP28, 5, 5, 1, 5, WeaponImages.DP28, SoundEffects.Shoot2)},

            //7
            { WeaponName.Colt, new WeaponData(WeaponName.Colt, 10, 10, 1, 15, WeaponImages.Colt, SoundEffects.Shoot1)},

            //2
            { WeaponName.BAR, new WeaponData(WeaponName.BAR, 10, 10, 2, 5, WeaponImages.BAR, SoundEffects.Shoot2)},
            { WeaponName.Gewehr43, new WeaponData(WeaponName.Gewehr43, 20, 35, 3, 25, WeaponImages.Gewehr43, SoundEffects.Shoot3)},

            //3
            { WeaponName.Kar98K, new WeaponData(WeaponName.Kar98K, 30, 100, 4, 60, WeaponImages.Kar98K, SoundEffects.Shoot4)},
            { WeaponName.Mosin, new WeaponData(WeaponName.Mosin, 30, 100, 4, 60, WeaponImages.Mosin, SoundEffects.Shoot4)},

            // 4
            { WeaponName.MP40, new WeaponData(WeaponName.MP40, 10, 5, 1, 7, WeaponImages.MP40, SoundEffects.Shoot1)},
            { WeaponName.PPSH, new WeaponData(WeaponName.PPSH, 10, 15, 1, 10, WeaponImages.PPSH, SoundEffects.Shoot1)},

            // 5
            { WeaponName.Revolver, new WeaponData(WeaponName.Revolver, 30, 50, 1, 20, WeaponImages.Revolver, SoundEffects.Shoot3)},

            // 6
            { WeaponName.Stg44, new WeaponData(WeaponName.Stg44, 10, 5, 1, 7, WeaponImages.Stg44, SoundEffects.Shoot2)},
            { WeaponName.Thompson, new WeaponData(WeaponName.Thompson, 10, 15, 1, 9, WeaponImages.Thompson, SoundEffects.Shoot2)},

            //1
            { WeaponName.LMG99, new WeaponData(WeaponName.LMG99, 20, 15, 1, 2, WeaponImages.LMG99, SoundEffects.Shoot3)},

            //0
            { WeaponName.Cheat, new WeaponData(WeaponName.Cheat, 80, 150, 10, 1, WeaponImages.LMG99, SoundEffects.Shoot2)},
        };
        

        public static Weapon CreateWeapon(WeaponName name, List<Type> whoCanBeDamage)
        {
            var wData = WeaponsData[name];
            var weapon = new Weapon(name, whoCanBeDamage, wData.PunchToExpire, wData.Damage, wData.BulletSpeed, wData.FramesCooldown, wData.Image, wData.Sound);
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
        public readonly SoundEffect Sound;

        public WeaponData(WeaponName name, int bulletSpeed, int damage, int punches, int cooldown, Texture2D image, SoundEffect sound)
        {
            Name = name;
            BulletSpeed = bulletSpeed;
            Damage = damage;
            PunchToExpire = punches;
            FramesCooldown = cooldown;
            Image = image;
            Sound = sound;
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

        Cheat
    }
}
