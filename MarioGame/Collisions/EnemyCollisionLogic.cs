using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarioGame.CollisionLogic;

namespace MarioGame.Collisions
{
    internal class EnemyCollisionLogic
    {
        public static void CheckEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                if (enemy is Piranha) continue;

                foreach (IBlock block in blocks)
                {
                    HandleEnemyBlockCollision(enemy, block);
                }
            }
        }

        private static void HandleEnemyBlockCollision(IEnemy enemy, IBlock block)
        {
            CollisionDirection collisionDirection = GetCollisionDirection(block.GetDestinationRectangle(), enemy.GetDestinationRectangle());

            if (collisionDirection == CollisionDirection.Above)
            {
                // Position the enemy on top of the block
                enemy.setPosY = (int)block.Position.Y - enemy.GetDestinationRectangle().Height;
            }
            else if (collisionDirection == CollisionDirection.Side)
            {
                // Only change direction if the enemy is aligned with the side of the block and is close to the top of the block
                if (enemy.GetDestinationRectangle().Bottom >= block.GetDestinationRectangle().Top - 1 &&
                    enemy.GetDestinationRectangle().Bottom <= block.GetDestinationRectangle().Top + 5)
                {
                    enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
                }
            }
        }

        public static void CheckEnemyEnemyCollision(List<IEnemy> enemies, GameTime gt)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IEnemy enemy2 in enemies)
                {
                    if (enemy == enemy2) continue;

                    // Check for collision and handle if conditions are met
                    if (GetCollisionDirection(enemy.GetDestinationRectangle(), enemy2.GetDestinationRectangle()) != CollisionDirection.None && enemy2.getdeathStartTime <= 0)
                    {
                        HandleEnemyEnemyCollision(enemy, enemy2, gt);
                    }
                }
            }
        }

        private static void HandleEnemyEnemyCollision(IEnemy enemy, IEnemy enemy2, GameTime gt)
        {
            enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;

            if (enemy is KoopaShell && enemy2.getdeathStartTime <= 0)
            {
                enemy2.TriggerDeath(gt, false);
            }
        }
    }
}
