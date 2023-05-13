using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss
{
    abstract class Entity
    {
        protected Texture2D image = Arts.Player;
        protected Color color = Color.White;
        public Vector2 Position, Velocity;
        public float Speed;
        public float Orientation;
        public bool IsExpired;
        public bool IsHaveCollision = true;
        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
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

        public abstract void Update(GameModel game);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Vector2.Zero, 1f, 0, 0);
        }
    }
}
