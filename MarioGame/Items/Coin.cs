using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Coin : ItemBase
    {
        public Coin(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(128, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(158, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(188, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(218, 95, 8, 14));

            maxFrameCnt = 4;
            bUseGravity = true;
        }

        public override string getName()
        {
            return "Coin";
        }
    }
}
