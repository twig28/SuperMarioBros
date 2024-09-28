//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace MarioGame.Blocks
//{
//    // Block class that can be destroyed
//    public class Block : BaseBlock
//    {
//        public override bool IsSolid => true;
//        public override bool IsBreakable => true;

//        public Block(Vector2 position, Texture2D texture, Rectangle sourceRectangle)
//            : base(position, texture, sourceRectangle)
//        {
//        }

//        public override void OnCollide()
//        {
//            // Add logic to destroy the block or remove it from the game world
//            // For now, we can log or set a flag to indicate it's destroyed
//            // Example: IsDestroyed = true; (assuming you have an IsDestroyed property)
//        }
//    }
//}
