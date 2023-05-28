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

namespace Abyss.Architecture
{
    public class Level
    {
        public Vector2 StartPos { get; set; }
        public Player Player { get; }

        public StartPosition StartPosition { get; private set; }
        public readonly List<Target> Targets;
        private int _targetsCount;
        public int CollectedTargetsCount { get => Targets.Count(t => t.IsCollected); }
        public bool IsAllTargetsCollected { get => CollectedTargetsCount == _targetsCount; }
        public bool IsPassed { get => StartPosition.IsPassed && IsAllTargetsCollected; }

        public readonly List<GameObject> Objects;

        private int _enemiesCount;
        public readonly List<Entity> Entities;
        public readonly HashSet<Entity> EntAddList;
        public readonly HashSet<Entity> EntRemoveList;

        public readonly List<Bullet> Bullets;
        public readonly HashSet<Bullet> BullAddList;
        public readonly HashSet<Bullet> BullRemoveList;

        private int _playerHealthOnStart;
        private int _playerMedicineOnStart;
        private int _playerMoneyOnStart;

        public readonly Map LevelMap;

        public Level(Player player, Map map, int targetsCount, int enemiesCount, int targetsCollected)
        {
            Player = player;
            LevelMap = map;
            Targets = new List<Target>();

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

            _targetsCount = targetsCount;
            _enemiesCount = enemiesCount;

            _playerHealthOnStart = Player.Health;
            _playerMedicineOnStart = Player.MedecineCount;
            _playerMoneyOnStart = Player.Money;
            GenerateLevel(targetsCollected);
        }

        private void GenerateLevel(int targetsCollected)
        {
            var emptyPos = new List<Vector2>();
            for (int x = 0; x < LevelMap.Width; x++)
                for (int y = 0; y < LevelMap.Height; y++)
                {
                    if (Map.EmtyStates.Contains(LevelMap[x, y]))
                        emptyPos.Add(Map.ToAbsPosition(new Point(x, y)));

                    Objects.Add(ObjectsFactory.CreateNewObject(LevelMap[x, y], Map.ToAbsPosition(new Point(x, y))));
                }
            CreateBorders(LevelMap, CellState.Cement);
            
            var rnd = new Random();
            StartPos = emptyPos[rnd.Next(emptyPos.Count)];
            Player.Position = StartPos;
            StartPosition = new StartPosition(StartPos - new Vector2(32, 32));
            CreateEnemies(emptyPos);
            CreateTargets(LevelMap.PossibleTargets.Select(p => Map.ToAbsPosition(p)).ToList());
            for (int i = 0; i < Math.Min(Targets.Count, targetsCollected); i++)
            {
                Targets[i].IsCollected = true;
            }
        }

        public void RestartLevel()
        {
            var emptyPos = GetEmptyPositions();

            Entities.Clear();
            Entities.Add(Player);
            Targets.Clear();
            Player.Position = StartPos;
            StartPosition = new StartPosition(StartPos - new Vector2(32, 32));
            Player.Health = _playerHealthOnStart;
            Player.MedecineCount = _playerMedicineOnStart;
            Player.Money = _playerMoneyOnStart;
            CreateEnemies(emptyPos);
            CreateTargets(LevelMap.PossibleTargets.Select(p => Map.ToAbsPosition(p)).ToList());
        }

        private List<Vector2> GetEmptyPositions()
        {
            var emptyPos = new List<Vector2>();
            for (int x = 0; x < LevelMap.Width; x++)
                for (int y = 0; y < LevelMap.Height; y++)
                    if (Map.EmtyStates.Contains(LevelMap[x, y]))
                        emptyPos.Add(Map.ToAbsPosition(new Point(x, y)));
            return emptyPos;
        }

        private void CreateEnemies(List<Vector2> emptyPos)
        {
            var rnd = new Random();
            var weapons = new[] { WeaponName.Revolver, WeaponName.DP28, WeaponName.MP40, WeaponName.Mosin };
            emptyPos = emptyPos.Where(v => v.DistanceSquared(Player.Position) > 32 * 32 * 10 * 10).ToList();
            for (int i = 0; i < Math.Min(_enemiesCount, emptyPos.Count / 4); i++)
            {
                var pos = rnd.Next(emptyPos.Count());
                Entities.Add(new Reider(emptyPos[pos], weapons[rnd.Next(weapons.Count())]));
                emptyPos.RemoveAt(pos);
            }
        }

        private void CreateTargets(List<Vector2> targetsPos)
        {
            var rnd = new Random();
            targetsPos = targetsPos.Where(v => v.DistanceSquared(Player.Position) > 32 * 32 * 15 * 15).ToList();
            _targetsCount = Math.Min(_targetsCount, targetsPos.Count);
            for (int i = 0; i < _targetsCount; i++)
            {
                var pos = rnd.Next(targetsPos.Count());
                Targets.Add(new Target(targetsPos[pos]));
                targetsPos.RemoveAt(pos);
            }
        }
        private void CreateBorders(Map map, CellState obj)
        {
            // Лево-Право
            for (int i = -1; i <= map.Height; i ++)
            {
                var left = new Point(-1, i);
                var right = new Point(map.Width, i);
                Objects.Add(ObjectsFactory.CreateNewObject(obj, Map.ToAbsPosition(left)));
                Objects.Add(ObjectsFactory.CreateNewObject(obj, Map.ToAbsPosition(right)));
            }
            // Верх-Низ
            for (int i = -1; i <= map.Width; i++)
            {
                var top = new Point(i, -1);
                var bottom = new Point(i, map.Height);
                Objects.Add(ObjectsFactory.CreateNewObject(obj, Map.ToAbsPosition(top)));
                Objects.Add(ObjectsFactory.CreateNewObject(obj, Map.ToAbsPosition(bottom)));
                Objects.Add(ObjectsFactory.CreateNewObject(obj, Map.ToAbsPosition(bottom + new Point(0, 1))));
            }

        }

        public void Update(GameModel game)
        {
            EntAddList.Clear();
            BullAddList.Clear();
            EntRemoveList.Clear();
            BullRemoveList.Clear();
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

            foreach (var target in Targets)
                target.Update(game);

            StartPosition.Update(game);

            foreach (var entity in EntAddList)
                Entities.Add(entity);
            foreach (var bullet in BullAddList)
                Bullets.Add(bullet);
            foreach (var bullet in BullRemoveList)
                Bullets.Remove(bullet);
            foreach (var entity in EntRemoveList)
            {
                if (entity is Player)
                    RestartLevel();
                else
                    Entities.Remove(entity);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obj in Objects)
            {
                obj.Draw(spriteBatch);
            }

            foreach (var bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }

            foreach (var target in Targets)
                target.Draw(spriteBatch);

            StartPosition.Draw(spriteBatch);

            foreach (var entity in Entities)
            {
                entity.Draw(spriteBatch);
            }

        }
    }
}
