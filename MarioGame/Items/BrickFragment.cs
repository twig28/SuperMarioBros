using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class BrickFragment : ItemBase
    {
        public BrickFragment(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(270, 112, 8, 8));

            bHasCollision = false;
            MaxLifeTime = 2000.0f;
        }

        public override string GetName()
        {
            return "BrickFragmentBlock";
        }
    }
}
