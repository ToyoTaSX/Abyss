using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    class Input
    {
        private KeyboardState keyboardState, lastKeyboardState;
        private MouseState mouseState, lastMouseState;
        public Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }
        public Player Player { get; }

        public Input(Player player)
        { 
            Player = player; 
        }

        public void Update()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        public Vector2 GetMovementDirection()
        {

            var direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            if (direction.LengthSquared() > 1)
                direction.Normalize();
            return direction;
        }

        public Vector2 GetAimDirection()
        {
            Vector2 direction = MousePosition - this.Player.Position;
            if (direction == Vector2.Zero)
                return Vector2.Zero;
            else
                return Vector2.Normalize(direction);
        }
    }
}
