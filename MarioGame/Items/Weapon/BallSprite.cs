using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioGame
{
    public class BallSprite
    {
        private static Texture2D spriteSheet;
        private static List<Rectangle> leftFrames;
        private static List<Rectangle> rightFrames;
        private float scale = 2f;
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.1f;
        private float timeCounter = 0f;

        private bool direction; // true -> left, false -> right
        
        public static void LoadContent(Texture2D texture)
        {
            spriteSheet = texture;

            // Define frames for fireball animation
            leftFrames = new List<Rectangle>
            {
                new Rectangle(101, 253, 24, 8),
                new Rectangle(131, 253, 24, 8)
            };

            rightFrames = new List<Rectangle>
            {
                new Rectangle(161, 253, 24, 8),
                new Rectangle(191, 254, 24, 8)
            };
        }

        public BallSprite(bool direction)
        {
            this.direction = direction;
            currentFrame = 0;
            totalFrames = leftFrames.Count; // Both left and right frames have the same count
        }

        public void Update(GameTime gameTime)
        {
            // Update animation frames
            timeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter >= timePerFrame)
            {
                currentFrame++;
                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
                timeCounter -= timePerFrame;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Select the appropriate frame list based on direction
            Rectangle sourceRectangle = direction ? leftFrames[currentFrame] : rightFrames[currentFrame];

            // Draw the current frame using the correct sourceRectangle
            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public Rectangle GetDestinationRectangle(Vector2 position)
        {
            Rectangle sourceRectangle = direction ? leftFrames[currentFrame] : rightFrames[currentFrame];
            return new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Width * scale), (int)(sourceRectangle.Height * scale));
        }
    }
}
