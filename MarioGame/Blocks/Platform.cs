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

        bool up = true;

        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(62, 37, 50, 25);
        protected Rectangle DestinationRectangle;

        public Platform(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 150, 50);
            IsCollided = false;
        }

        public void OnCollide()
        {}

        public virtual void Update(GameTime gameTime)
        {
            if (up) { DestinationRectangle.Y -= 1; }
            else { DestinationRectangle.Y += 1; }
            if(DestinationRectangle.Y < -50)
            {
                DestinationRectangle.Y = 800;
            }
            else if(DestinationRectangle.Y > 800)
            {
                DestinationRectangle.Y = 0;
            }
        }

        public void ReverseDirection()
        {
            up = !up;
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }
    }
}
