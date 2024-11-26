using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class Block : BaseBlock
    {
        public override bool IsSolid => true;
        public override bool IsBreakable => true;

        private bool isBumped = false;
        private bool isBreaking = false;
        private float bumpTimer = 0f;
        private const float bumpDuration = 0.2f;
        private float breakDelay = 0.2f;
        private float breakTimer = 0f;

        public bool getIsBumped() => isBumped;
        public bool IsDestroyed { get; private set; } = false;

        public Block(Vector2 position, Texture2D texture)
            : base(position, texture, new Rectangle(4, 4, 40, 40))
        {
        }

        public override void OnCollide()
        {
            isBumped = true;
            if (!isBreaking && !IsDestroyed)
            {
                isBreaking = true;
                breakTimer = 0f;
            }
        }

        public void Bump()
        {
            if (!isBumped && !IsDestroyed)
            {
                isBumped = true;
                bumpTimer = 0f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isBumped)
            {
                bumpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (bumpTimer >= bumpDuration)
                {
                    isBumped = false;
                    bumpTimer = 0f;
                }
            }

            if (isBreaking)
            {
                breakTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (breakTimer >= breakDelay)
                {
                    IsDestroyed = true;
                    isBreaking = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDestroyed)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
