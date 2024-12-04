using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class FireFlower : ItemBase
    {
        public FireFlower(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(4, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(34, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(64, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(94, 64, 16, 16));

            maxFrameCnt = 4;
            timePerFrame = 100; // Milliseconds
            bHasCollision = true;
        }


        public override string getName()
        {
            return "FireFlower";
        }
    }
}