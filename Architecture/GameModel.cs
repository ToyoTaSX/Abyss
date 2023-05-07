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
        public int CurrentLevel { get; set; }
        public GameModel()
        {
            Player = new Player();
            Levels.Add(new Level("level1", 1, Player));
            Levels.Add(new Level("level2", 2, Player));
            Levels.Add(new Level("level3", 3, Player));
        }

        public void Update()
        {
            if (CurrentLevel == Levels.Count)
                return;

            Levels[CurrentLevel].Update();
            if (Levels[CurrentLevel].IsPassed)
            {
                CurrentLevel += 1;
                if (CurrentLevel < Levels.Count)
                    Player.Position = Levels[CurrentLevel].StartPos;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentLevel == Levels.Count)
                return;

            Levels[CurrentLevel].Draw(spriteBatch);
        }
    }
}
