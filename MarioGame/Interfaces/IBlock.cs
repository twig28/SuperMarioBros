using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Interfaces
{
    // Would have texture and position properties
    public interface IBlock
    {
        Vector2 Position { get; set; } // Block position

        bool IsSolid { get; } // Is block solid

        bool IsBreakable { get; } // Is block breakable

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        public Rectangle GetDestinationRectangle();

        void OnCollide(); // Handle block being hit by the player
    }
}
