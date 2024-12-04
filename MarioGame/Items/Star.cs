using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Star : ItemBase
    {
        public Star(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(5, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(35, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(65, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(95, 94, 14, 16));

            timePerFrame = 100;
            bHasCollision = true;
        }

        public override void Update(GameTime gameTime)
        {
            yOffset += 2;
            xOffset += 1;
            
            base.Update(gameTime);
        }

        public override string GetName()
        {
            return "Star";
        }
    }
}
