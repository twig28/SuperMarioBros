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
    internal class GoombaSprite : ISprite
    {
        private double posX;
        private double posY;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRecrangle;
        private int spacingInterval = 20;
        public GoombaSprite(Texture2D Texture, SpriteBatch SpriteBatch, double X, double Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
        }

        public void Draw()
        {

        }

        public void Update()
        {

        }
    }
}
