using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame
{
	public abstract class BaseBlock : IBlock
	{

		const int dimension = 50;
        public Vector2 Position { get; set; }
        public abstract bool IsSolid { get; }
        public abstract bool IsBreakable { get; }

        protected Texture2D Texture { get; set; }
        protected Rectangle DestinationRectangle;
        protected Rectangle SourceRectanlge;
        // private ISprite sprite;

        public BaseBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
		{
			Position = position;
			Texture = texture;
			SourceRectanlge = sourceRectangle;
			DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, dimension, dimension);
        }

        public abstract void OnCollide();

		public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Update(GameTime gameTime)
		{
            DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, dimension, dimension);
        }

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, DestinationRectangle, SourceRectanlge, Color.White);
		}
	}
}
