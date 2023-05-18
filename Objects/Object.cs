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

        public bool IsColliding(Entity other)
        {
            return !(
                this.Position.Y > (other.Position + other.Size).Y ||
                (this.Position + this.Size).Y < other.Position.Y ||
                this.Position.X > (other.Position + other.Size).X ||
                (this.Position + this.Size).X < other.Position.X
                );
        }

        public virtual void Update(GameModel game)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.White, Orientation, Size / 2f, 1f, 0, 0.5f);
        }
    }
}
