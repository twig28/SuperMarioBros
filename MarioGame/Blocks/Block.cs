using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame
{
    // Block class that can be destroyed
    public class Block : BaseBlock
    {
        public override bool IsSolid => true;
        public override bool IsBreakable => true;

        private bool isBumped = false;
        private float bumpTimer = 0f;
        private const float bumpDuration = 0.2f; 

        public bool getIsBumped()
        {
            return isBumped;
        }
        public bool IsDestroyed { get; private set; } = false;

        public Block(Vector2 position, Texture2D texture)
            : base(position, texture, new Rectangle(4, 4, 40, 40))
        {
        }

        public override void OnCollide()
        {
            IsDestroyed = true;

            //play a breaking animation
        }

        public void Bump()
        {
            isBumped = true;
            bumpTimer = 0f; 
            //play animation
        }

        public override void Update(GameTime gameTime)
        {
            // Update bump timer if the block is bumped
            if (isBumped)
            {
                bumpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (bumpTimer >= bumpDuration)
                {
                    isBumped = false;
                    bumpTimer = 0f;
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
