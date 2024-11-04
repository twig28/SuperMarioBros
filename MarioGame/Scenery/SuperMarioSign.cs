using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Scenery
{
    internal class SuperMarioSign : IScenery
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public SuperMarioSign(Texture2D texture, int x, int y)
        {
            sourceRectangle = new Rectangle(0, 0, 600, 300);
            destinationRectangle = new Rectangle(x, y, 600, 300);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
