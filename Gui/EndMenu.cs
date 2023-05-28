using Abyss.Architecture;
using Abyss.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Gui
{
    public class EndMenu : Menu
    {
        private int framesToLive = 180;
        public EndMenu(int width, int heigth) : base(width, heigth)
        {
        }

        public override void OnCreate()
        {
            State = MenuState.End;
        }

        public override void OnResume()
        { }

        public override void Update(View view)
        {
            framesToLive--;
            if (framesToLive == 0)
            {
                view.MenuState = MenuState.MainMenu;
                view.Game.State = GameState.Menue;
                view.Game.CurrentLevel = null;
                view.Game.CurrentLevelIndex = 0;
                var p = view.Game.Player;
                var newP = new Player();
                p.Money = newP.Money;
                p.MedecineCount = newP.MedecineCount;
                p.Health = newP.Health;
                p.Weapon = newP.Weapon;
            }
            framesToLive = 180;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(0, 0, Width, Height);
            spriteBatch.DrawRectangle(rect, Color.Black);
            spriteBatch.DrawStrCentered(rect, "Спасибо за прохождение!", Vector2.Zero, Color.White, scale: 3);
        }
    }
}
