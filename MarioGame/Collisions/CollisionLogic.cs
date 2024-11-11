using MarioGame.Blocks;
using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public static void CheckItemBlockCollision(List<IBlock> blocks, List<IItem> items)
        {
            Rectangle itemRectangle;
            Rectangle blockRectangle;
            Rectangle intersection;
            foreach (IItem item in items)
            {
                String itemName = item.getName();
                // if (itemName.Equals("Mushroom") || itemName.Equals("Star"))
                {
                    itemRectangle = item.getDestinationRectangle();
                    foreach (IBlock block in blocks)
                    {
                        blockRectangle = block.GetDestinationRectangle();
                        intersection = Rectangle.Intersect(itemRectangle, blockRectangle);
                        if (!intersection.IsEmpty)
                        {
                            if (intersection.Width >= intersection.Height)
                            {
                                if (itemName.Equals("Mushroom") || itemName.Equals("Star"))
                                    item.moveY(-intersection.Height);
                                item.OnCollide();
                                block.OnCollide();
                            }
                        }
                    }
                }
            }
        }

        public static void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt)
        {
            IItem itemToRemove = null;

            foreach (IItem item in items)
            {
                if (mario.GetDestinationRectangle().Intersects(item.getDestinationRectangle()))
                {
                    itemToRemove = item;
                    ApplyItemEffect(mario, item);
                }
            }

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        private static void ApplyItemEffect(PlayerSprite mario, IItem item)
        {
            switch (item.getName())
            {
                case "Star":
                    ActivateStarPower(mario);
                    break;

                case "FireFlower":
                    ActivateFirePower(mario);
                    break;

                case "Mushroom":
                    GrowMario(mario);
                    break;

                case "Coin":
                    IncreaseScore(mario);
                    break;
            }
        }

        private static void ActivateStarPower(PlayerSprite mario)
        {
            mario.Star = true;
        }

        private static void ActivateFirePower(PlayerSprite mario)
        {
            if (!mario.Star)
            {
                mario.Big = false;
                mario.Fire = true;
                mario.Game.Fire = true;
            }
        }

        private static void GrowMario(PlayerSprite mario)
        {
            if (!mario.Fire && !mario.Star)
            {
                mario.Big = true;
            }
        }

        private static void IncreaseScore(PlayerSprite mario)
        {
            mario.score += 1;
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
            }
        }


    }
}




