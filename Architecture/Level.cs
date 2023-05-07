using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    internal class Level
    {
        public string Name { get; }
        public int Id { get; }
        public Vector2 StartPos { get; }
        public Vector2 EndPos { get; }
        public Player Player { get; }
        public bool IsPassed = false;
        private List<Entity> entities;
        private Input input;

        public Level(string name, int id, Player player)
        {
            Name = name;
            Id = id;
            Player = player;
            StartPos = new Vector2(0f, 0f);
            EndPos = new Vector2(300f, 0f);
            entities = new List<Entity>
            {
                Player
            };
            input = new Input(Player);
        }

        public void Update()
        {
            input.Update();
            foreach (var entity in entities)
            {
                entity.Update(input);
            }

            if (Player.Position == EndPos)
                IsPassed = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
