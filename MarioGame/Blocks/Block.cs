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
            // Set the flag to indicate the block is destroyed
            IsDestroyed = true;

            // Optionally, play a breaking animation or sound here
            // For example: PlayBreakingAnimation();
        }


        // Draw only if the block is not destroyed
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDestroyed)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
