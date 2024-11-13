using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items
{
    internal class BrickFragment : IItem
    {
        public Vector2 Position;
        protected Texture2D Texture { get; set; }
        Rectangle sourceRectangle = new Rectangle(270, 112, 8, 8);
        protected Rectangle DestinationRectangle;

        private int yOffset = 0;

        // need a base class to do physical simulation
        public float GravityScale { get; set; }
        public bool EnableGravity { get; set; }
        public Vector2 Velocity { get; set; }
        public float MaxLifeTime { get; set; }
        private float timer;

        public BrickFragment(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;

            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += deltaTime;

            if (EnableGravity)
            {
                Velocity += new Vector2(0, GravityScale * deltaTime);
            }
            Position.X += Velocity.X * deltaTime;
            Position.Y += Velocity.Y * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y + yOffset, 32, 32);
            spriteBatch.Draw(Texture, DestinationRectangle, sourceRectangle, Color.White);
        }

        public Rectangle getDestinationRectangle()
        {
            return DestinationRectangle;
        }

        public string getName()
        {
            return "BrickFragmentBlock";
        }

        public void moveY(int y)
        {
            yOffset += y;
        }

        public void OnCollide()
        {
            // no collision
        }

        public bool HasCollision()
        {
            return false;
        }

        public float GetLifeTime()
        {
            return MaxLifeTime - timer;
        }
    }
}
