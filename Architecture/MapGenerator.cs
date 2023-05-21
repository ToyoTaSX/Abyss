using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
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
            var mapArr = new CellState[width,height];
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
}
