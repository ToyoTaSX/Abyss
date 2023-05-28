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
using System.CodeDom;

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
            var clr = new Color(143, 8, 121);
            var colorState = new Dictionary<Color, CellState>()
            {
                // R G B A

                // Фиолетовый
                { new Color(143, 8, 121), CellState.Box},

                // Черный - стены
                { new Color(0, 0, 0), CellState.Bricks},

                // Темно-серый - стены
                { new Color(95, 95, 95), CellState.Cement},
                
                // Тёмно-коричнеый
                { new Color(136, 75, 49), CellState.Dirt},
                
                // Зеленый
                { new Color(79, 141, 43), CellState.Grass},
                
                // Желтый
                { new Color(255, 242, 0), CellState.Sand},
                
                // Светло-серый
                { new Color(195, 195, 195), CellState.Stone},
                
                // Красный
                { new Color(255, 0, 0), CellState.Tiles},
                
                // Светло-коричнеый
                { new Color(185, 122, 86), CellState.WoodPlanks},
                
                // Синий - непроходима
                { new Color(0, 0, 255), CellState.Water},
            };
            var colors = ToDoubleArray(Image);
            var result = new CellState[colors.GetLength(0), colors.GetLength(1)];
            for (int x = 0; x <  colors.GetLength(0); x++)
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    var p = colors[x, y];
                    //var state = colorState.MinBy(kv => kv.Key.DistanceTo(p)).Value;
                    if (colorState.TryGetValue(p, out var state))
                        result[x, y] = state;
                    else
                        result[x, y] = CellState.Grass;
                }
            return new Map(result);
        }

        private static Color[,] ToDoubleArray(Texture2D image)
        {
            var width = image.Width;
            var height = image.Height;
            var colorData = new Color[width * height];
            image.GetData(colorData);
            var result = new Color[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = colorData[y * width + x];
                }
            return result;
        }
    }
}
