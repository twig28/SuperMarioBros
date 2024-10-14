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
		// private ISprite sprite;

		public BaseBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
		{
			Position = position;
			Texture = texture;
            SourceRectangle = sourceRectangle;
        }

        public abstract void OnCollide();

		public Rectangle GetDestinationRectangle() { return new Rectangle((int)Position.X, (int)Position.Y, dimension, dimension); }

        public virtual void Update(GameTime gameTime)
		{
			// Base update logic
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, dimension, dimension), SourceRectangle, Color.White);
		}
	}
}
