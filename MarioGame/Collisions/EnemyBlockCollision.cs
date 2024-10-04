using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame;
using Microsoft.Xna.Framework;
using MarioGame.Interfaces;

namespace MarioGame.Collisions
{
    internal class EnemyBlockCollision
    {
        void ExecuteCollision(IEnemy e, IBlock b)
        {
            e.setPosY = (int)b.Position.Y;
        }
    }
}
