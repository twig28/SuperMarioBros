using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Mushroom : ItemBase
    {
        public Mushroom(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(184, 34, 16, 16));
        }

        public override void Update(GameTime gameTime)
        {
            yOffset += 2;
            xOffset += 1;
        }

        public override string getName()
        {
            return "Mushroom";
        }
    }
}