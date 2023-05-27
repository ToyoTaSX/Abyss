using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abyss.Gui
{
    public class SaveMenu : Menu
    {
        public SaveMenu(int width, int heigth) : base(width, heigth)
        {}

        public override void OnCreate()
        {
            Background = Arts.SaveBacground;
            State = MenuState.SaveMenu;

            var leftCenter = Width / 2 - Arts.ButtonActive.Width / 2;
            var padding = new Vector2(0, 120);

            var pos = new Vector2(leftCenter, 200);
            for (int i = 1; i <= 5; i++)
            {
                var f = CreateSaveFunction($"save{i}");
                var text = SaveLoadManager.LoadSavesDateStr($"save{i}");
                var btn = new Button(f, (int)pos.X, (int)pos.Y, 400, 100, text);
                Buttons.Add(btn);
                pos += padding;
            }

            pos += new Vector2(0, 50);
            Action<View> returnAction = (v) => v.MenuState = MenuState.MainMenu;
            var returnBtn = new Button(returnAction, (int)pos.X, (int)pos.Y, 400, 100, "Назад");
            Buttons.Add(returnBtn);
        }

        public override void OnResume()
        {}

        public override void Update(View view)
        {
            for (int i = 1; i <= 5; i++)
            {
                var text = SaveLoadManager.LoadSavesDateStr($"save{i}");
                var btn = Buttons[i - 1];
                btn.Text = text;
            }
            base.Update(view);
        }

        private Action<View> CreateSaveFunction(string filename)
        {
            Action<View> func = (v) =>
            {
                if (v.Game.CurrentLevel == null)
                    return;

                var level = v.Game.CurrentLevelIndex;
                var money = v.Game.Player.Money;
                var med = v.Game.Player.MedecineCount;
                var health = v.Game.Player.Health;
                var weapon = v.Game.Player.Weapon.Name;
                var enemies = v.Game.CurrentLevel.Entities.Count(e => e is Enemy);
                var targetsCollected = v.Game.CurrentLevel.CollectedTargetsCount;
                var targetsTotal = v.Game.CurrentLevel.Targets.Count();
                var data = new SaveData(level, money, health, med, weapon, enemies, targetsCollected, targetsTotal);
                SaveLoadManager.SavePlayerData(data, filename);
                //v.MenuState = MenuState.SaveMenu;
            };
            return func;

        }
    }
}
