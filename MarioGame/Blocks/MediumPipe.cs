using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;

namespace MarioGame
{
    public class MediumPipe : IBlock
    {

        const int width = 120;
        const int height = 250;
        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }

        protected Texture2D Texture { get; set; }
        protected Rectangle SourceRectangle = new Rectangle(230, 385, 35, 66);
        protected Rectangle DestinationRectangle;

        public MediumPipe(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height); ;
        }

        public void OnCollide()
        {
            //no collision impact
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Update(GameTime gameTime)
        {
            // no update logic
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
    }
}
