using Abyss.Architecture;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    internal class GameModel
    {
        public List<Level> Levels = new List<Level>();
        public readonly Player Player;
        public readonly Input GameInput;
        public Level CurrentLevel 
        {
            get { return Levels[_currentLevel]; } 
            set { _currentLevel = Levels.IndexOf(value); }
        }
        private int _currentLevel = 0;
        public GameModel()
        {
            Player = new Player();
            GameInput = new Input(Player);
            Levels.Add(new Level("level1", 1, Player, CreatedMaps.Map1));
            Player.Position = Levels[0].StartPos;
        }

        public void Update()
        {
            if (_currentLevel == Levels.Count)
                return;

            CurrentLevel.Update(this);
            if (CurrentLevel.IsPassed)
            {
                _currentLevel += 1;
                if (_currentLevel < Levels.Count)
                    Player.Position =CurrentLevel.StartPos;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentLevel == Levels.Count)
                return;

            Levels[_currentLevel].Draw(spriteBatch);
        }
    }
}
