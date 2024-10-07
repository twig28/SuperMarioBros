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

        public const double animInterval = 0.5;
        public const int spriteInterval = 16;
        public int currSprite = 0;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        protected Texture2D Texture { get; set; }
        protected Rectangle SourceRectangle;
        protected Rectangle DestinationRectangle;
        // private ISprite sprite;

        public MysteryBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
        {
            Position = position;
            Texture = texture;
            SourceRectangle = sourceRectangle;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 60, 60);
        }

        public void OnCollide() { }

        public virtual void Update(GameTime gameTime)
        {
            timeElapsed = gameTime.TotalGameTime.TotalSeconds;
            if (timeElapsed - timeElapsedSinceUpdate > animInterval)
            {
                timeElapsedSinceUpdate = timeElapsed;
                if (currSprite == 2)
                {
                    SourceRectangle.X -= (spriteInterval * 2);
                    currSprite = 0;
                }
                else
                {
                    SourceRectangle.X += spriteInterval;
                    currSprite += 1;
                }
            }
        }

        public Rectangle GetDestinationRectangle() { return new Rectangle((int)Position.X, (int)Position.Y, 60, 60); }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
    }
}
