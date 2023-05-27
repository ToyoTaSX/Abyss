using Abyss.ContentClasses;
using Abyss.Architecture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Abyss.Entities;
using Abyss.Hud;
using Abyss.Gui;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using Microsoft.Xna.Framework.Media;

namespace Abyss
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameModel _gameModel;
        private Camera _camera;
        private GameHud _hud;
        private Input _input;
        private LoadingScreen _loadingScreen;
        private bool _isOnPause;
        private View _mainMenuView;
        private View _ingameView;
        private int width;
        private int height;
        private AudioManager audioManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = height = 1080;
            _graphics.PreferredBackBufferWidth = width = 1920;
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            var pathToRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            if (!Directory.Exists(Path.Combine(pathToRoot, "Abyss")))
            {
                Directory.CreateDirectory(Path.Combine(pathToRoot, "Abyss", "usersaves"));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Arts.Load(Content);
            WeaponImages.Load(Content);
            MapImages.Load(Content);
            Audios.Load(Content);
            SoundEffects.Load(Content);

            _camera = new Camera(new Point(width, height), 2.2f);
            _input = new Input(null, _camera);
            _gameModel = new GameModel(_camera, _input);
            _hud = new GameHud(width, height);
            _loadingScreen = new LoadingScreen();
            var menuesMain = new List<Menu>()
            {
                new MainMenu(width, height),
                new SaveMenu(width, height),
                new LoadMenu(width, height),
                new EndMenu(width, height)
            };
            _mainMenuView = new View(_gameModel, menuesMain, MenuState.MainMenu);

            var tradingMenues = new List<Menu>()
            {
                new EmptyMenue(width, height),
                new TradingMenu(width, height),
                new DeadMenu(width, height),
            };
            _ingameView = new View(_gameModel, tradingMenues, MenuState.Empty);

            MediaPlayer.Play(Audios.MainMenu);
            audioManager = new AudioManager(_gameModel);
        }

        protected override void Update(GameTime gameTime)
        {
            audioManager.Update();
            base.Update(gameTime);
            _input.Update();
            if (_input.WasKeyPressed(Keys.Escape) && _gameModel.State == GameState.Running)
                _gameModel.State = GameState.Menue;
            else if (_input.WasKeyPressed(Keys.Escape) && _gameModel.State == GameState.Menue && _mainMenuView.MenuState == MenuState.MainMenu)
                _gameModel.State = GameState.Loading;


            if (_gameModel.State == GameState.Menue)
            {
                _mainMenuView.Update();
            }

            if (_gameModel.State == GameState.Ended)
            {
                _mainMenuView.Update(MenuState.End);
            }

            if (_gameModel.State == GameState.Trading || _gameModel.State == GameState.Dead)
            {
                var state = _gameModel.State == GameState.Trading ? MenuState.TradeMenu : MenuState.DeadMenu;
                _ingameView.Update(state);
            }

            if (_gameModel.State == GameState.Running)
            {
                _gameModel.Update();
                _hud.Update(_gameModel);
                _camera.Follow(_gameModel.Player, _gameModel.CurrentLevel.LevelMap);
            }

            if (_gameModel.State == GameState.Loading)
            {
                _gameModel.Update();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            if (_gameModel.State == GameState.Loading)
            {
                _spriteBatch.Begin();
                _loadingScreen.Draw(_spriteBatch, width, height);
                _spriteBatch.End();
            }

            else if (_gameModel.State == GameState.Menue || _gameModel.State == GameState.Ended)
            {
                _spriteBatch.Begin();
                _mainMenuView.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            else if (_gameModel.State == GameState.Running)
            {
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                _gameModel.Draw(_spriteBatch);
                _spriteBatch.End();

                _spriteBatch.Begin();
                _hud.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            else if (_gameModel.State == GameState.Trading || _gameModel.State == GameState.Dead)
            {
                _spriteBatch.Begin();
                _ingameView.Draw(_spriteBatch);
                _spriteBatch.End();
            }

        }
    }
}