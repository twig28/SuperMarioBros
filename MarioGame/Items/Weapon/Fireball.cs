using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class Fireball:IEnemy
    {
        private Vector2 position;
        private bool movingLeft;
        private int speed = 5;
        private bool isAlive = true;
        private BallSprite sprite; 

        public bool Alive => isAlive;

        public bool DefaultMoveMentDirection { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int setPosX { set => throw new System.NotImplementedException(); }
        public int setPosY { set => throw new System.NotImplementedException(); }

        public double getdeathStartTime => throw new System.NotImplementedException();

        bool IEnemy.Alive { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Rectangle GetDestinationRectangle()
        {
            return sprite.GetDestinationRectangle(position);
        }

        public Fireball(Vector2 initialPosition, bool movingLeft)
        {
            this.position = initialPosition;
            this.movingLeft = movingLeft;
            this.sprite = new BallSprite(movingLeft); 
        }

        public void Update(GameTime gameTime)
        {
            if (!isAlive) return;

            position.X += movingLeft ? -speed : speed;

          
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                sprite.Draw(spriteBatch, position);
            }
        }

        public void Destroy()
        {
            isAlive = false; 
        }

        public void Draw()
        {
            throw new System.NotImplementedException();
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            throw new System.NotImplementedException();
        }
    }
}
