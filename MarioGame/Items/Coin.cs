using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Coin : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Vector2 position;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 200;
        private int yOffset = 0;

        public Coin(Texture2D texture, Vector2 position) {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(128, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(158, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(188, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(218, 95, 8, 14));
        }

        public void Update(GameTime gameTime)
        {
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
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y + yOffset, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }

        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public string getName()
        {
            return "Coin";
        }

        public void moveY(int y)
        {
            yOffset += y;
        }
    }
}
