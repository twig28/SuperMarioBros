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
            else
            {
                return CollisionDirection.Side;
            }
        }
        static public void CheckEnemyBlockCollisions(List<IEnemy> enemies, List<IBlock> blocks)
        {
            foreach (IEnemy enemy in enemies)
            {
                //Piranhas have no collision with blocks
                if (!(enemy is Piranha))
                {
                    foreach (IBlock block in blocks)
                    {
                        if (GetCollisionDirection(block.GetDestinationRectangle(), enemy.GetDestinationRectangle()) == CollisionDirection.Above)
                        {
                            enemy.setPosY = (int)block.Position.Y - enemy.GetDestinationRectangle().Height;
                            break;
                        }
                        else if (GetCollisionDirection(enemy.GetDestinationRectangle(), block.GetDestinationRectangle()) == CollisionDirection.Side)
                        {
                            enemy.MovingRight = !enemy.MovingRight;
                        }
                    }
                }
            }
        }

        //THESE ARE TODO BUT DONT HAVE TO BE IN THIS FILE
        //function that has a for each between mario and blocks/obstacles
        public static void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks)
        {
            List<IBlock> blocksToRemove = new List<IBlock>();

            foreach (IBlock block in blocks)
            {
                CollisionDirection collisionDirection = GetCollisionDirection(mario.GetDestinationRectangle(), block.GetDestinationRectangle());

                if (collisionDirection != CollisionDirection.None)
                {
                    if (collisionDirection == CollisionDirection.Below)
                    {
                        // Collision from below detected
                        if (block.IsBreakable)
                        {
                            block.OnCollide();
                            blocksToRemove.Add(block);
                        }
                    }
                    else if (collisionDirection == CollisionDirection.Above)
                    {
                        // Handle collision from above if necessary
                        mario.setPosY = (int)block.Position.Y - mario.GetDestinationRectangle().Height;
                    }
                    else if (collisionDirection == CollisionDirection.Side)
                    {
                        // Handle side collisions if necessary
                        if (mario.MovingRight)
                        {
                            mario.setPosX = (int)block.Position.X - mario.GetDestinationRectangle().Width;
                        }
                        else
                        {
                            mario.setPosX = (int)block.Position.X + block.GetDestinationRectangle().Width;
                        }
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
                    if (GetCollisionDirection(enemy.GetDestinationRectangle(), enemy2.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        enemy.MovingRight = !enemy.MovingRight;
                    }
                }

            }
        }

        void CheckMarioEnemyCollision(PlayerSprite mario, List<IEnemy> enemies, GameTime gt) { }

        void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt) { }

        void CheckFireballEnemyCollision(List<Ball> fireballs, List<IEnemy> enemies, GameTime gt)
        {
            foreach (Ball fireball in fireballs)
            {
            //if(GetCollisionDirection(fireball.GetDestinationRectangle(), enemy.GetDestinationRectangle())){
            //enemy.TriggerKill(gt, false)
            //}
            }
        }
    }
}
