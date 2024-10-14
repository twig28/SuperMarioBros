using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame.Blocks
{
	public abstract class BaseBlock : IBlock
	{

		const int dimension = 60;
        public Vector2 Position { get; set; }
        public abstract bool IsSolid { get; }
        public abstract bool IsBreakable { get; }

        protected Texture2D Texture { get; set; }
        protected Rectangle SourceRectangle;
        protected Rectangle DestinationRectangle;
        // private ISprite sprite;

        public BaseBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
		{
			Position = position;
			Texture = texture;
            SourceRectangle = sourceRectangle;
			DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, dimension, dimension); ;
        }

        public abstract void OnCollide();

		public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Update(GameTime gameTime)
		{
			// Base update logic
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
		}
	}
}
