using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Abyss.Gui
{
    public abstract class Menu
    {
        public List<Button> Buttons;
        public MenuState State;
        public readonly int Width;
        public readonly int Height;
        public Texture2D Background;
        public Dictionary<string, string> ExtraData;
        public Rectangle Rectangle { get => new Rectangle(0, 0, Width, Height); }

        public Menu(int width, int heigth) 
        {
            Width = width;
            Height = heigth;
            Buttons = new List<Button>();
            OnCreate();
        }

        public abstract void OnCreate();

        public abstract void OnResume();

        public virtual void Update(View view) 
        {
            foreach (var button in Buttons)
            {
                button.Update(view);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            foreach(var button in Buttons)
                button.Draw(spriteBatch);
        }
    }
}
