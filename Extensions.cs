using Abyss.ContentClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Abyss
{
    public static class Extensions
    {
        public static (int XIntersection, int YIntersection, int Area) Intersection(this Rectangle r1, Rectangle r2)
        {
            var xOverlap = Math.Max(0, Math.Min(r1.X + r1.Width, r2.X + r2.Width) - Math.Max(r1.X, r2.X));
            var yOverlap = Math.Max(0, Math.Min(r1.Y + r1.Height, r2.Y + r2.Height) - Math.Max(r1.Y, r2.Y));
            return (xOverlap, yOverlap, xOverlap * yOverlap);
        }

        public static Point DirectionTo(this Point p1, Point p2)
        {;
            return p2 - p1;
        }

        public static double DistanceTo(this Point p1, Point p2)
        {
            var delta = p2 - p1;
            return Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
        }

        public static double DistanceTo(this Vector2 v1, Vector2 v2)
        {
            return Math.Sqrt(DistanceSquared(v1, v2));
        }

        public static double DistanceSquared(this Vector2 v1, Vector2 v2)
        {
            var delta = v2 - v1;
            return delta.X * delta.X + delta.Y * delta.Y;
        }

        public static IEnumerable<Vector2> GetPointsBetween(this Vector2 startPos, Vector2 endPos)
        {
            var dir = endPos - startPos;
            dir.Normalize();
            while (startPos.DistanceSquared(endPos) > dir.LengthSquared())
            {
                if ((endPos - startPos).LengthSquared() < dir.LengthSquared())
                    yield break;
                startPos += dir;
                yield return startPos;
            }
        }

        public static bool AlmostEqual(this Vector2 v1,  Vector2 v2)
        {
            var delta = v2 - v1;
            return delta.LengthSquared() < 1; ;
        }

        public static int DistanceTo(this Color color1, Color color2)
        {
            return Math.Abs(color1.R - color2.R) + Math.Abs(color1.G - color2.G) + Math.Abs(color1.B - color2.B);
        }

        public static void DrawStrCentered(this SpriteBatch spriteBatch, Rectangle rect, string text,  Vector2 offset, Color? color = null, int scale = 1)
        {
            var v = Arts.Font.MeasureString(text) * scale;
            var pos = rect.Center.ToVector2() - v / 2 + offset;
            var clr = color == null? Color.Black : (Color)color;
            spriteBatch.DrawString(Arts.Font, text, pos, clr, 0, Vector2.Zero, scale, 0, 0);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { color });
            spriteBatch.Draw(pixel, rect, color);
        }
    }
}
