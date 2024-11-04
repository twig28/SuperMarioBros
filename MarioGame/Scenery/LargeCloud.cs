using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Scenery
{
    internal class LargeCloud : IScenery
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public LargeCloud(Texture2D texture, int x, int y)
        {
            sourceRectangle = new Rectangle(143, 68, 66, 30);
            destinationRectangle = new Rectangle(x, y, 200, 90);
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
