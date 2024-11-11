using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioGame
{
    public class Ball : IBall
    {
        private Vector2 Position;         // Position of the fireball
        private float Speed;              // Movement speed of the fireball
        public bool IsVisible { get; set; } // Determines if the fireball is visible on screen

        private float InitialY;           // Initial Y-coordinate of the fireball (for wave calculation)
        private float InitialX;           // Initial X-coordinate of the fireball
        private float lifeTime;           // Time the fireball has been active
        private BallSprite ballSprite;    // Sprite representation of the fireball
        private bool direction;           // Fireball direction: true = left, false = right

        // Static list to store all active fireball instances
        private static List<IBall> balls = new List<IBall>();

        /// <summary>
        /// Constructor for the Ball class, initializes position, speed, direction, and other properties.
        /// </summary>
        /// <param name="position">Starting position of the fireball.</param>
        /// <param name="speed">Speed of the fireball.</param>
        /// <param name="direction">Direction of the fireball, true = left, false = right.</param>
        public Ball(Vector2 position, float speed, bool direction)
        {
            Position = position;
            Position.Y -= 30;           // Offset Y position slightly upwards
            InitialY = Position.Y;
            InitialX = Position.X;
            Speed = speed;
            IsVisible = true;
            lifeTime = 0f;              // Initialize lifetime to zero

            this.direction = direction;
            ballSprite = new BallSprite(direction);
        }

        /// <summary>
        /// Creates new fireballs based on player input flags from the keyboard controller.
        /// </summary>
        /// <param name="playerPosition">Current position of the player.</param>
        /// <param name="ballSpeed">Speed of the new fireballs.</param>
        /// <param name="controller">Keyboard controller instance.</param>
        public static void CreateFireballs(Vector2 playerPosition, float ballSpeed, Controllers.KeyboardController controller)
        {
            if (controller.keyboardPermitZ) // If 'Z' key is pressed for left fireball
            {
                balls.Add(new Ball(playerPosition, ballSpeed, true));
                controller.keyboardPermitZ = false; // Reset key flag
                Game1.Instance.GetSoundLib().PlaySound("fireball");
            }

            if (controller.keyboardPermitN) // If 'N' key is pressed for right fireball
            {
                balls.Add(new Ball(playerPosition, ballSpeed, false));
                controller.keyboardPermitN = false; // Reset key flag
                Game1.Instance.GetSoundLib().PlaySound("fireball");
            }
        }

        /// <summary>
        /// Updates all fireball instances in the game, handling visibility and removal.
        /// </summary>
        /// <param name="gameTime">GameTime instance for elapsed time calculations.</param>
        /// <param name="screenWidth">Width of the game screen, used for fireball limits.</param>
        public static void UpdateAll(GameTime gameTime, int screenWidth)
        {
            foreach (var ball in balls)
            {
                ball.Update(gameTime, screenWidth);
            }

            // Remove any fireballs that are no longer visible
            balls.RemoveAll(b => !b.IsVisible);
        }

        /// <summary>
        /// Draws all visible fireball instances on the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used to draw sprites.</param>
        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var ball in balls)
            {
                ball.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Updates the position, animation, and visibility of the fireball.
        /// Fireball will be removed after 4 seconds of being active.
        /// </summary>
        /// <param name="gameTime">GameTime instance for elapsed time calculations.</param>
        /// <param name="screenWidth">Width of the game screen, used for fireball limits.</param>
        public void Update(GameTime gameTime, int screenWidth)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Increment the lifetime of the fireball
            lifeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update fireball's X position based on direction
            Position.X += direction ? -updatedSpeed : updatedSpeed;

            // Create a wave trajectory in the Y axis using a sine function
            float amplitude = 30f;         // Height of the wave motion
            float frequency = 0.1f;        // Frequency of the wave motion
            Position.Y = InitialY + amplitude * (float)System.Math.Sin(frequency * Position.X);

            // Update the animation of the fireball sprite
            ballSprite.Update(gameTime);

            // Set the fireball to invisible if it has existed for more than 4 seconds
            if (lifeTime > 4f)
            {
                IsVisible = false;
            }
        }

        /// <summary>
        /// Draws the fireball on the screen if it is visible.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used to draw sprites.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                ballSprite.Draw(spriteBatch, Position);
            }
        }

        /// <summary>
        /// Retrieves the current list of all active fireball instances.
        /// </summary>
        /// <returns>List of IBall instances.</returns>
        public static List<IBall> GetBalls()
        {
            return balls;
        }

        /// <summary>
        /// Gets the destination rectangle for collision or rendering purposes.
        /// </summary>
        /// <returns>Rectangle representing the fireball's position and size.</returns>
        public Rectangle GetDestinationRectangle()
        {
            return ballSprite.GetDestinationRectangle(Position);
        }
    }
}
