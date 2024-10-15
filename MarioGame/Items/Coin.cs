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
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 200;

        public Coin(Texture2D texture) {
            this.texture = texture;
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 32, 32);
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
    }
}
