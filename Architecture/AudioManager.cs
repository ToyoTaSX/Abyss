using Abyss.ContentClasses;
using Microsoft.Xna.Framework.Media;

namespace Abyss.Architecture
{
    public class AudioManager
    {
        private GameModel _game;
        private GameState _lastGameState;

        public AudioManager(GameModel game)
        {
            this._game = game;
            _lastGameState = game.State;
        }

        public void Update()
        {
            if (_game.State == _lastGameState)
                return;

            switch (_game.State)
            {
                case GameState.Running:
                    MediaPlayer.Play(Audios.MainTheme); break;
                case GameState.Menue:
                    MediaPlayer.Play(Audios.MainMenu); break;
                case GameState.Trading:
                    MediaPlayer.Play(Audios.TradeMenu); break;
            }
            _lastGameState = _game.State;

        }
    }
}
