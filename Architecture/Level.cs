using Abyss.Objects;
using Abyss.Architecture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss.Enemies;
using System;
using System.Diagnostics;

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
        public List<GameObject> Objects;
        public List<Entity> Entities;
        public List<Entity> DeleteList;
        public List<Entity> AddList;
        public readonly Map LevelMap;

        public Level(string name, int id, Player player, Map map)
        {
            Name = name;
            Id = id;
            Player = player;
            StartPos = new Vector2(32f * 12, 32f * 7);
            EndPos = new Vector2(3000f, 0f);
            Objects = new List<GameObject>();
            Entities = new List<Entity>
            {
                Player,
            };
            DeleteList = new List<Entity>();
            AddList = new List<Entity>();
            LevelMap = map;
            var emptyPos = new List<Vector2>();
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    if (map[x, y] == CellState.Empty)
                        emptyPos.Add(new Vector2(x, y) * 32);

                    CreateObjectByState(new Vector2(x * 32, y * 32), map[x, y]);
                }
            CreateBorders(1280, 720);
            var rnd = new Random();
            player.Position = StartPos;
            //Entities.Add(new Zombie(emptyPos[0]));
            for (int i = 0; i < emptyPos.Count / 5; i++)
            {
                //Entities.Add(new Zombie(emptyPos[rnd.Next(emptyPos.Count)]));
                Entities.Add(new Zombie(emptyPos[i]));
            }
        }

        private void CreateObjectByState(Vector2 pos, CellState cellState)
        {
            switch (cellState)
            {
                case CellState.Grass:
                    Objects.Add(new GrassObject(pos)); 
                    break;
            }
        }

        private void CreateBorders(int width, int heigth)
        {
            // Лево-Право
            for (int i = 0; i <= heigth + 32; i += 32)
            {
                Objects.Add(new GrassObject(new Vector2(-32, i)));
                Objects.Add(new GrassObject(new Vector2(width, i)));
            }
            // Верх-Низ
            for (int i = -32; i <= width + 32; i += 32)
            {
                Objects.Add(new GrassObject(new Vector2(i, -32)));
                Objects.Add(new GrassObject(new Vector2(i, heigth)));
            }

        }

        public void Update(GameModel game)
        {
            DeleteList.Clear();
            AddList.Clear();
            game.GameInput.Update();
            foreach (var entity in Entities)
            {
                if (entity.IsExpired())
                {
                    entity.OnExpire(game);
                    DeleteList.Add(entity);
                }
                else
                    entity.Update(game);
            }

            foreach (var entity in DeleteList)
                Entities.Remove(entity);
            foreach (var entity in AddList)
                Entities.Add(entity);

            if (Player.Position == EndPos)
                IsPassed = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
            foreach(var obj in Objects)
            {
                obj.Draw(spriteBatch);
            }
            spriteBatch.DrawString(Arts.Font, Entities.Count(e => e is Enemy).ToString(), new Vector2(100, 400), Color.Black);
        }
    }
}
