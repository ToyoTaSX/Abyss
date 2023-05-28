using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Weapons;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace Abyss.Gui
{
    public class LoadMenu : Menu
    {
        public LoadMenu(int width, int heigth) : base(width, heigth)
        {
        }

        public override void OnCreate()
        {
            State = MenuState.LoadMenu;
            Background = Arts.LoadBackground;

            var leftCenter = Width / 2 - Arts.ButtonActive.Width / 2;
            var padding = new Vector2(0, 120);

            var pos = new Vector2(leftCenter, 200);
            for (int i = 1; i <= 5; i++)
            {
                var f = CreateLoadFunction($"save{i}");
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
        { }

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

        private Action<View> CreateLoadFunction(string filename)
        {
            Action<View> func = (v) =>
            {
                var data = SaveLoadManager.LoadPlayerData(filename);
                if (data == null)
                    return;

                v.Game.Player.Money = data.Money;
                v.Game.Player.Health = data.Health;
                v.Game.Player.Weapon = WeaponsFactory.CreateWeapon(data.Weapon, new List<Type> { typeof(Enemy) });
                v.Game.CurrentLevelIndex = data.Level;
                v.Game.Player.MedecineCount = data.MedecineCount;
                v.MenuState = MenuState.MainMenu;
                v.Game.LevelLoadTask = Task.Run(() => v.Game.Levels[v.Game.CurrentLevelIndex](v.Game.Player, data.EnemiesCount, data.TotalTargets, data.CollectedTargets));
                v.Game.State = GameState.Loading;
            };
            return func;

        }
    }
}
