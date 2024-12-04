using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Coin : ItemBase
    {
        private bool bAutoCollect = false;
        public Coin(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            sourceRectangle.Add(new Rectangle(128, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(158, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(188, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(218, 95, 8, 14));

            //bUseGravity = true;
            //SetAutoCollect(true);
        }

        public override string GetName()
        {
            return "Coin";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (GetLifeTime() <= 500.0f) // Milliseconds
            //{
            //    TeleportToTarget(new Vector2(40, 0));
            //}
        }

        public void SetAutoCollect(bool value)
        {
            bAutoCollect = value;
            MaxLifeTime = 2000.0f;
        }

        private void TeleportToTarget(Vector2 target)
        {
            bUseGravity = false;
            bCanCollect = false;

            double amount = 1.0 - (GetLifeTime()) / 500.0f; // Milliseconds
            position = Vector2.Lerp(position, target, (float)amount);
        }
    }
}
