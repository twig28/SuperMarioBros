using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioGame
{
    public class Ball : IBall
    {
        public Vector2 Position;
        public float Speed;
        public bool IsVisible { get; set; }

        private BallSprite ballSprite;
        private bool direction; // true -> left, false -> right

        // Static fireball list, managing all instances of balls
        private static List<IBall> balls = new List<IBall>();

        public Ball(Vector2 position, float speed, bool direction)
        {
            Position = position;
            Position.Y = Position.Y - 30;
            Speed = speed;
            IsVisible = true; // Initialize to true so the ball is visible

            this.direction = direction; // Initialize the direction
            ballSprite = new BallSprite(direction);
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
            Position.X += direction ? -updatedSpeed : updatedSpeed;

            // Update the sprite animation
            ballSprite.Update(gameTime);

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
                ballSprite.Draw(spriteBatch, Position);
            }
        }

        // Get all balls
        public static List<IBall> GetBalls()
        {
            return balls;
        }

        public Rectangle GetDestinationRectangle()
        {
            return ballSprite.GetDestinationRectangle(Position);
        }
    }
}
