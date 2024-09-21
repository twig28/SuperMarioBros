using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class KoopaSprite : ISprite
    {
        private double posX;
        private double posY;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRecrangle;
        Rectangle SourceRectangle;
        private int spacingInterval = 20;
        public KoopaSprite(Texture2D Texture, SpriteBatch SpriteBatch, double X, double Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
        }

        public void Draw()
        {
            //Draw, incorporate state and timing and change Pos accordingly
        }

        public void Update()
        {
            //Destination Rectangle is Pos
        }
    }
}
