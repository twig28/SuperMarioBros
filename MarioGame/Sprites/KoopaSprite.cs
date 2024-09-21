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
        private Texture2D Texture;
        Rectangle DestinationRecrangle;
        public KoopaSprite(SpriteBatch SpriteBatch, double X, double Y)
        {
            //make ISprite
            sb = SpriteBatch;
            posX = X; posY = Y;
        }

        public void Draw()
        {

        }

        public void Update()
        {

        }
    }
}
