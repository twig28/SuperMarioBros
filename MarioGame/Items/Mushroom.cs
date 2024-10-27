using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class Mushroom : IItem
    {
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private Vector2 position;
        private int currentFrame = 0;
        private double timer = 0;
        private const int timePerFrame = 200;
        private int yOffset = 0;
        private int xOffest = 0;

        public Mushroom(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(184, 34, 16, 16));
        }

        public void Update(GameTime gameTime)
        {
            yOffset += 2;
            xOffest += 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)position.X + xOffest, (int)position.Y + yOffset, 32, 32);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }

        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public string getName()
        {
            return "Mushroom";
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