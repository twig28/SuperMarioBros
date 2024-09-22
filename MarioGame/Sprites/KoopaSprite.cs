using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class KoopaSprite : ISprite
    {
        private const int SpriteWidth = 75;
        private const int SpriteHeight = 115;
        private int SourceX = 210;
        private int SourceY = 0;
        private const int SourceWidth = 16;
        private const int SourceHeight = 24;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRectangle;
        Rectangle SourceRectangle;
        private int spacingInterval = 30;
        int currSprite = 0;

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

        public void Update(GameTime gm)
        {

            if (ChangeDirection)
            {
                //Going Right
                if (SourceRectangle.X >= 220)
                {
                    SourceRectangle.X = 200;
                    currSprite = 0;
                }
                //Going Left
                else
                {
                    SourceRectangle.X = 150;
                    currSprite = 0;
                }
                //SourceRectangle = new Rectangle(50, 0, SpriteWidth, SpriteHeight);
                ChangeDirection = false;
            }
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
