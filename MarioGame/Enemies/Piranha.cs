using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Sprites;

namespace MarioGame
{
    internal class Piranha : IEnemy
    {
        private const double MovementInterval = 2;
        private const double AnimationInterval = 0.4;
        private const double DelayInterval = 5;

        private double movementTimer = 0;
        private double animationTimer = 0;
        private double delayTimer = 0;
        private bool isWaiting = false;
        private double deathStartTime = -1;

        private PiranhaSprite sprite;
        private bool isMovingUp = true;
        private bool _alive = true;

        public bool DefaultMoveMentDirection
        {
            get => isMovingUp;
            set => isMovingUp = value;
        }

        public double getdeathStartTime => deathStartTime;

        public bool Alive
        {
            get => _alive;
            set => _alive = value;
        }

        private int posX;
        public int setPosX { set => posX = value; }

        private int posY;
        public int setPosY { set => posY = value; }

        public Rectangle GetDestinationRectangle() => sprite.GetDestinationRectangle();

        public Piranha(Texture2D texture, SpriteBatch spriteBatch, int x, int y)
        {
            posX = x;
            posY = y;
            sprite = new PiranhaSprite(texture, spriteBatch, posX, posY);
            DefaultMoveMentDirection = true;  // Initial movement direction
        }

        public void Draw()
        {
            if (Alive) sprite.Draw();
        }

        public void Update(GameTime gameTime)
        {
            if (!Alive) return;

            double elapsed = gameTime.ElapsedGameTime.TotalSeconds;

            // Handle animation updates
            animationTimer += elapsed;
            if (animationTimer >= AnimationInterval)
            {
                animationTimer = 0;
                sprite.Update(gameTime);
            }

            if (isWaiting)
            {
                delayTimer += elapsed;
                if (delayTimer >= DelayInterval)
                {
                    // End delay and reset the timer
                    delayTimer = 0;
                    isWaiting = false;
                    isMovingUp = !isMovingUp;
                }
            }
            else
            {
                movementTimer += elapsed;
                if (movementTimer >= MovementInterval)
                {
                    movementTimer = 0;
                    isWaiting = true;
                }

                // Move the Piranha in the current direction if moving
                posY += isMovingUp ? -1 : 1;
            }

            sprite.posX = posX;
            sprite.posY = posY;
        }

        public void TriggerDeath(GameTime gameTime, bool stomped)
        {
            Alive = false;  // Use the new property
            sprite.Update(gameTime);  // Optional: freeze or animate on death
        }
    }
}
