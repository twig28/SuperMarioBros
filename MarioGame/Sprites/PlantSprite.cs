using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Sprites
{
    internal class PlantSprite : ISprite
    {
        private int posX;
        private int posY;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRectangle;
        Rectangle SourceRectangle;
        private int spacingInterval = 20;
        public PlantSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
            DestinationRectangle = new Rectangle(posX, posY, 150, 150);
        }

        public void Draw()
        {
            //Draw, incorporate state and timing and change Pos accordingly
        }

        public void Update()
        {
            DestinationRectangle = new Rectangle(posX, posY, 150, 150);
        }
    }
}
