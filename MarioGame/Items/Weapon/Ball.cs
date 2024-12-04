using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Interfaces;
using System;

namespace MarioGame
{
    public class Ball : IBall
    {
        private Vector2 Position;         // Position of the fireball
        private Vector2 Velocity;         // Velocity of the fireball (includes direction and speed)
        public bool IsVisible { get; set; } // Determines if the fireball is visible on screen

        private float bounceHeight = 30f; // Height of each bounce
        private float currentGroundLevel; // Current ground level where the fireball will collide
        private BallSprite ballSprite;    // Sprite representation of the fireball
        private bool isAscending;         // Flag to track if the fireball is in the ascending phase

        private const float Gravity = 500f; // Gravity effect for fireball arc
        private const int MaxFireballs = 100; // Maximum number of active fireballs allowed at once

        private static List<IBall> balls = new List<IBall>(); // Static list to store all active fireball instances

        /// <summary>
        /// Constructor for initializing a new fireball instance at a specified position, speed, and direction.
        /// </summary>
        /// <param name="position">Initial position of the fireball.</param>
        /// <param name="speed">Initial speed of the fireball.</param>
        /// <param name="direction">Direction of the fireball (true for left, false for right).</param>
        public Ball(Vector2 position, float speed, bool direction)
        {
            // Initialize the fireball starting position slightly above Mario's body center
            Position = new Vector2(position.X, position.Y - 15);
            currentGroundLevel = Position.Y; // Set the initial ground level to the current Y position
            IsVisible = true;
            isAscending = false;

            // Calculate initial velocity for a 45-degree downward launch angle
            float launchAngle = MathHelper.ToRadians(45);
            Velocity = new Vector2((direction ? -1 : 1) * speed * (float)Math.Cos(launchAngle),
                                   speed * (float)Math.Sin(launchAngle));

            ballSprite = new BallSprite(direction);
        }

        /// <summary>
        /// Creates new fireball instances based on player input and plays the fireball sound, with a limit of MaxFireballs.
        /// </summary>
        /// <param name="playerPosition">Position of the player (Mario) to launch the fireball from.</param>
        /// <param name="ballSpeed">Speed of the newly created fireballs.</param>
        /// <param name="controller">Controller handling keyboard input.</param>
        /// <param name="soundLib">Sound library instance for playing fireball sound effect.</param>
        public static void CreateFireballs(Vector2 playerPosition, float ballSpeed, Controllers.KeyboardController controller, SoundLib soundLib)
        {
            // Check if the number of active fireballs is less than the maximum allowed
            if (balls.Count < MaxFireballs)
            {
                if (controller.keyboardPermitZ) // Left fireball
                {
                    balls.Add(new Ball(playerPosition, ballSpeed, true));
                    controller.keyboardPermitZ = false;
                    soundLib.PlaySound("fireball");
                }

                if (controller.keyboardPermitN) // Right fireball
                {
                    balls.Add(new Ball(playerPosition, ballSpeed, false));
                    controller.keyboardPermitN = false;
                    soundLib.PlaySound("fireball");
                }
            }
            else
            {
                // Reset the fireball trigger if max fireballs are present
                // so that no new fireballs are created automatically when they disappear
                controller.keyboardPermitZ = false;
                controller.keyboardPermitN = false;
            }
        }

        public static void UpdateAll(GameTime gameTime, int screenWidth, List<IBlock> blocks)
        {
            foreach (var ball in balls)
            {
                ball.Update(gameTime, screenWidth, blocks);
            }
            balls.RemoveAll(b => !b.IsVisible); // Remove any fireballs marked as invisible
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var ball in balls)
            {
                ball.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, int screenWidth, List<IBlock> blocks)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isAscending)
            {
                // Fireball is in the ascending phase
                Position.Y -= Gravity * deltaTime;
                if (Position.Y <= currentGroundLevel - bounceHeight)
                {
                    isAscending = false; // Switch to descending phase after reaching peak
                }
            }
            else
            {
                // Fireball is in the descending phase
                Velocity.Y += Gravity * deltaTime;
                Position += Velocity * deltaTime;

                // Check for collisions with blocks
                foreach (var block in blocks)
                {
                    CollisionLogic.CollisionDirection collisionDirection = 
                        CollisionLogic.GetCollisionDirection(GetDestinationRectangle(), block.GetDestinationRectangle());

                    if (collisionDirection == CollisionLogic.CollisionDirection.Below)
                    {
                        // Collision with the top of the block, set new ground level
                        currentGroundLevel = block.GetDestinationRectangle().Top;
                        Position.Y = currentGroundLevel;
                        Bounce(); // Trigger bounce
                        break;
                    }
                    else if (collisionDirection == CollisionLogic.CollisionDirection.Side)
                    {
                        // Collision with the side, mark fireball for removal
                        IsVisible = false;
                        return; // Exit to avoid further calculations
                    }
                }
            }

            // Remove fireball if it goes off-screen
            if (Position.Y > screenWidth || Position.Y < 0)
            {
                IsVisible = false;
            }

            ballSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                ballSprite.Draw(spriteBatch, Position);
            }
        }

        public static List<IBall> GetBalls()
        {
            return balls;
        }

        public Rectangle GetDestinationRectangle()
        {
            return ballSprite.GetDestinationRectangle(Position);
        }

        public void Bounce()
        {
            if (!isAscending) // Ensure the fireball only bounces when descending
            {
                isAscending = true; // Switch to ascending state
                Velocity.Y = -MathF.Sqrt(2 * Gravity * bounceHeight); // Set bounce height
            }
        }
    }
}
