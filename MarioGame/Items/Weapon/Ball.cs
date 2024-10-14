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

        private int rows = 1;    // sprite sheet rows
        private int columns = 8; // sprite sheet columns
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.1f; 
        private float timeCounter = 0f;    

        public Ball(Texture2D texture, Vector2 position, float speed, bool direction)
        {
            Texture = texture;
            Position = position;
            Position.Y = Position.Y - 40;
            Speed = speed;
            this.direction = direction;
            IsVisible = true; // initialize to be true true

            currentFrame = 0;
            totalFrames = rows * columns; //calculate all the frame
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // based on the moving fire ball
            if (direction) // left
            {
                Position.X -= updatedSpeed;
            }
            else // right
            {
                Position.X += updatedSpeed;
            }

            // update animation
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

            // to see if it is out of the screen
            if (Position.X < 0 || Position.X > screenWidth)
            {
                IsVisible = false; // after it get out of the screen make it invisible
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

        public void Update(object gameTime, int width)
        {
            throw new System.NotImplementedException();
        }
         public Rectangle GetDestinationRectangle()
        {
            int frameWidth = Texture.Width / columns;
            int frameHeight = Texture.Height / rows;
            return new Rectangle((int)Position.X, (int)Position.Y, frameWidth, frameHeight);
        }
    }
}
