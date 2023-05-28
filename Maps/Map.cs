using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abyss.Maps
{
    public class Map
    {
        private readonly CellState[,] _mapCells;
        private Dictionary<Point, Dictionary<Point, Point>> _pointToPoint;
        public readonly List<Point> PossibleTargets;
        public readonly int Width;
        public readonly int Height;

        public static HashSet<CellState> EmtyStates = new HashSet<CellState>()
        {
            CellState.Empty,
            CellState.Dirt,
            CellState.Grass,
            CellState.Sand,
            CellState.Stone,
            CellState.Tiles,
            CellState.WoodPlanks,
        };

        public CellState this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    throw new ArgumentOutOfRangeException();
                return _mapCells[x, y];
            }
        }

        public Map(CellState[,] mapCells)
        {
            this._mapCells = mapCells;
            Width = mapCells.GetLength(0);
            Height = mapCells.GetLength(1);
            GeneratePathTable();
            PossibleTargets = GetPossibleTargets2x2().ToList();
        }

        public Vector2 GetNextPosition(Vector2 startPos, Vector2 endPos)
        {
            var point1 = ToMapPosition(startPos);
            var point2 = ToMapPosition(endPos);
            var direction = Point.Zero;
            if (_pointToPoint.TryGetValue(point1, out var dict))
                if (dict.TryGetValue(point2, out var result))
                    direction = result;
            var nextPos = point1 + direction;
            return ToAbsPosition(nextPos);
        }

        private IEnumerable<Point> GetPossibleTargets2x2()
        {
            var busyPoints = new HashSet<Point>();
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    var tl = new Point(x, y);
                    var tr = new Point(x + 1, y);
                    var bl = new Point(x, y + 1);
                    var br = new Point(x + 1, y + 1);
                    var points = new[] {tl, tr, bl, br };
                    if (points.Any(p => 
                                busyPoints.Contains(p) ||
                                !InBounds(p) ||
                                this[p.X, p.Y] != CellState.Box))
                        continue;
                    busyPoints.Add(tl);
                    busyPoints.Add(tr);
                    busyPoints.Add(bl);
                    busyPoints.Add(br);
                    yield return tl + new Point(-1, -1);
                }
        }

        #region Path Table Generation
        private void GeneratePathTable()
        {
            _pointToPoint = new Dictionary<Point, Dictionary<Point, Point>>();
            var tasks = new List<Task<(Point StartPos, Dictionary<Point, Point> Pathes)>>();
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    var point = new Point(x, y);
                    if (!EmtyStates.Contains(_mapCells[x, y]))
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
                    _pointToPoint[task.Result.StartPos] = task.Result.Pathes;
            }
        }

        private (Point StartPos, Dictionary<Point, Point> Pathes) FindPaths(Point startPos)
        {
            if (!EmtyStates.Contains(_mapCells[startPos.X, startPos.Y]))
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
        Box,
        Empty,
        Bricks,
        Cement,
        Dirt,
        Grass,
        Sand,
        Stone,
        Tiles,
        WoodPlanks,
        Water
    }
}
