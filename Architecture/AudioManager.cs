using Abyss.ContentClasses;
using Abyss.Gui;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    public class AudioManager
    {
        private GameModel game;
        private GameState lastGameState;


        public AudioManager(GameModel game)
        {
            this.game = game;
            lastGameState = game.State;
        }

        public void Update()
        {
            if (game.State == lastGameState)
                return;

            switch (game.State)
            {
                case GameState.Running:
                    MediaPlayer.Play(Audios.MainTheme); break;
                case GameState.Menue:
                    MediaPlayer.Play(Audios.MainMenu); break;
                case GameState.Trading:
                    MediaPlayer.Play(Audios.TradeMenu); break;
            }
            lastGameState = game.State;

        }
    }
}
