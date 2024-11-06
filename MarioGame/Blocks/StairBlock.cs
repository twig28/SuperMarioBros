using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Blocks
{
    public class StairBlock : BaseBlock
    {
        public override bool IsSolid => true;
        public override bool IsBreakable => false;

        public StairBlock(Vector2 position, Texture2D texture)
            : base(position, texture, new Rectangle(0, 0, 160, 155))
        {
        }

        public override void OnCollide()
        {
            //do nothing
        }
    }
}
