using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Sprites
{
    internal class PiranhaSprite : ISprite
    {
        private const int SpriteWidth = 75;
        private const int SpriteHeight = 115;
        private int SourceX = 390;
        private int SourceY = 30;
        private const int SourceWidth = 16;
        private const int SourceHeight = 24;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRectangle;
        Rectangle SourceRectangle;
        private const int spacingInterval = 30;
        int currSprite = 0;

        public bool ChangeDirection { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public PiranhaSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
            DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
            ChangeDirection = false;
        }

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
                currSprite = 0;
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
