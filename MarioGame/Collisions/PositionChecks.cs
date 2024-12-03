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

        public static bool checkDeathByFalling(PlayerSprite mario, float screenHeight)
        {
            if (mario.GetDestinationRectangle().Y > screenHeight)
            {
                if(mario.lives > 0)
                {
                    mario.lives--;
                    Game1.Instance.ResetLevel();
                }
                else
                {
                    mario.current = PlayerSprite.SpriteType.Damaged;
                }
            }
            return false;
        }

        public static bool renderEnemy(IEnemy enemy, Vector2 offset, int width, int height)
        {
            Rectangle enemyRect = enemy.GetDestinationRectangle();
            float enemyScreenX = enemyRect.X + offset.X;
            float enemyScreenY = enemyRect.Y + offset.Y;

            if (enemyScreenX + enemyRect.Width > 0 && enemyScreenX < width &&
                enemyScreenY + enemyRect.Height > 0 && enemyScreenY < height)
            {
                return true;
            }
            return false;
        }
    }
}
