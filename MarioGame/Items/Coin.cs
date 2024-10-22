using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
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

        // need a base class to do physical simulation
        public float GravityScale { get; set; }
        public bool EnableGravity { get; set; }
        public Vector2 Velocity { get; set; }

        public Coin(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle.Add(new Rectangle(128, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(158, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(188, 95, 8, 14));
            sourceRectangle.Add(new Rectangle(218, 95, 8, 14));

            Velocity = Vector2.Zero;
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

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (EnableGravity)
            {
                Velocity += new Vector2(0, GravityScale * deltaTime);
            }
            position.X += Velocity.X * deltaTime;
            position.Y += Velocity.Y * deltaTime;
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

        public void OnCollide()
        {
            Velocity = Vector2.Zero;
            GravityScale = 0.0f;
            EnableGravity = false;
        }
    }
}
