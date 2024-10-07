using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Blocks
{
    // Block class that can be destroyed
    public class Block : BaseBlock
    {
        public override bool IsSolid => true;
        public override bool IsBreakable => true;

        // Property to track if the block is destroyed
        public bool IsDestroyed { get; private set; } = false;

        public Block(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
            : base(position, texture, sourceRectangle)
        {
        }

        public override void OnCollide()
        {
            // Add logic to destroy the block or remove it from the game world
            // For now, we can log or set a flag to indicate it's destroyed

            // Set the flag to indicate the block is destroyed
            IsDestroyed = true;

            //// Log message for debugging
            //System.Diagnostics.Debug.WriteLine("Block destroyed at position: " + Position);
        }


        // Optional method to draw only if the block is not destroyed
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDestroyed)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
