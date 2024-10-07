﻿using MarioGame.Interfaces;
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
                        }
                        else if (GetCollisionDirection(enemy.GetDestinationRectangle(), block.GetDestinationRectangle()) == CollisionDirection.Side)
                        {
                            enemy.MovingRight = !enemy.MovingRight;
                        }
                    }
                }
            }

            //THESE ARE TODO BUT DONT HAVE TO BE IN THIS FILE
            //function that has a for each between mario and blocks/obstacles
            void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks) { }
            //function that has a for each between enemies and other enemies
            void CheckEnemyEnemyCollision(List<IEnemy> enemies, GameTime gt) { }

            void CheckMarioEnemyCollision(PlayerSprite mario, List<IEnemy> enemies, GameTime gt) { }

            void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt) { }

            void CheckFireballEnemyCollision(List<Ball> fireballs, List<IEnemy> enemies, GameTime gt)
            {
                //if(){
                //enemy.TriggerKill(gt, false)
                //}
            }
        }
    }
}