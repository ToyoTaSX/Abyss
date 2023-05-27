using Abyss.Maps;
using Abyss.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

namespace Abyss.Objects
{
    public class GameObject
    {
        public Texture2D Image;
        public CellState CellState;
        public Vector2 Position;
        public Point MapPosition;
        public bool IsHaveCollision;

        public GameObject(Texture2D image, CellState cellState, Vector2 position) 
        {
            Image = image;
            CellState = cellState;
            Position = position;
            MapPosition = Map.ToMapPosition(position);
            IsHaveCollision = !Map.EmtyStates.Contains(cellState);
        }

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
            if (!IsHaveCollision)
                return false;

            var inter = Rectangle.Intersection(other.Rectangle);
            var x = inter.XIntersection;
            var y = inter.YIntersection;
            return !(x < 5 || y < 5);
        }

        //public bool IsColliding(Bullet bullet)
        //{
        //    return Rectangle.Intersection(bullet.Start).Area > 0 || Rectangle.Intersection(bullet.End).Area > 0;
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.White, 0, Vector2.Zero, 1f, 0, 0.5f);
        }
    }
}
