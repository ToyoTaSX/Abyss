using Abyss.Entities;
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
        public List<Entity> Entities;

        public Level(string name, int id, Player player)
        {
            Name = name;
            Id = id;
            Player = player;
            StartPos = new Vector2(400f, 200f);
            EndPos = new Vector2(3000f, 0f);
            Entities = new List<Entity>
            {
                Player,
                new Block(new Vector2(600, 100)),
                new Block(new Vector2(632, 100)),
                new Block(new Vector2(664, 100)),
                new Block(new Vector2(696, 100)),
                new Block(new Vector2(728, 100))
            };
            CreateBorders(1280, 720);
        }

        private void CreateBorders(int width, int heigth)
        {
            // Лево-Право
            for (int i = 0; i <= heigth + 32; i += 32)
            {
                Entities.Add(new Block(new Vector2(0, i)));
                Entities.Add(new Block(new Vector2(width - 32, i)));
            }
            // Верх-Низ
            for (int i = -32; i <= width + 32; i += 32)
            {
                Entities.Add(new Block(new Vector2(i, 0)));
                Entities.Add(new Block(new Vector2(i, heigth - 32)));
            }

        }

        public void Update(GameModel game)
        {
            game.GameInput.Update();
            foreach (var entity in Entities)
            {
                entity.Update(game);
            }

            if (Player.Position == EndPos)
                IsPassed = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
