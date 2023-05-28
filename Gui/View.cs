using Abyss.Architecture;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Abyss.Gui
{
    public class View
    {
        public List<Menu> Menues;
        public GameModel Game;
        private MenuState _state;
        public MenuState MenuState
        {
            get
            { return _state; }
            set
            {
                _state = value;
                CurrentMenu.OnResume();
            }
        }
        public Input Input { get => Game.GameInput; }
        public Menu CurrentMenu { get => Menues.Find(m => m.State == MenuState); }

        public View(GameModel game, List<Menu> menues, MenuState startMenu)
        {
            Game = game;
            Menues = menues;
            MenuState = startMenu;
        }

        public void Update()
        {
            CurrentMenu.Update(this);
        }

        public void Update(MenuState state)
        {
            if (_state != state) 
                _state = state;
            CurrentMenu.Update(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMenu.Draw(spriteBatch);
        }

    }
}
