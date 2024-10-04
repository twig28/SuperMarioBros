using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Items;

namespace MarioGame
{
    internal class KoopaSprite : ISprite
    {
        private int SpriteWidth = 75;
        private int SpriteHeight = 115;
        private int SourceX = 210;
        private int SourceY = 0;
        private const int SourceWidth = 16;
        private const int SourceHeight = 24;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRectangle;
        Rectangle SourceRectangle;
        private const int spacingInterval = 30;
        int currSprite = 0;
        private bool hasChangedToShellAnim = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceLastUpdate = 0;
        private double elapsedTimeForShellAnim = 0.7;

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }
        public KoopaSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
            DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
            ChangeDirection = false;
        }

        public bool ChangeDirection { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }

        public void Draw()
        {
            DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            sb.Begin();
            sb.Draw(texture, DestinationRectangle, SourceRectangle, Color.White);
            sb.End();
        }

        public bool ChangeToShell(GameTime gm)
        {
            timeElapsed = gm.TotalGameTime.TotalSeconds;
            if (hasChangedToShellAnim && timeElapsed - timeElapsedSinceLastUpdate > elapsedTimeForShellAnim)
            {
                timeElapsedSinceLastUpdate = timeElapsed;
                SourceRectangle.X = 360;
                return true;
            }
            else
            {
                SpriteWidth = 75;
                SpriteHeight = 75;
                SourceRectangle.X = 330;
                hasChangedToShellAnim = true;
                return false;
            }
        }

        public void Update(GameTime gm)
        {

            if (ChangeDirection)
            {
                //Going Right Now
                if (SourceRectangle.X >= 220)
                {
                    SourceRectangle.X = 200;
                    currSprite = 0;
                }
                //Going Left Now
                else
                {
                    SourceRectangle.X = 150;
                    currSprite = 0;
                }
                ChangeDirection = false;
            }
            if (!hasChangedToShellAnim)
            {
                if (currSprite == 0)
                {
                    SourceRectangle.X += spacingInterval;
                    currSprite = 1;
                }
                else
                {
                    SourceRectangle.X -= spacingInterval;
                    currSprite = 0;
                }
            }

        }
    }
}
