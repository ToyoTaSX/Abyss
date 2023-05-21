using Abyss.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Architecture
{
    internal class Bullet : Entity
    {
        public int Damage;
        public int PunchToExpire;
        public Entity Owner;

        public Bullet()
        {
            image = Arts.Bullet;
        }

        public override bool IsExpired()
        {
            return PunchToExpire <= 0;
        }
        public override void Update(GameModel game)
        {
            var punchDelta = 0;
            var oldPos = Position;
            Position.X += Velocity.X;
            foreach (var obj in game.CurrentLevel.Objects)
                if (obj.IsColliding(this))
                {
                    Position.X = oldPos.X;
                    Velocity.X *= -1;
                    punchDelta--;
                }

            Position.Y += Velocity.Y;
            foreach (var obj in game.CurrentLevel.Objects)
                if (obj.IsColliding(this))
                {
                    Position.Y = oldPos.Y;
                    Velocity.Y *= -1;
                    punchDelta--;
                }

            foreach (var entity in game.CurrentLevel.Entities)
                if (entity.IsColliding(this) && entity != this && entity != Owner)
                {
                    punchDelta--;
                    entity.OnDamage(this);
                    break;
                }
            PunchToExpire += Math.Sign(punchDelta);
        }
    }
}
