using Abyss.Architecture;
using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Abyss.Gui
{
    public class DeadMenu : Menu
    {
        public DeadMenu(int width, int heigth) : base(width, heigth)
        {
        }

        public override void OnCreate()
        {
            State = MenuState.DeadMenu;

            var leftCenter = Width / 2 - Arts.ButtonActive.Width / 2;

            var resumeAction = new Action<View>((v) => 
            {
                v.Game.CurrentLevel.RestartLevel();
                v.Game.State = GameState.Loading; 
            });
            var resumeButton = new Button(resumeAction, leftCenter, Height - 200, 400, 100, "Заново");
            Buttons.Add(resumeButton);
        }

        public override void OnResume()
        { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new Rectangle(0, 0, Width, Height), Color.Black);
            spriteBatch.DrawStrCentered(new Rectangle(0, 0, Width, Height), "ПОМЕР", Vector2.Zero, Color.Red, 4);
            foreach (var button in Buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
