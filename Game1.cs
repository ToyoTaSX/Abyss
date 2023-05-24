using Abyss.ContentClasses;
using Abyss.Architecture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Abyss.Entities;

namespace Abyss
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameModel _gameModel;
        private Camera _camera;
        private GameHud _hud;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Arts.Load(Content);
            WeaponImages.Load(Content);
            _camera = new Camera(new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), 2.2f);
            _gameModel = new GameModel(_camera);
            _hud = new GameHud(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _camera.Follow(_gameModel.Player, _gameModel.CurrentLevel.LevelMap);
            _gameModel.Update();
            _hud.Update(_gameModel);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: _camera.Transform);
            _gameModel.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
            //_spriteBatch.Begin();
            //_spriteBatch.DrawString(Arts.Font, "Camera " + _camera.CenterPosition.ToString(), new Vector2(100, 100), Color.Black);
            //_spriteBatch.DrawString(Arts.Font, "Player " + _gameModel.Player.CenterPosition.ToString(), new Vector2(100, 200), Color.Black);
            //_spriteBatch.DrawString(Arts.Font, "Mouse " + _gameModel.GameInput.MousePosition.ToString(), new Vector2(100, 300), Color.Black);
            //_spriteBatch.DrawString(Arts.Font, "Direction " + _gameModel.GameInput.GetAimDirection().ToString(), new Vector2(100, 400), Color.Black);
            //_spriteBatch.End();
            _spriteBatch.Begin();
            _hud.Draw(_spriteBatch);
            _spriteBatch.End();

        }
    }
}