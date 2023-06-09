﻿using Abyss.ContentClasses;
using Abyss.Objects;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Architecture
{
    public abstract class Entity
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

        public Vector2 CenterPosition
        {
            get
            {
                return image == null ? Vector2.Zero : Position + Size / 2f;
            }
        }

        public Rectangle Rectangle
        {
            get => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }

        public bool IsColliding(Entity other)
        {
            return Rectangle.Intersection(other.Rectangle).Area > 0;
        }

        public bool IsColliding(GameObject other)
        {
            var inter = Rectangle.Intersection(other.Rectangle);
            return inter.XIntersection > 3 || inter.YIntersection > 3;
        }

        public virtual bool IsExpired() => false;

        public virtual void OnDamage(Bullet bullet)
        { }

        public virtual void OnExpire(GameModel game)
        { }
        public abstract void Update(GameModel game);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position + Size / 2, null, Color.White, Orientation, Size / 2, 1, 0, 0f);
        }
    }
}
