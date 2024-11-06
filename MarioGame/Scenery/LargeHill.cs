using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Scenery
{
    internal class LargeHill : IScenery
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public LargeHill(Texture2D texture, int x, int y)
        {
            sourceRectangle = new Rectangle(85, 4, 82, 36);
            destinationRectangle = new Rectangle(x, y, 300, 140);
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
