using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Gui
{
    public class EmptyMenue : Menu
    {
        public EmptyMenue(int width, int heigth) : base(width, heigth)
        {
        }

        public override void OnCreate()
        {
            State = MenuState.Empty;
        }

        public override void OnResume()
        { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new Rectangle(0, 0, Width, Height), Color.Black);
        }
    }
}
