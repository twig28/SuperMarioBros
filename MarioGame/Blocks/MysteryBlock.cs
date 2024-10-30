using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame.Blocks
{
    public class MysteryBlock : IBlock
    {
        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }
        public bool IsOpened { get; set; }

        public const double animInterval = 0.5;
        public const int spriteInterval = 16;
        public int currSprite = 0;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;
        private const int dimension = 50;

        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(80, 112, 15, 15);
        protected Rectangle DestinationRectangle;
        // private ISprite sprite;

        public MysteryBlock(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, dimension, dimension);
            IsOpened = false;
        }

        public void OnCollide()
        {
            IsOpened = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsOpened)
            {
                sourceRectangle.X = 80 + spriteInterval * 4;
                currSprite = 4;// after hit
                return;
            }
            timeElapsed = gameTime.TotalGameTime.TotalSeconds;
            if (timeElapsed - timeElapsedSinceUpdate > animInterval)
            {
                timeElapsedSinceUpdate = timeElapsed;
                if (currSprite == 2)
                {
                    sourceRectangle.X -= (spriteInterval * 2);
                    currSprite = 0;
                }
                else
                {
                    sourceRectangle.X += spriteInterval;
                    currSprite += 1;
                }
            }
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }
    }
}
