using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MarioGame.Interfaces;

namespace MarioGame.Items
{
    internal class Spring : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Vector2 position;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 100;
        private int yOffset = 0;

        public Spring(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(94, 0, 16, 24));
            sourceRectangle.Add(new Rectangle(124, 0, 16, 24));
            sourceRectangle.Add(new Rectangle(154, 0, 16, 24));
        }

        public void Update(GameTime gameTime)
        {
            if (timer > timePerFrame)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame == 3)
                    currentFrame = 0;
            }
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }

        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public string getName()
        {
            return "Spring";
        }

        public void moveY(int y)
        {
            yOffset += y;
        }

        public void OnCollide()
        {

        }
    }
}
