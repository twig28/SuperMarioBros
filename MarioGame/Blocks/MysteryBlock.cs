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

        public const double animInterval = 0.2;
        public const int spriteInterval = 15;
        public int currSprite = 0;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        protected Texture2D Texture { get; set; }
        protected Rectangle SourceRectangle;
        // private ISprite sprite;

        public MysteryBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
        {
            Position = position;
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }

        public void OnCollide() { }

        public virtual void Update(GameTime gameTime)
        {
            if (timeElapsed - timeElapsedSinceUpdate > animInterval)
            {
                timeElapsedSinceUpdate = timeElapsed;
                if (currSprite == 3)
                {
                    SourceRectangle.X -= (spriteInterval * 3);
                }
                else
                {
                    SourceRectangle.X += spriteInterval;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
        }
    }
}
