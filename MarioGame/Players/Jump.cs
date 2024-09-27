﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class Jump : IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public GraphicsDeviceManager graphics;
        public float Scale = 5f;
        private const int width = 17;
        private const int height = 17;
        private const int SX = 358;
        private const int SY = 0;
        private const int bigwidth = 18;
        private const int bigheight = 32;
        private const int bigSX = 358;
        private const int bigSY = 52;
        public bool Big = false;
        private bool jumporfall = true;
        public Jump(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 JumpPosition = Position;
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            JumpPosition.Y = JumpPosition.Y - height;

            if (jumporfall)
            {
                Position.Y -= updatedSpeed;

                if (Position.Y < height * Scale / 2 || Position.Y < JumpPosition.Y) //check if reach to the top edge
                {

                    jumporfall = false; // Change direction to move down

                }

            }
            else
            {
                Position.Y += updatedSpeed;
                if (Position.Y > graphics.PreferredBackBufferHeight - (height * Scale / 2))//check if reach to the bottom edge
                {
                    jumporfall = true;// Change direction to move up

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Big)
            {
                Rectangle sourceRectangle = new Rectangle(SX, SY, width, height);
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
            }
            else
            {
                Rectangle bigsourceRectangle = new Rectangle(bigSX, bigSY, bigwidth, bigheight);
                spriteBatch.Draw(Texture, Position, bigsourceRectangle, Color.White, 0f, new Vector2(bigwidth / 2, bigheight / 2), Scale, SpriteEffects.None, 0f);
            }
        }
    }
}

