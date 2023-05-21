﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    public static class Extensions
    {
        public static (int XIntersection, int YIntersection, int Area) Intersection(this Rectangle r1, Rectangle r2)
        {
            var xOverlap = Math.Max(0, Math.Min(r1.X + r1.Width, r2.X + r2.Width) - Math.Max(r1.X, r2.X));
            var yOverlap = Math.Max(0, Math.Min(r1.Y + r2.Height, r2.Y + r2.Height) - Math.Max(r1.Y, r2.Y));
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



        public static bool AlmostEqual(this Vector2 v1,  Vector2 v2)
        {
            var delta = v2 - v1;
            return delta.LengthSquared() < 1; ;
        }
    }
}