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

        public void ItemBlockCollision(List<IBlock> blocks, IItem item)
        {
            Rectangle itemRectangle = item.getDestinationRectangle();
            Rectangle blockRectangle;
            Rectangle intersection;
            foreach (IBlock block in blocks)
            {
                blockRectangle = block.GetDestinationRectangle();
                intersection = Rectangle.Intersect(itemRectangle, blockRectangle);
                if (!intersection.IsEmpty)
                {
                    if (intersection.Width >= intersection.Height)
                    {
                        item.moveY(-intersection.Height);
                    }
                }
            }
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
                                enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
                            }
                        }
                    }
                }
            }
        }


        //function that has a for each between mario and blocks/obstacles
        public static void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks)
        {
            if(mario.current != PlayerSprite.SpriteType.Damaged) { 
            List<IBlock> blocksToRemove = new List<IBlock>();
            List<IBlock> StandingBlock = new List<IBlock>();
            List<IBlock> BelowBlock = new List<IBlock>();

            foreach (IBlock block in blocks)
            {
                Rectangle block_rec = block.GetDestinationRectangle();
                Rectangle mario_rec = mario.GetDestinationRectangle();
                if (mario_rec.Intersects(block_rec))
                {
                    if (mario.UPlayerPosition.Y < block_rec.Top)
                    {
                        StandingBlock.Add(block);
                        if (!mario.isGrounded)
                        {
                            mario.isGrounded = true;
                            mario.isJumping = false;
                            mario.velocity = 0f;
                            mario.Fallplayer.Speed = 0f;
                            if (!mario.left)
                            {
                                mario.current = PlayerSprite.SpriteType.Static;
                            }
                            else
                            {
                                mario.current = PlayerSprite.SpriteType.StaticL;
                            }



                        }
                        if (mario.Big || mario.Fire)
                        {
                            mario.UPlayerPosition.Y = block_rec.Top - mario_rec.Height / 2 + 26;
                        }
                        else if (!mario.Big && !mario.Fire)
                        {
                            mario.UPlayerPosition.Y = block_rec.Top - mario_rec.Height / 2 + 2;

                        }
                    }
                    else if (mario.UPlayerPosition.Y > block_rec.Bottom && !mario.isGrounded)
                    {
                        mario.velocity = 0f;

                        if (mario.Big || mario.Fire)
                        {
                            mario.UPlayerPosition.Y = block_rec.Bottom + mario_rec.Height / 2 + 24;
                        }
                        else if (!mario.Big && !mario.Fire)
                        {
                            mario.UPlayerPosition.Y = block_rec.Bottom + mario_rec.Height / 2 + 2;

                        }
                            /*if(write for if this block is mystery block)
                            {
                             1.  this block change to common state, i.e every mystery block can only be turn for one time
                             2.item come ou of mystery block and ?move?
                            }
                            */
                    }
                }
                else
                {
                    if (StandingBlock.Contains(block))
                    {
                        StandingBlock.Remove(block);
                    }
                }



            }

            if (StandingBlock.Count == 0 && mario.isGrounded)
            {

                mario.isGrounded = false;
                mario.isFalling = true;
                mario.current = PlayerSprite.SpriteType.Falling;
            }

            foreach (IBlock block in blocks)
            {
                Rectangle block_rec = block.GetDestinationRectangle();
                Rectangle mario_rec = mario.GetDestinationRectangle();

                if (!StandingBlock.Contains(block) && mario_rec.Intersects(block_rec))
                {

                        if ((mario.Big || mario.Fire) && block.IsBreakable)
                        {
                            blocksToRemove.Add(block);
                        }
                        else
                        {
                            if (mario_rec.Right >= block_rec.Left && mario_rec.Left < block_rec.Left)
                            {
                                mario.UPlayerPosition.X = block_rec.Left - mario_rec.Width / 2;
                            }
                            else if (mario_rec.Left <= block_rec.Right && mario_rec.Right > block_rec.Right)
                            {
                                mario.UPlayerPosition.X = block_rec.Right + mario_rec.Width / 2;
                            }
                        }

                }


            }
            foreach(IBlock block in blocksToRemove)
                {
                    blocks.Remove(block);
                }


        }




        }
     
        //function that has a for each between enemies and other enemies
        public static void CheckEnemyEnemyCollision(List<IEnemy> enemies, GameTime gt)
        {
            foreach (IEnemy enemy in enemies)
            {
                foreach (IEnemy enemy2 in enemies)
                {
                    if (enemy2 == enemy) { continue; }
                    if (GetCollisionDirection(enemy.GetDestinationRectangle(), enemy2.GetDestinationRectangle()) != CollisionDirection.None && enemy2.getdeathStartTime <= 0)
                    {
                        enemy.DefaultMoveMentDirection = !enemy.DefaultMoveMentDirection;
                        if (enemy is KoopaShell && enemy2.getdeathStartTime <= 0)
                        {
                            enemy2.TriggerDeath(gt, false);
                        }
                    }
                }
            }
        }
        
        public static void CheckMarioEnemyCollision(PlayerSprite mario, ref List<IEnemy> enemies, GameTime gt)
        {
            IEnemy enemyToRemove = null;
            IEnemy enemyToAdd = null;
            foreach (IEnemy enemy in enemies)
            {
                if (enemy.Alive == false) {
                    enemyToRemove = enemy;
                }
                if (GetCollisionDirection(mario.GetDestinationRectangle(), enemy.GetDestinationRectangle()) == CollisionDirection.Below)
                {
                    if(enemy is Piranha)
                    {
                        mario.current = PlayerSprite.SpriteType.Damaged;
                    }
                    else
                    {
                        if (enemy is Koopa koopa && koopa.getdeathStartTime <= 0)
                        {
                            enemy.TriggerDeath(gt, true);
                            //spawn new koopa (which includes triggering death)
                            KoopaShell shell = koopa.SpawnKoopa(gt);
                            enemyToAdd = shell;
                            
                            //Make mario Jump
                        }
                        else if (enemy is KoopaShell shell)
                        {
                            shell.Start();
                        }
                        //is normal enemy
                        else
                        {
                            enemy.TriggerDeath(gt, true);
                            //Make mario Jump
                        }
                    }
                }
                else if (GetCollisionDirection(mario.GetDestinationRectangle(), enemy.GetDestinationRectangle()) != CollisionDirection.None && enemy.getdeathStartTime <= 0)
                {
                    if (enemy is KoopaShell koopashell)
                    {
                        //nothing for now
                    }
                    else if (mario.Big || mario.Fire)
                    {
                        mario.Big = false;
                        mario.Fire = false;

                    }
                   
                    else
                    {
                        mario.current = PlayerSprite.SpriteType.Damaged;
                    }
                }
            }
            if (enemyToRemove != null) { enemies.Remove(enemyToRemove); }
            if (enemyToAdd != null) { enemies.Add(enemyToAdd); }
        }



        public static void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt)
        {
            IItem itemRemove = null;
            String a = "FireFlower";
            foreach (IItem item in items)
            {
                Rectangle mario_rec = mario.GetDestinationRectangle();
                Rectangle item_rec = item.getDestinationRectangle();
               
                if (mario_rec.Intersects(item_rec))
                {
                    itemRemove = item;
                    if(item.getName() == "FireFlower")
                    {
                        mario.Big = false;
                        mario.Fire = true;
                        mario.Game.Fire = true;
                    }
                    else if (item.getName() == "Mushroom")
                    {
                        if(!mario.Fire)
                        {
                            mario.Big = true;
                        }
                    }
                }
            }
            items.Remove(itemRemove);
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



        public static void CheckFireballEnemyCollision(List<IBall> fireballs, ref List<IEnemy> enemies, GameTime gm, bool stomped)
        {
            List<IBall> fireballsToRemove = new List<IBall>();
            List<IEnemy> enemyToDie = new List<IEnemy>();
            foreach (IBall fireball in fireballs)
            {
                foreach (IEnemy enemy in enemies)
                {
                    if (GetCollisionDirection(fireball.GetDestinationRectangle(), enemy.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        // Fireball hits the block, add fireball to removal list
                        fireballsToRemove.Add(fireball);
                        enemyToDie.Add(enemy);
                        break; // Exit the loop after finding a collision for this fireball
                    }
                }
            }

            // Remove fireballs that have collided with blocks
            foreach (IBall fireball in fireballsToRemove)
            {
                fireballs.Remove(fireball);
            }
            foreach (IEnemy enemy in enemyToDie)
            {
                enemy.TriggerDeath(gm, stomped);
                enemies.Remove(enemy);
            }
        }

        
    }
}




