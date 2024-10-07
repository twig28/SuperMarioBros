using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class CollisionLogic
    {
        enum CollisionDirection
        {
            None = 0,
            Above = 1,
            Below = 2,
            Side = 3
        }
        CollisionDirection getCollisionDirection(Rectangle r1, Rectangle r2) //Comparing r1 to r2, i.e. r1 is below r2
        {
            if (!r1.Intersects(r2))
            {
                return CollisionDirection.None;
            }

            int overlapX = Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left);
            int overlapY = Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top);

            if (overlapY < overlapX)
            {
                if (r1.Top < r2.Top)
                {
                    return CollisionDirection.Below;
                }
                else
                {
                    return CollisionDirection.Above;
                }
            }
            else
            {
                return CollisionDirection.Side;
            }
        }
        void checkEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                //Piranhas have no collision with blocks
                if (!(enemy is Piranha))
                {
                    foreach (IBlock block in blocks)
                    {
                        //if (getCollisionDirection(enemy.GetDestinationRectangle(), block.getDestinationRectangle()) == CollisionDirection.Above)
                        {
                            enemy.setPosY = (int)block.Position.Y;
                        }
                    }
                }
            }
            //function that has a for each between mario and blocks/obstacles
            //function that has a for each between enemies and other enemies
            //function that has a for each between mario and enemies
            //function that has a for each between mario and items
            //function taht has a for each between fireballs and enemies

            void checkFireballEnemyCollision(List<Ball> fireballs, List<IEnemy> enemies, GameTime gt)
            {
                //TODO
                //ememy.TriggerKill(gt, false)
            }
        }
    }
}
