using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    internal class KoopaShellSprite : ISprite
    {
        private const int SpriteWidth = 60;
        private const int SpriteHeight = 50;
        private const int SourceX = 360;
        private int SourceY = 4;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;

        public int posX { get; set; }
        public int posY { get; set; }

        public KoopaShellSprite(Texture2D texture, SpriteBatch spriteBatch, int x, int y, int color)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            posX = x;
            posY = y;

            if(color == 2)
            {
                SourceY = 63;
            }
            destinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            sourceRectangle = new Rectangle(SourceX, SourceY, 17, 16);
        }

        public Rectangle GetDestinationRectangle() => destinationRectangle;

        public void Draw()
        {
            destinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update(GameTime gm)
        {
            // No animation updates needed; the shell sprite is static.
        }
    }
}
