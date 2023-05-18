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
            var m = map.Split('\n');
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
    }
}
