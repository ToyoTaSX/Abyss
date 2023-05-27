using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
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
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Microsoft.Xna.Framework.Graphics;

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
            return new Map(mapArr);

        }

        public static Map CreateMapFromImage(Texture2D Image)
        {
            var colorState = new Dictionary<Color, CellState>()
            {
                { Color.Green, CellState.Grass},
                { Color.Brown, CellState.Bricks},
                { Color.Blue, CellState.Water },
                { Color.Gray, CellState.Gravel},
                { Color.Black, CellState.Dirt},
                { Color.Purple, CellState.Box },
                { Color.Yellow, CellState.Sandstone },
            };
            var width = Image.Width;
            var height = Image.Height;
            var colorData = new Color[width * height];
            Image.GetData<Color>(colorData);

            var result = new CellState[width, height];
            for (int x = 0; x <  width; x++)
                for (int y = 0; y < height; y++)
                {
                    var p = colorData[x * height + y];
                    //var v = new Vector4(p.R, p.G, p.B, p.A);
                    var pix = Color.FromNonPremultiplied(p.R, p.G, p.B, p.A);
                    var state = colorState.MinBy(kv => kv.Key.DistanceTo(pix)).Value;
                    result[x, y] = state;
                }
            return new Map(result);
        }
    }
}
