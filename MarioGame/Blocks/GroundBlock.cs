using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Blocks
{
    // GroundBlock class that cannot be destroyed
    public class GroundBlock : BaseBlock
    {
        public override bool IsSolid => true;
        public override bool IsBreakable => false;

        public GroundBlock(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
            : base(position, texture, sourceRectangle)
        {
        }

        public override void OnCollide()
        {
            // Ground blocks do nothing when Collide
        }
    }
}
