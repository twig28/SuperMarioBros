using Microsoft.Xna.Framework;
using MarioGame.Interfaces;
using System.Collections.Generic;
using System;

namespace MarioGame
{
    public static class BallCollisionLogic
    {
        public enum CollisionDirection
        {
            None = 0,
            Above = 1,
            Below = 2,
            Side = 3
        }

        /// <summary>
        /// Determines the collision direction between two rectangles.
        /// </summary>
        public static CollisionDirection GetCollisionDirection(Rectangle r1, Rectangle r2)
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

        /// <summary>
        /// Handles the collision between the fireball and blocks, applying bounce or removal effects as necessary.
        /// </summary>
        public static void HandleFireballBlockCollision(IBall fireball, List<IBlock> blocks)
        {
            foreach (var block in blocks)
            {
                CollisionDirection collisionDirection = GetCollisionDirection(fireball.GetDestinationRectangle(), block.GetDestinationRectangle());

                if (collisionDirection == CollisionDirection.Below)
                {
                    // Trigger bounce if collision detected below
                    fireball.Bounce();
                    break;
                }
                else if (collisionDirection == CollisionDirection.Side)
                {
                    // Mark fireball as invisible if collision detected on the side
                    fireball.IsVisible = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Handles collisions between fireballs and enemies, marking fireballs for removal and triggering enemy death.
        /// </summary>
        public static void CheckFireballEnemyCollision(List<IBall> fireballs, ref List<IEnemy> enemies, GameTime gameTime, bool stomped)
        {
            List<IBall> fireballsToRemove = new List<IBall>();
            List<IEnemy> enemiesToDie = new List<IEnemy>();

            foreach (IBall fireball in fireballs)
            {
                foreach (IEnemy enemy in enemies)
                {
                    if (GetCollisionDirection(fireball.GetDestinationRectangle(), enemy.GetDestinationRectangle()) != CollisionDirection.None)
                    {
                        // Collision detected: mark the fireball and enemy for removal
                        fireballsToRemove.Add(fireball);
                        enemiesToDie.Add(enemy);
                        break;
                    }
                }
            }

            // Remove collided fireballs and trigger enemy death
            foreach (IBall fireball in fireballsToRemove)
            {
                fireballs.Remove(fireball);
            }
            foreach (IEnemy enemy in enemiesToDie)
            {
                enemy.TriggerDeath(gameTime, stomped);
            }
        }
    }
}
