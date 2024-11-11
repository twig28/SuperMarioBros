using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame.Blocks
{
    public class LPipe : IBlock
    {
        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }
        public bool IsCollided { get; set; }

        public const double animInterval = 0;
        public const int spriteInterval = 0;
        public int currSprite = 0;

        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(156, 381, 65, 70);
        protected Rectangle DestinationRectangle;

        public LPipe(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 200, 200);
            IsCollided = false;
        }

        public void OnCollide()
        {
            IsCollided = true;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }
    }
}
