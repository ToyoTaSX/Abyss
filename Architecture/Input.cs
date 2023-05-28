using Abyss.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Abyss.Architecture
{
    public class Input
    {
        private KeyboardState _keyboardState, _lastKeyboardState;
        private MouseState _mouseState, _lastMouseState;
        public bool IsLmbDown { get => _mouseState.LeftButton == ButtonState.Pressed || _lastMouseState.LeftButton == ButtonState.Pressed; }
        public bool IsRmbDown { get => _mouseState.RightButton == ButtonState.Pressed || _lastMouseState.RightButton == ButtonState.Pressed; }
        public Vector2 MousePosition { get { return new Vector2(_mouseState.X, _mouseState.Y); } }
        public Player Player { get; set; }
        public Camera Camera { get; set; }

        public Input(Player player, Camera camera)
        { 
            Player = player; 
            Camera = camera;
        }

        public void Update()
        {
            _lastKeyboardState = _keyboardState;
            _lastMouseState = _mouseState;
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        public bool WasKeyPressed(Keys key)
        {
            return _lastKeyboardState.IsKeyUp(key) && _keyboardState.IsKeyDown(key);
        }

        public bool WasLMBPressed()
        {
            return _lastMouseState.LeftButton == ButtonState.Released && IsLmbDown;
        }

        public bool WasRMBPressed()
        {
            return _lastMouseState.RightButton == ButtonState.Released && IsRmbDown;
        }

        public Vector2 GetMovementDirection()
        {

            var direction = Vector2.Zero;
            if (_keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (_keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (_keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (_keyboardState.IsKeyDown(Keys.S))
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
