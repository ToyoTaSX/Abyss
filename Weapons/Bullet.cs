using Abyss.Architecture;
using Abyss.ContentClasses;
using Abyss.Maps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abyss.Weapons
{
    public class Bullet : Entity
    {
        public int Damage { get; private set; }
        public int PunchToExpire { get; set; }
        public float Direction { get => (float)Math.Atan2(Velocity.Y, Velocity.X); }

        public Vector2 StartVect
        {
            get
            {
                var v = CenterPosition + Velocity * Size.X / 2;
                return new Vector2(v.X, v.Y);
            }
        }

        public Vector2 EndVect
        {
            get
            {
                var v = CenterPosition - Velocity * Size.X / 2;
                return new Vector2(v.X, v.Y);
            }
        }
        public readonly List<Type> WhoCanBeDamaged;

        public Bullet(Vector2 velocity, int speed,  int punchToExpire, Vector2 startPos, int damage, List<Type> damageList)
        {
            image = Arts.Bullet;
            Speed = speed;
            WhoCanBeDamaged = damageList;
            PunchToExpire = punchToExpire;
            Velocity = velocity;
            Position = startPos;
            Damage = damage;
            Orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);
        }

        public override bool IsExpired()
        {
            return PunchToExpire <= 0;
        }
        public override void Update(GameModel game)
        {
            var map = game.CurrentLevel.LevelMap;
            var punchDelta = 0;
            var oldPos = Position;
            Position.X += Velocity.X * Speed;
            var startPos = Map.ToMapPosition(StartVect);
            if (!map.InBounds(startPos) || !Map.EmtyStates.Contains(map[startPos.X, startPos.Y]))
            {
                Position.X = oldPos.X;
                Velocity.X *= -1;
                punchDelta--;
            }

            Position.Y += Velocity.Y * Speed;
            startPos = Map.ToMapPosition(StartVect);
            if (!map.InBounds(startPos) || !Map.EmtyStates.Contains(map[startPos.X, startPos.Y]))
            {
                Position.Y = oldPos.Y;
                Velocity.Y *= -1;
                punchDelta--;
            }

            foreach (var entity in game.CurrentLevel.Entities.Where(e => WhoCanBeDamaged.Any(t => e.GetType().IsSubclassOf(t) || e.GetType() == t)))
                if (entity != this && entity.IsColliding(this))
                {
                    punchDelta--;
                    entity.OnDamage(this);
                    break;
                }
            PunchToExpire += Math.Sign(punchDelta);
            Orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);
        }
    }
}
