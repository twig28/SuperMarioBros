using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Scenery
{
    internal class SmallBush : IScenery
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public SmallBush(Texture2D texture, int x, int y)
        {
            sourceRectangle = new Rectangle(286, 23, 34, 20);
            destinationRectangle = new Rectangle(x, y, 100, 70);
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
