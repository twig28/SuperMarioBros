using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioGame.Interfaces;

namespace MarioGame.Items
{
    internal class Flower2 : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 100;

        public Flower2(Texture2D texture)
        {
            this.texture = texture;
            sourceRectangle.Add(new Rectangle(124, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(154, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(184, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(214, 64, 16, 16));
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
            return "Flower2";
        }
    }
}
