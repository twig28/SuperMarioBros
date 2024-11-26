using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame.Blocks
{
    public class Platform : IBlock
    {
        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }
        public bool IsCollided { get; set; }

        public const double animInterval = 0;
        public const int spriteInterval = 0;
        public int currSprite = 0;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        bool up = true;

        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(248, 592, 25, 170);
        protected Rectangle DestinationRectangle;

        public Platform(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 75, 500);
            IsCollided = false;
        }

        public void OnCollide()
        {
            IsCollided = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (IsCollided)
            {
                if (up) { }
                else { }
            }
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }
    }
}
