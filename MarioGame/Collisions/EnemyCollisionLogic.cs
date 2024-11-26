using MarioGame.Blocks;
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
        public static void CheckEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks, GameTime gt, PlayerSprite mario)
        {
            foreach (IEnemy enemy in enemies)
            {
                if (enemy is Piranha) continue;
                if (enemy.getdeathStartTime > 0) continue;
                foreach (IBlock block in blocks)
                {
                    HandleEnemyBlockCollision(enemy, block, gt, mario);
                }
            }
        }
        private static void HandleEnemyBlockCollision(IEnemy enemy, IBlock block, GameTime gt, PlayerSprite mario)
        {
            CollisionDirection collisionDirection = GetCollisionDirection(block.GetDestinationRectangle(), enemy.GetDestinationRectangle());

            // If the enemy is above the block, set its position on top
            if (collisionDirection == CollisionDirection.Above)
            {
                enemy.setPosY = (int)block.Position.Y - enemy.GetDestinationRectangle().Height;
                if(block is Block b && b.getIsBumped() || block is MysteryBlock b2 && b2.getIsBumped())
                {
                    enemy.TriggerDeath(gt, false);
                    mario.score += 100;
                }
            }
            // Handle side collision only if the enemy is near the block's top
            else if (collisionDirection == CollisionDirection.Side)
            {
                // Check that the enemy's bottom is close to the block's top (within a small tolerance)
                int bottomTolerance = 50;
                if (Math.Abs(enemy.GetDestinationRectangle().Bottom - block.GetDestinationRectangle().Top) >= bottomTolerance)
                {
                    enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
                }
            }
        }

        public static void CheckEnemyEnemyCollision(List<IEnemy> enemies, GameTime gt, PlayerSprite mario)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IEnemy enemy2 in enemies)
                {
                    if (enemy == enemy2) continue;
                    // Check for collision and handle if conditions are met
                    if (GetCollisionDirection(enemy.GetDestinationRectangle(), enemy2.GetDestinationRectangle()) != CollisionDirection.None && enemy2.getdeathStartTime <= 0)
                    {
                        HandleEnemyEnemyCollision(enemy, enemy2, gt, mario);
                    }
                }
            }
        }
        private static void HandleEnemyEnemyCollision(IEnemy enemy, IEnemy enemy2, GameTime gt, PlayerSprite mario)
        {
            enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
            if (enemy is KoopaShell && enemy2.getdeathStartTime <= 0)
            {
                enemy2.TriggerDeath(gt, false);
                enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
                mario.score += 100;
            }
        }
    }
}