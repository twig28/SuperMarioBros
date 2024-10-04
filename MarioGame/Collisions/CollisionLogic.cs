using MarioGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class CollisionLogic
    {
        //this will call the different collision classes

        //0 = no collision, 1 = above, 2 = below, 3 = from the side, will use enum
        int collisionDirection(Rectangle r1, Rectangle r2)
        {
            return 0;
        }

        //function that has a for each between enemies and blocks/obstacles
        void checkEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IBlock block in blocks)
                {
                    //if (collisionDirection(enemy.GetDestinationRectangle(), block.getDestinationRectangle()))
                    {
                        enemy.setPosY = (int)block.Position.Y;
                    }
                }
            }
            //function that has a for each between mario and blocks/obstacles
            //function that has a for each between enemies and other enemies
            //function that has a for each between mario and enemies
            //function that has a for each between mario and items
        }
    }
}
