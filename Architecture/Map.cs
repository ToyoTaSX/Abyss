using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    class Map
    {
        private readonly CellState[,] mapCells;
        public readonly int Width;
        public readonly int Height;

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
        }
    }

    public enum CellState
    {
        Empty,
        Grass
    }
}
