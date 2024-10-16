using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MarioGame
{
    public class Ball : IBall
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public bool IsVisible { get; set; }
        private bool direction;  // true -> left, false -> right

        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.1f; // Time per frame in seconds
        private float timeCounter = 0f;    // Timer for tracking the frame updates
        private float scale = 2f; // Scale factor to enlarge the fireball

        // Static textures for fireball
        private static Texture2D spriteSheet;

        // Static fireball list, managing all instances of balls
        private static List<IBall> balls = new List<IBall>();

        // Fireball animation frames for left and right directions
        private static List<Rectangle> leftFrames;
        private static List<Rectangle> rightFrames;

        public static void LoadContent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("smb_enemies_sheet");

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

        public Ball(Vector2 position, float speed, bool direction)
        {
            Position = position;
            Position.Y = Position.Y - 30;
            Speed = speed;
            this.direction = direction;
            IsVisible = true; // Initialize to true so the ball is visible

            currentFrame = 0;
            totalFrames = leftFrames.Count; // Both left and right frames have the same count
        }

        // Handle fireball creation based on input flags from the controller
        public static void CreateFireballs(Vector2 playerPosition, float ballSpeed, Controllers.KeyboardController controller)
        {
            if (controller.keyboardPermitZ) // Fire to the left
            {
                balls.Add(new Ball(playerPosition, ballSpeed, true));
                controller.keyboardPermitZ = false; // Reset the flag
            }

            if (controller.keyboardPermitN) // Fire to the right
            {
                balls.Add(new Ball(playerPosition, ballSpeed, false));
                controller.keyboardPermitN = false; // Reset the flag
            }
        }

        // Update all balls
        public static void UpdateAll(GameTime gameTime, int screenWidth)
        {
            foreach (var ball in balls)
            {
                ball.Update(gameTime, screenWidth);
            }

            // Remove invisible balls
            balls.RemoveAll(b => !b.IsVisible);
        }

        // Draw all balls
        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var ball in balls)
            {
                ball.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move the ball based on its direction
            if (direction) // Move left
            {
                Position.X -= updatedSpeed;
            }
            else // Move right
            {
                Position.X += updatedSpeed;
            }

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

            // Check if the ball is off the screen
            if (Position.X < 0 || Position.X > screenWidth)
            {
                IsVisible = false; // Hide the ball when it goes off-screen
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                // Select the appropriate frame list based on direction
                Rectangle sourceRectangle = direction ? leftFrames[currentFrame] : rightFrames[currentFrame];

                // Draw the current frame using the correct sourceRectangle
                spriteBatch.Draw(spriteSheet, Position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        // Get all balls
        public static List<IBall> GetBalls()
        {
            return balls;
        }

        public Rectangle GetDestinationRectangle()
        {
            Rectangle sourceRectangle = direction ? leftFrames[currentFrame] : rightFrames[currentFrame];
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(sourceRectangle.Width * scale), (int)(sourceRectangle.Height * scale));
        }
    }
}