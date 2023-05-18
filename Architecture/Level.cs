using Abyss.Objects;
using Abyss.Architecture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss.Enemies;

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
            StartPos = new Vector2(4 * 32f + 16, 10 * 32f + 16);
            EndPos = new Vector2(3000f, 0f);
            Objects = new List<GameObject>();
            Entities = new List<Entity>
            {
                Player,
            };
            DeleteList = new List<Entity>();
            AddList = new List<Entity>();
            LevelMap = map;      
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    CreateObjectByState(new Vector2(x * 32 + 16, y * 32 + 16), map[x, y]);
            CreateBorders(1280, 720);
            Entities.Add(new Zombie());
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
                Objects.Add(new GrassObject(new Vector2(-16, i)));
                Objects.Add(new GrassObject(new Vector2(width + 16, i)));
            }
            // Верх-Низ
            for (int i = -32; i <= width + 32; i += 32)
            {
                Objects.Add(new GrassObject(new Vector2(i, -16)));
                Objects.Add(new GrassObject(new Vector2(i, heigth + 16)));
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
                    DeleteList.Add(entity);
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
        }
    }
}
