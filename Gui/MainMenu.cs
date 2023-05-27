using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss.Architecture;
using Abyss.ContentClasses;

namespace Abyss.Gui
{
    public class MainMenu : Menu
    {
        public MainMenu(int width, int heigth) : base(width, heigth)
        { }

        public override void OnCreate()
        {
            State = MenuState.MainMenu;
            Background = Arts.MainBackground;

            var leftCenter = Width / 2 - Arts.ButtonActive.Width / 2;
            var paddingY = 20;

            var resumeAction = new Action<View>((v) => { v.Game.State = GameState.Loading; });
            var resumeButton = new Button(resumeAction, leftCenter, 200, 400, 100, "");
            Buttons.Add(resumeButton);

            var loadAction = new Action<View>((v) => { v.MenuState = MenuState.LoadMenu; });
            var loadButton = new Button(loadAction, leftCenter, resumeButton.Bottom + paddingY, 400, 100, "Загрузка");
            Buttons.Add(loadButton);

            var saveAction = new Action<View>((v) => { v.MenuState = MenuState.SaveMenu; });
            var saveButton = new Button(saveAction, leftCenter, loadButton.Bottom + paddingY, 400, 100, "Сохранение");
            Buttons.Add(saveButton);

            var exitAction = new Action<View>((v) => { Environment.Exit(0); });
            var exitButton = new Button(exitAction, leftCenter, saveButton.Bottom + 100, 400, 100, "Выход");
            Buttons.Add(exitButton);

        }

        public override void Update(View view)
        {
            var text = view.Game.CurrentLevel == null ? "Новая игра" : "Продолжить";
            Buttons[0].Text = text;
            Buttons[2].IsActive = view.Game.CurrentLevel != null;
            base.Update(view);
        }

        public override void OnResume()
        {
            return;
        }
    }
}
