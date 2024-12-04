using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MarioGame.Items
{
    internal class ItemBase : IItem
    {
        protected List<Rectangle> sourceRectangle = new List<Rectangle>();
        protected Rectangle destinationRectangle;
        protected Texture2D texture;
        protected Vector2 position;

        protected int currentFrame = 0;
        protected int maxFrameCnt = 1;
        protected int timePerFrame = 200; // Milliseconds

        protected double itemTime = 0;
        protected double lastUpdateTime = 0;

        protected int xOffset = 0;
        protected int yOffset = 0;

        protected int width = 32;
        protected int height = 32;

        public float GravityScale { get; set; }
        public bool bUseGravity { get; set; }
        public Vector2 Velocity { get; set; }
        public bool bHasCollision { get; set; }

        public ItemBase(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;

            Velocity = Vector2.Zero;

            bUseGravity = false;
            bHasCollision = false;
        }

        public Rectangle getDestinationRectangle()
        {
            return destinationRectangle;
        }

        public virtual string getName()
        {
            return "ItemBase";
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdatePhysical(gameTime);

            itemTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(itemTime - lastUpdateTime >= timePerFrame)
            {
                currentFrame = (currentFrame++) % maxFrameCnt;
                lastUpdateTime = itemTime;
            }
        }

        protected virtual void UpdatePhysical(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (bUseGravity)
            {
                Velocity += new Vector2(0, GravityScale * deltaTime);
            }
            position.X += Velocity.X * deltaTime;
            position.Y += Velocity.Y * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle((int)position.X + xOffset, (int)position.Y + yOffset, width, height);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle[currentFrame], Color.White);
        }

        public void moveX(int x)
        {
            xOffset += x;
        }

        public void moveY(int y)
        {
            yOffset += y;
        }

        public virtual void OnCollide()
        {
            Velocity = Vector2.Zero;
            GravityScale = 0.0f;
            bUseGravity = false;
        }

        public bool HasCollision()
        {
            return bHasCollision;
        }

        public virtual float GetLifeTime()
        {
            return float.MaxValue;
        }
    }
}
