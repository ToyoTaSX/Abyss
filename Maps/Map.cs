using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;
using System.Net.Sockets;

namespace Abyss.Maps
{
    public class Map
    {
        private readonly CellState[,] mapCells;
        public Dictionary<Point, Dictionary<Point, Point>> pointToPoint;
        public readonly int Width;
        public readonly int Height;

        public HashSet<CellState> EmtyStates = new HashSet<CellState>()
        { CellState.Empty, };

        public CellState this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    throw new ArgumentOutOfRangeException();
                return mapCells[x, y];
            }
        }

        public Map(CellState[,] mapCells)
        {
            this.mapCells = mapCells;
            Width = mapCells.GetLength(0);
            Height = mapCells.GetLength(1);
            mapCells[2, 2] = CellState.Empty;
            GeneratePathTable();
        }

        public Vector2 GetNextPosition(Vector2 startPos, Vector2 endPos)
        {
            var point1 = ToMapPosition(startPos);
            var point2 = ToMapPosition(endPos);
            var direction = Point.Zero;
            if (pointToPoint.TryGetValue(point1, out var dict))
                if (dict.TryGetValue(point2, out var result))
                    direction = result;
            var nextPos = point1 + direction;
            return ToAbsPosition(nextPos);
        }

        #region Path Table Generation
        private void GeneratePathTable()
        {
            pointToPoint = new Dictionary<Point, Dictionary<Point, Point>>();
            var tasks = new List<Task<(Point StartPos, Dictionary<Point, Point> Pathes)>>();
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    var point = new Point(x, y);
                    if (!EmtyStates.Contains(mapCells[x, y]))
                        continue;
                    var task = Task.Run(() => FindPaths(point));
                    tasks.Add(task);
                    //var result = FindPaths(point);
                    //if (result != null && result.Count > 0)
                    //    pointToPoint[point] = result;
                }
            Task.WaitAll(tasks.ToArray());
            foreach (var task in tasks)
            {
                if (task.Result.Pathes != null && task.Result.Pathes.Count > 0)
                    pointToPoint[task.Result.StartPos] = task.Result.Pathes;
            }
        }

        private (Point StartPos, Dictionary<Point, Point> Pathes) FindPaths(Point startPos)
        {
            if (!EmtyStates.Contains(mapCells[startPos.X, startPos.Y]))
                return (startPos, null);

            var firstSteps = new Dictionary<Point, Point>();
            var opt = new double[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    opt[x, y] = double.MaxValue;
                }

            opt[startPos.X, startPos.Y] = 0;

            var visited = new HashSet<Point>() { startPos };
            var queue = new Queue<Point>();
            queue.Enqueue(startPos);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                foreach (var newPoint in GetNeighboringPoints(point))
                {
                    if (visited.Contains(newPoint))
                        continue;
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                    var pointCameFrom = GetNeighboringPoints(newPoint)
                                                    .Select(p => (Point: p, Value: opt[p.X, p.Y] + newPoint.DistanceTo(p)))
                                                    .MinBy(t => t.Value);
                    opt[newPoint.X, newPoint.Y] = pointCameFrom.Value;
                    if (pointCameFrom.Point == startPos)
                        firstSteps[newPoint] = startPos.DirectionTo(newPoint);
                    else
                        firstSteps[newPoint] = firstSteps[pointCameFrom.Point];
                }
            }
            return (startPos, firstSteps);
        }

        private IEnumerable<Point> GetNeighboringPoints(Point point)
        {
            foreach (var delta in StraightDirections)
            {
                var newPoint = point + delta;
                if (InBounds(newPoint) && EmtyStates.Contains(this[newPoint.X, newPoint.Y]))
                    yield return newPoint;
            }

            foreach (var delta in DiagDirections)
            {
                var newPoint = point + delta;
                var point1 = newPoint;
                point1.X -= delta.X;
                var point2 = newPoint;
                point2.Y -= delta.Y;

                if (InBounds(newPoint) &&
                    EmtyStates.Contains(this[newPoint.X, newPoint.Y]) &&
                    EmtyStates.Contains(this[point1.X, point1.Y]) &&
                    EmtyStates.Contains(this[point2.X, point2.Y]))
                    yield return newPoint;
            }

        }

        private Point[] StraightDirections = new[]
        {
            new Point(0, 1),
            new Point(1, 0),
            new Point(-1, 0),
            new Point(0, -1),
        };

        private Point[] DiagDirections = new[]
        {
            new Point(1, 1),
            new Point(-1, -1),
            new Point(-1, 1),
            new Point(1, -1)
        };
        #endregion

        public bool InBounds(Point point)
        {
            return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        }

        public static Point ToMapPosition(Vector2 position)
        {
            var ceilX = (int)(position.X / 32);
            var ceilY = (int)(position.Y / 32);
            return new Point(ceilX, ceilY);

        }

        public static Vector2 ToAbsPosition(Point position)
        {
            return new Vector2(position.X, position.Y) * 32;
        }
    }

    public enum CellState
    {
        Empty,
        Grass
    }
}
