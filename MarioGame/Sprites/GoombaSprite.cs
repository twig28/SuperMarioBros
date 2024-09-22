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
    internal class GoombaSprite : ISprite
    {
        private int posX;
        private int posY;
        private SpriteBatch sb;
        private Texture2D texture;
        Rectangle DestinationRectangle;
        Rectangle SourceRectangle;
        private int spacingInterval = 20;
        public GoombaSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            texture = Texture;
        }

        public void Draw()
        {

        }

        public void Update(GameTime gm)
        {

        }
    }
}
