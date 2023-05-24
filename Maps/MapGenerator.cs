using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;

namespace Abyss.Maps
{
    static class MapGenerator
    {
        private static Dictionary<char, CellState> charStates = new Dictionary<char, CellState>()
        {
            {' ', CellState.Empty },
            {'G', CellState.Grass },
        };
        public static Map CreateMapFromString(string map)
        {
            var m = map.Split("\n");
            var newMap = m
                    .Select(arr => arr
                                .Select(c => charStates.TryGetValue(c, out var res) ? res : CellState.Empty)
                                .ToArray())
                    .ToArray();
            var width = newMap.Max(s => s.Count());
            var height = newMap.Count();
            var result = new CellState[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    result[x, y] = newMap[y][x];
            return new Map(result);
        }

        public static Map CreateEmptyMap(int width, int height)
        {
            var mapArr = new CellState[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    mapArr[x, y] = CellState.Empty;
            for (int x = 10; x <= 15; x++)
            {
                mapArr[x, 5] = CellState.Grass;
                mapArr[x, 10] = CellState.Grass;
                mapArr[10, x - 5] = CellState.Grass;
                mapArr[15, x - 5] = CellState.Grass;
            }
            var ypos = 10;
            for (int x = 20; x < 30; x++)
            {
                mapArr[x, ypos - 2] = CellState.Grass;
                mapArr[x, ypos + 1] = CellState.Grass;
                ypos++;
            }
            mapArr[15, 7] = CellState.Empty;


            return new Map(mapArr);

        }
    }

    public class MazeGenerator
    {
        private int width;
        private int height;
        private CellState[,] maze;
        private bool[,] visited;

        public CellState[,] GenerateMaze(int mapWidth, int mapHeight)
        {
            width = mapWidth;
            height = mapHeight;

            maze = new CellState[width, height];
            visited = new bool[width, height];

            // Заполняем карту пустыми ячейками
            InitializeMaze();

            // Генерируем лабиринт
            Generate(0, 0);

            return maze;
        }

        private void InitializeMaze()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y] = CellState.Grass;
                    visited[x, y] = false;
                }
            }
        }

        private void Generate(int x, int y)
        {
            visited[x, y] = true;
            maze[x, y] = CellState.Empty;

            List<int> directions = new List<int> { 0, 1, 2, 3 };
            directions = ShuffleList(directions);

            foreach (int direction in directions)
            {
                int nx = x;
                int ny = y;

                if (direction == 0) // Вверх
                {
                    ny -= 2;
                    if (ny < 0)
                        continue;
                }
                else if (direction == 1) // Вправо
                {
                    nx += 2;
                    if (nx >= width)
                        continue;
                }
                else if (direction == 2) // Вниз
                {
                    ny += 2;
                    if (ny >= height)
                        continue;
                }
                else if (direction == 3) // Влево
                {
                    nx -= 2;
                    if (nx < 0)
                        continue;
                }

                if (visited[nx, ny])
                    continue;

                maze[nx, ny] = CellState.Empty;

                int mx = x + (nx - x) / 2;
                int my = y + (ny - y) / 2;
                maze[mx, my] = CellState.Empty;

                Generate(nx, ny);
            }
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            Random random = new Random();

            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = random.Next(i, list.Count);
                T temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }

            return list;
        }
    }
}
