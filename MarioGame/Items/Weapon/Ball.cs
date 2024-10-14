using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class Ball : IBall
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public bool IsVisible { get; set; }
        private bool direction;  // true -> left, false -> right

        private int rows = 1;    // Number of rows in the sprite sheet
        private int columns = 8; // Number of columns in the sprite sheet
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.1f; // Time per frame in seconds
        private float timeCounter = 0f;    // Timer for tracking the frame updates

        public Ball(Texture2D textureLeft, Texture2D textureRight, Vector2 position, float speed, bool direction)
        {
            Texture = direction ? textureLeft : textureRight; // Choose the correct texture based on direction
            Position = position;
            Position.Y = Position.Y - 40;
            Speed = speed;
            this.direction = direction;
            IsVisible = true; // Initialize to true so the ball is visible

            currentFrame = direction ? columns - 1 : 0; // Start from the appropriate frame based on direction
            totalFrames = rows * columns; // Calculate the total number of frames
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
                if (direction) // Reverse animation for left direction
                {
                    currentFrame--;
                    if (currentFrame < 0)
                    {
                        currentFrame = columns - 1; // Loop back to the last frame
                    }
                }
                else // Forward animation for right direction
                {
                    currentFrame++;
                    if (currentFrame >= totalFrames)
                    {
                        currentFrame = 0;
                    }
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
                // Calculate the width and height of each frame
                int frameWidth = Texture.Width / columns;
                int frameHeight = Texture.Height / rows;

                // Calculate the current frame's row and column
                int row = currentFrame / columns;
                int column = currentFrame % columns;

                // Ensure the correct portion of the sprite sheet is drawn
                Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
                // Draw the current frame using the correct sourceRectangle
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White);
            }
        }

        public Rectangle GetDestinationRectangle()
        {
            int frameWidth = Texture.Width / columns;
            int frameHeight = Texture.Height / rows;
            return new Rectangle((int)Position.X, (int)Position.Y, frameWidth, frameHeight);
        }
    }
}