using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame.Blocks
{
    public class Flagpole : IBlock
    {
        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }
        public bool IsCollided { get; set; }

        public bool isFinished = false;

        public const double animInterval = 0;
        public const int spriteInterval = 0;
        public int currSprite = 0;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(248, 592, 25, 170);
        protected Rectangle DestinationRectangle;

        public Flagpole(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 75, 500);
            IsCollided = false;
        }

        public void OnCollide()
        {
            if ((!IsCollided))
            {
                IsCollided = true;
            }
        }
        public Vector2 marioPosition()
        {
            return new Vector2(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsCollided)
            {
                //to be implemented
            }
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }
    }
}
