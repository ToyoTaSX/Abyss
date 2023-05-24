using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss.Entities;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;

namespace Abyss.Architecture
{
    public class Input
    {
        private KeyboardState keyboardState, lastKeyboardState;
        private MouseState mouseState, lastMouseState;
        public bool IsLmbDown { get => mouseState.LeftButton == ButtonState.Pressed || lastMouseState.LeftButton == ButtonState.Pressed; }
        public bool IsRmbDown { get => mouseState.RightButton == ButtonState.Pressed || lastMouseState.RightButton == ButtonState.Pressed; }
        public Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }
        public Player Player { get; }
        public Camera Camera { get; }

        public Input(Player player, Camera camera)
        { 
            Player = player; 
            Camera = camera;
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
            var cameraToMouse = MousePosition - Camera.MainWindowCenter;
            var playerToCamera = Camera.CenterPosition - Player.CenterPosition;
            var res = playerToCamera * Camera.Scale + cameraToMouse;
            res.Normalize();
            return res;
        }
    }
}
