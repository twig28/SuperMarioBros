using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioGame
{
    public class DropFireball
    {
        private static List<DropFireball> dropFireballs = new List<DropFireball>();
        private Vector2 Position;
        private Vector2 Velocity;
        private BallSprite ballSprite;
        public bool IsVisible { get; private set; }
        private const float Gravity = 500f;
        private const float DropSpeed = 300f;
        private List<IBall> sharedBalls;

        public DropFireball(Vector2 startPosition, List<IBall> balls)
        {
            Position = startPosition;
            Velocity = new Vector2(0, DropSpeed);
            IsVisible = true;
            sharedBalls = balls;
            ballSprite = new BallSprite(false);
        }

        public void Update(GameTime gameTime, List<IBlock> blocks)
        {
            if (!IsVisible) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity.Y += Gravity * deltaTime;
            Position += Velocity * deltaTime;

            foreach (var block in blocks)
            {
                var collisionDirection = CollisionLogic.GetCollisionDirection(GetDestinationRectangle(), block.GetDestinationRectangle());

                if (collisionDirection == CollisionLogic.CollisionDirection.Below)
                {
                    IsVisible = false;
                    SpawnSideBalls();
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                ballSprite.Draw(spriteBatch, Position);
            }
        }

        public Rectangle GetDestinationRectangle()
        {
            return ballSprite.GetDestinationRectangle(Position);
        }

       private void SpawnSideBalls()
        {
            float speed = 200f; 
            Vector2 leftPosition = new Vector2(Position.X - 10, Position.Y);
            Vector2 rightPosition = new Vector2(Position.X + 10, Position.Y);

            sharedBalls.Add(new Ball(leftPosition, speed, true));
          
            sharedBalls.Add(new Ball(rightPosition, speed, false));
            IsVisible = false; 
        }


        public static void CreateDropFireball(Vector2 position)
        {
            dropFireballs.Add(new DropFireball(position, Ball.GetBalls()));
        }

        public static void UpdateAll(GameTime gameTime, List<IBlock> blocks)
        {
            foreach (var fireball in dropFireballs)
            {
                fireball.Update(gameTime, blocks);
            }
            dropFireballs.RemoveAll(f => !f.IsVisible);
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var fireball in dropFireballs)
            {
                fireball.Draw(spriteBatch);
            }
        }
    }
}
