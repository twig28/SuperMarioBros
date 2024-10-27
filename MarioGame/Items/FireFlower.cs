using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioGame.Interfaces;
using System.Net.NetworkInformation;

namespace MarioGame.Items
{
    internal class FireFlower : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Vector2 position;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 100;
        private int yOffset = 0;

        public FireFlower(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(4, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(34, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(64, 64, 16, 16));
            sourceRectangle.Add(new Rectangle(94, 64, 16, 16));
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
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }
        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public string getName()
        {
            return "FireFlower";
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