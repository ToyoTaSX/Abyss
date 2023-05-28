using Abyss.Weapons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    public static class SaveLoadManager
    {
        private static readonly string _saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Abyss", "usersaves");

        public static void SavePlayerData(SaveData playerData, string fileName)
        {
            string savePath = Path.Combine(_saveFolderPath, fileName);
            string json = JsonConvert.SerializeObject(playerData);
            File.WriteAllText(savePath, json);
        }

        public static SaveData LoadPlayerData(string fileName)
        {
            string savePath = Path.Combine(_saveFolderPath, fileName);
            if (!File.Exists(savePath))
            {
                return null;
            }

            string json = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }

        public static string LoadSavesDateStr(string fileName)
        {
            string savePath = Path.Combine(_saveFolderPath, fileName);
            if (!File.Exists(savePath))
            {
                return "Пусто";
            }

            string json = File.ReadAllText(savePath);
            var date = JsonConvert.DeserializeObject<SaveData>(json).DateTime;
            return $"{date.Day}.{date.Month}.{date.Year}";
        }
    }

    public class SaveData
    {
        public readonly int Level;
        public readonly int Money;
        public readonly int Health;
        public readonly WeaponName Weapon;
        public readonly int MedecineCount;
        public readonly DateTime DateTime;
        public readonly int TotalTargets;
        public readonly int CollectedTargets;
        public readonly int EnemiesCount;

        public SaveData(int level, int money, int health, int medecintCount, WeaponName weapon, int enemiesCount, int collectedTargets, int totalTargets)
        {
            DateTime = DateTime.Now;
            Level = level;
            Money = money;
            Health = health;
            Weapon = weapon;
            MedecineCount = medecintCount;
            EnemiesCount = enemiesCount;
            CollectedTargets = collectedTargets;
            TotalTargets = totalTargets;
        }
    }
}
