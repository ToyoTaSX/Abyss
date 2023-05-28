using Abyss.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    public class GameModel
    {
        public List<Func<Player, int?, int?, int?, Level>> Levels = new List<Func<Player, int?, int?, int?, Level>>();
        public readonly Player Player;
        public readonly Input GameInput;
        public Level CurrentLevel = null;
        public int CurrentLevelIndex = 0;
        public Task<Level> LevelLoadTask;
        public GameState State;
        public int FramesToStart;
        public GameModel(Camera camera, Input input)
        {
            Player = new Player();
            input.Player = Player;
            GameInput = input;
            Levels.Add(CreatedLevels.GetLevel1);
            Levels.Add(CreatedLevels.GetLevel2);
            Levels.Add(CreatedLevels.GetLevel3);
            Levels.Add(CreatedLevels.GetLevel4);
            Levels.Add(CreatedLevels.GetLevel5);
        }

        public void Update()
        {
            if (State == GameState.Ended)
                return;
            if (LevelLoadTask != null && !LevelLoadTask.IsCompleted)
                return;
            if (LevelLoadTask != null && LevelLoadTask.IsCompleted && State == GameState.Loading)
            {
                CurrentLevel = LevelLoadTask.Result;
                FramesToStart = 60;
                State = GameState.Running;
                return;
            }

            if (CurrentLevel == null || CurrentLevel.IsPassed)
            {
                LevelLoadTask = Task.Run(() => Levels[CurrentLevelIndex](Player, null, null, null));
                State = GameState.Loading;
                return;
            }

            if (FramesToStart > 0)
            {
                FramesToStart--;
                return;
            }

            CurrentLevel.Update(this);
            if (CurrentLevel.IsPassed)
            {
                CurrentLevelIndex++;
                if (CurrentLevelIndex >= Levels.Count)
                {
                    State = GameState.Ended;
                    return;
                }
                State = GameState.Trading;
                return;
            }
            if (Player.IsExpired())
                State = GameState.Dead;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);
        }
    }
}
