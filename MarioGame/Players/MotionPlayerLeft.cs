using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    public class MotionPlayerLeft : IPlayer
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Scale = 0.2f;
        public float Speed;
        public GraphicsDeviceManager graphics;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.2f; // Time per frame in seconds
        private float timeCounter = 0f;
        public MotionPlayerLeft(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, int rows, int columns)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(GameTime gm)
        {
            //make spirte animated with a fixed period
            float updatedSpeed = Speed * (float)gm.ElapsedGameTime.TotalSeconds;
            timeCounter += (float)gm.ElapsedGameTime.TotalSeconds;

            if (timeCounter >= timePerFrame)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;

                timeCounter -= timePerFrame;
            }
            //move
            
            
                Position.X -= updatedSpeed;

              


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }
    }
}