using MarioGame.Blocks;
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
        public enum CollisionDirection
        {
            None = 0,
            Above = 1,
            Below = 2,
            Side = 3
        }
        static public CollisionDirection GetCollisionDirection(Rectangle r1, Rectangle r2) //Comparing r1 to r2, i.e. r1 is below r2
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
            return CollisionDirection.Side;
        }

        static public void CheckEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                // Piranhas have no collision with blocks
                if (!(enemy is Piranha))
                {
                    foreach (IBlock block in blocks)
                    {
                        CollisionDirection collisionDirection = GetCollisionDirection(block.GetDestinationRectangle(), enemy.GetDestinationRectangle());

                        if (collisionDirection == CollisionDirection.Above)
                        {
                            enemy.setPosY = (int)block.Position.Y - enemy.GetDestinationRectangle().Height;
                        }
                        else if (collisionDirection == CollisionDirection.Side)
                        {
                            // Allow side collision if enemy's bottom is not within a tolerance of the block's top (10)
                            if (enemy.GetDestinationRectangle().Bottom > block.GetDestinationRectangle().Top + 10)
                            {
                                enemy.MovingRight = !enemy.MovingRight;
                            }
                        }
                    }
                }
            }
        }

        //function that has a for each between mario and blocks/obstacles
        public static void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks)
        {
            List<IBlock> blocksToRemove = new List<IBlock>();

            foreach (IBlock block in blocks)
            {
                CollisionDirection collisionDirection = GetCollisionDirection(mario.GetDestinationRectangle(), block.GetDestinationRectangle());

                /*
                    if (collisionDirection == CollisionDirection.Below)
                    {
                        // Collision from below detected
                        if (block.IsBreakable)
                        {
                            block.OnCollide();
                            blocksToRemove.Add(block);
                        }
                    }
                    */
                    if (collisionDirection == CollisionDirection.Above)
                    {
                    // Handle collision from above if necessary
                    //mario.UPlayerPosition.Y = (float)(block.GetDestinationRectangle().Top);
                    mario.onblock = true;
                    }
                    
                    else if (collisionDirection == CollisionDirection.Side)
                    {
                        // Handle side collisions if necessary
                        if (mario.left)
                        {
                            mario.UPlayerPosition.X = (float)(block.GetDestinationRectangle().Right + mario.GetDestinationRectangle().Width /2);
                        }
                        else
                        {
                            mario.UPlayerPosition.X = (float)(block.GetDestinationRectangle().Left - mario.GetDestinationRectangle().Width /2);
                        }
                    }
                
            }

            // Remove destroyed blocks from the blocks list
            foreach (IBlock block in blocksToRemove)
            {
                blocks.Remove(block);
            }
        }

        //function that has a for each between enemies and other enemies
        void CheckEnemyEnemyCollision(List<IEnemy> enemies, GameTime gt)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IEnemy enemy2 in enemies)
                {
                    if (enemy2 == enemy) { continue; }
                    if (GetCollisionDirection(enemy.GetDestinationRectangle(), enemy2.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        enemy.MovingRight = !enemy.MovingRight;
                    }
                }

            }
        }

        public static void CheckMarioEnemyCollision(PlayerSprite mario, List<IEnemy> enemies, GameTime gt) { }

        public static void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt) { }

        public static void CheckFireballEnemyCollision(List<IBall> fireballs, List<IEnemy> enemies, GameTime gt)
        {
            foreach (IBall fireball in fireballs)
            {
                foreach (IEnemy enemy in enemies)
                {
                    if (GetCollisionDirection(fireball.GetDestinationRectangle(), enemy.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        enemy.TriggerDeath(gt, false);
                    }
                }
            }
        }

        public static void CheckFireballBlockCollision(List<IBall> fireballs, List<IBlock> blocks)
        {
            List<IBall> fireballsToRemove = new List<IBall>();

            foreach (IBall fireball in fireballs)
            {
                foreach (IBlock block in blocks)
                {
                    if (GetCollisionDirection(fireball.GetDestinationRectangle(), block.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        // Fireball hits the block, add fireball to removal list
                        fireballsToRemove.Add(fireball);
                        break; // Exit the loop after finding a collision for this fireball
                    }
                }
            }

            // Remove fireballs that have collided with blocks
            foreach (IBall fireball in fireballsToRemove)
            {
                fireballs.Remove(fireball);
            }
        }

        
    }
}
