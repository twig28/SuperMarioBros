using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Spring : ItemBase
    {
        public Spring(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(94, 0, 16, 24));
            sourceRectangle.Add(new Rectangle(124, 0, 16, 24));
            sourceRectangle.Add(new Rectangle(154, 0, 16, 24));

            maxFrameCnt = 3;
            timePerFrame = 100;
            bHasCollision = true;
        }

        public override string getName()
        {
            return "Spring";
        }
    }
}
