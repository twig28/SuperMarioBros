using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MarioGame.Interfaces;

namespace MarioGame.Items
{
    internal class Star : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Vector2 position;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 100;
        private int yOffset = 0;
        private int xOffset = 0;

        public Star(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(5, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(35, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(65, 94, 14, 16));
            sourceRectangle.Add(new Rectangle(95, 94, 14, 16));
        }

        public void Update(GameTime gameTime)
        {
            yOffset += 2;
            xOffset++;
            if (timer > timePerFrame)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame == 4)
                    currentFrame = 0;
            }
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)position.X + xOffset, (int)position.Y + yOffset, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }

        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public string getName()
        {
            return "Star";
        }
        public void moveY(int y)
        {
            yOffset += y;
        }

    }
}
