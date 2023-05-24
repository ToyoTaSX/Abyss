using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Maps;
using Abyss.Objects;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Level
    {
        public Vector2 StartPos { get; }
        public Player Player { get; }
        public bool IsPassed = false;
        public readonly List<GameObject> Objects;
        public readonly List<Entity> Entities;
        public readonly HashSet<Entity> EntAddList;
        public readonly HashSet<Entity> EntRemoveList;
        public readonly List<Bullet> Bullets;
        public readonly HashSet<Bullet> BullAddList;
        public readonly HashSet<Bullet> BullRemoveList;
        public readonly Map LevelMap;

        public Level(Player player, Map map)
        {
            Player = player;
            LevelMap = map;
            Objects = new List<GameObject>();
            Entities = new List<Entity>
            {
                Player,
            };
            EntAddList = new HashSet<Entity>();
            EntRemoveList = new HashSet<Entity>();
            Bullets = new List<Bullet>();
            BullAddList = new HashSet<Bullet>();
            BullRemoveList = new HashSet<Bullet>();

            var emptyPos = new List<Vector2>();
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    if (map[x, y] == CellState.Empty)
                    {
                        emptyPos.Add(Map.ToAbsPosition(new Point(x, y)));
                        continue;
                    }

                    Objects.Add(ObjectsFactory.CreateNewObject(map[x, y], Map.ToAbsPosition(new Point(x, y))));
                }
            CreateBorders(map);
            Entities.Add(new Reider(StartPos));
            player.Position = StartPos;
        }

        private void CreateBorders(Map map)
        {
            // Лево-Право
            for (int i = -1; i <= map.Height; i ++)
            {
                var left = new Point(-1, i);
                var right = new Point(map.Width, i);
                Objects.Add(ObjectsFactory.CreateNewObject(CellState.Grass, Map.ToAbsPosition(left)));
                Objects.Add(ObjectsFactory.CreateNewObject(CellState.Grass, Map.ToAbsPosition(right)));
            }
            // Верх-Низ
            for (int i = -1; i <= map.Width; i++)
            {
                var top = new Point(i, -1);
                var bottom = new Point(i, map.Height);
                Objects.Add(ObjectsFactory.CreateNewObject(CellState.Grass, Map.ToAbsPosition(top)));
                Objects.Add(ObjectsFactory.CreateNewObject(CellState.Grass, Map.ToAbsPosition(bottom)));
            }

        }

        public void Update(GameModel game)
        {
            EntAddList.Clear();
            BullAddList.Clear();
            game.GameInput.Update();
            foreach (var entity in Entities)
            {
                entity.Update(game);
                if (entity.IsExpired())
                {
                    entity.OnExpire(game);
                    EntRemoveList.Add(entity);
                }
            }

            foreach (var bullet in Bullets)
            {
                bullet.Update(game);
                if (bullet.IsExpired())
                {
                    bullet.OnExpire(game);
                    BullRemoveList.Add(bullet);
                }
            }

            foreach (var entity in EntAddList)
                Entities.Add(entity);
            foreach (var entity in EntRemoveList)
                Entities.Remove(entity);
            foreach (var bullet in BullAddList)
                Bullets.Add(bullet);
            foreach (var bullet in BullRemoveList)
                Bullets.Remove(bullet);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }
            foreach (var obj in Objects)
            {
                obj.Draw(spriteBatch);
            }

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
