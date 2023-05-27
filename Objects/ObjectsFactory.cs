using Abyss.ContentClasses;
using Abyss.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Abyss.Objects
{
    public static class ObjectsFactory
    {
        private static Dictionary<CellState, Texture2D> objectsImages = new Dictionary<CellState, Texture2D>()
        {
            {CellState.Grass, Arts.Grass },
            {CellState.Bricks, Arts.Bricks },
            {CellState.Water, Arts.Water },
            {CellState.Gravel, Arts.Gravel },
            {CellState.Dirt, Arts.Dirt },
            {CellState.Box, Arts.Box },
            {CellState.Sandstone, Arts.Sandstone },
            {CellState.Empty, Arts.Grass },

        };

        public static GameObject CreateNewObject(CellState cellState, Vector2 position)
        {
            return new GameObject(objectsImages[cellState], cellState, position);
        }
    }
}
