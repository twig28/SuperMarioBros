using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class MarioPositionChecks
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
            if(marioRect.Y > screenHeight)
            {
                return true;
            }
            return false;
        }

        public static bool isLevelFinished(Rectangle marioRect, int level)
        {
            if(level == 1 && marioRect.X > 7000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
