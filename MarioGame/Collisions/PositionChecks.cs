using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class PositionChecks
    {
        public static Vector2 GetCameraOffset(Rectangle marioRect, float screenWidth)
        {
            Vector2 offset = new Vector2(0, 0);
            float cameraOffsetThreshold = 650;
            float marioPositionX = marioRect.X + marioRect.Width / 2;
            float screenCenterX = screenWidth / 2;

            if (marioPositionX > cameraOffsetThreshold)
            {
                offset = new Vector2(screenCenterX - marioPositionX, 0);
            }
            return offset;
        }

        public static bool checkDeathByFalling(Rectangle marioRect, float screenHeight)
        {
            if (marioRect.Y > screenHeight)
            {
                return true;
            }
            return false;
        }

        public static bool renderEnemy(IEnemy enemy, Vector2 offset, int width, int height)
        {
            Rectangle enemyRect = enemy.GetDestinationRectangle();
            float enemyScreenX = enemyRect.X + offset.X;
            float enemyScreenY = enemyRect.Y + offset.Y;

            // Check if the enemy is within the visible screen boundaries
            if (enemyScreenX + enemyRect.Width > 0 && enemyScreenX < width &&
                enemyScreenY + enemyRect.Height > 0 && enemyScreenY < height)
            {
                return true;
            }
            return false;
        }

        public static void checkPipe(PlayerSprite mario, int level)
        {
            int newLevel;
            Vector2 newPosition;
            Rectangle secretRectangle;
            if(level == 1)
            {
                newLevel = 0;
                secretRectangle = new Rectangle(0,0,20,1);
            }
            else
            {
                newLevel = 1;
                secretRectangle = new Rectangle(0, 0, 1, 20);
            }
                if (CollisionLogic.GetCollisionDirection(mario.GetDestinationRectangle(), secretRectangle) == CollisionLogic.CollisionDirection.Below) //&& crouched or pushing down
                {
                    Game1.Instance.SetLevel(newLevel);
                    //move mario to the shortcut location using newPosition
                }
                }
            }
        }
