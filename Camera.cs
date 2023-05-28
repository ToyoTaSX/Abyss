using Abyss.Architecture;
using Abyss.Maps;
using Microsoft.Xna.Framework;

namespace Abyss
{
    public class Camera
    {
        public Point WindowSize { get; private set; }
        public Vector2 MainWindowCenter { get => WindowSize.ToVector2() / 2f; }

        public Point VisionWindowSize { get => new Point((int)(WindowSize.X / Scale), (int)(WindowSize.Y / Scale)); }
        public Vector2 CenterPosition {  get => WindowPos + VisionWindowSize.ToVector2() / 2f; }

        public Vector2 WindowPos { get; private set; }

        public readonly float Scale;

        public Matrix Transform { get; private set; }

        public Camera(Point windowSize, float scale)
        {
            this.WindowSize = windowSize;
            Scale = scale;
        }

        public void Follow(Entity target, Map map)
        {
            var dx = MathHelper.Clamp(target.CenterPosition.X, VisionWindowSize.X / 2 - 30, (map.Width + 1) * 32 - VisionWindowSize.X / 2 - 2);
            var dy = MathHelper.Clamp(target.CenterPosition.Y, VisionWindowSize.Y / 2 - 30, (map.Height + 2) * 32 - VisionWindowSize.Y / 2 - 2);
            WindowPos = new(dx - VisionWindowSize.X / 2, dy - VisionWindowSize.Y / 2);

            var position = Matrix.CreateTranslation(
            -dx,
            -dy,
            0);

            var offset = Matrix.CreateTranslation(
            WindowSize.X / 2,
            WindowSize.Y / 2,
            0);

            var scale = Matrix.CreateScale(Scale, Scale, 0);

            Transform = position * scale * offset;
        }
    }
}
