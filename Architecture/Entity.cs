using Abyss.Architecture;
using Abyss.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss
{
    abstract class Entity
    {
        protected Texture2D image = Arts.Player;
        public Vector2 Position, Velocity;
        public float Speed;
        public float Orientation;
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

        public bool IsColliding(GameObject other)
        {
            return !(
                this.Position.Y > (other.Position + other.Size).Y ||
                (this.Position + this.Size).Y < other.Position.Y ||
                this.Position.X > (other.Position + other.Size).X ||
                (this.Position + this.Size).X < other.Position.X
                );
        }

        public virtual bool IsExpired() => false;

        public virtual void OnDamage(Bullet bullet)
        { }

        public virtual void OnExpire(GameModel game)
        { }
        public abstract void Update(GameModel game);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, Color.White, Orientation, Size / 2f, 1f, 0, 0);
        }
    }
}
