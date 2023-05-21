using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Objects
{
    abstract class GameObject
    {
        public Texture2D Image;
        public Vector2 Position;
        float Orientation;

        public Vector2 Size
        {
            get => Image == null ? Vector2.Zero : new Vector2(Image.Width, Image.Height);
        }

        public Rectangle Rectangle
        {
            get =>  new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }

        public bool IsColliding(Entity other)
        {
            var inter = Rectangle.Intersection(other.Rectangle);
            var x = inter.XIntersection;
            var y = inter.YIntersection;
            return !(x < 5 || y < 5);
        }

        public virtual void Update(GameModel game)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.White, Orientation, Vector2.Zero, 1f, 0, 0.5f);
        }
    }
}
