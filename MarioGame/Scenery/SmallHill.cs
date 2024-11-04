using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Scenery
{
    internal class SmallHill : IScenery
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public SmallHill(Texture2D texture, int x, int y)
        {
            sourceRectangle = new Rectangle(169, 20, 49, 20);
            destinationRectangle = new Rectangle(x, y, 150, 70);
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
