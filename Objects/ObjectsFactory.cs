using Abyss.ContentClasses;
using Abyss.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Abyss.Objects
{
    public static class ObjectsFactory
    {
        private static Dictionary<CellState, Func<Texture2D>> _objectsImages = new Dictionary<CellState, Func<Texture2D>>()
        {
            {CellState.Empty, () => Arts.Grass },
            {CellState.Box, () => Arts.Box },
            {CellState.Bricks, () =>  Arts.Bricks },
            {CellState.Cement, () => Arts.Cement },
            {CellState.Dirt, () => Arts.Dirt },
            {CellState.Grass, () => Arts.Grass },
            {CellState.Sand, () => Arts.Sand },
            {CellState.Stone, () => Arts.Stone },
            {CellState.Tiles, () => Arts.Tiles },
            {CellState.WoodPlanks, () => Arts.WoodPlanks },
            {CellState.Water, () => Arts.Water },
        };

        public static GameObject CreateNewObject(CellState cellState, Vector2 position)
        {
            return new GameObject(_objectsImages[cellState](), cellState, position);
        }
    }
}
