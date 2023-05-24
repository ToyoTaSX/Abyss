using Abyss.Entities;
using Abyss.Maps;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    public class GameModel
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
        public GameModel(Camera camera)
        {
            Player = new Player();
            GameInput = new Input(Player, camera);
            var mazeGenerator = new MazeGenerator();
            Levels.Add(new Level(Player, MapGenerator.CreateEmptyMap(50, 50)));
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
