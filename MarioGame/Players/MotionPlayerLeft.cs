﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    public class MotionPlayerLeft : IPlayer
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Scale = 5f;
        public float Speed;
        public GraphicsDeviceManager graphics;
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.2f; // Time per frame in seconds
        private float timeCounter = 0f;
         private const int width = 14;
        private const int height = 16;
        private List<Rectangle> sourceRectangle = new List<Rectangle>();
        private const int bigwidth = 18;
        private const int bigheight = 32;
        private List<Rectangle> bigsourceRectangle = new List<Rectangle>();
        public bool Big = false;

        private const int firewidth = 18;
        private const int fireheight = 32;
        private List<Rectangle> firesourceRectangle = new List<Rectangle>();
        public bool Fire = false;
        public MotionPlayerLeft(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
            currentFrame = 0;
            totalFrames = 3;
            sourceRectangle.Add(new Rectangle(150, 0, width, height));
            sourceRectangle.Add(new Rectangle(120, 0, width, height));
            sourceRectangle.Add(new Rectangle(88, 0, width, height));
            bigsourceRectangle.Add(new Rectangle(150, 52, bigwidth, bigheight));
            bigsourceRectangle.Add(new Rectangle(120, 52, bigwidth, bigheight));
            bigsourceRectangle.Add(new Rectangle(89, 52, bigwidth, bigheight));
            firesourceRectangle.Add(new Rectangle(151, 122, firewidth, fireheight));
            firesourceRectangle.Add(new Rectangle(128, 122, firewidth, fireheight));
            firesourceRectangle.Add(new Rectangle(102, 122, firewidth, fireheight));

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
            if (!Big && !Fire)
            {
                if (Position.X > width * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }
            else if (Fire)
            {
                if (Position.X > firewidth * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }
            else if (!Fire && Big)
            {
                if (Position.X > bigwidth * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }






        }

        public void Draw(SpriteBatch spriteBatch)
        {


            if (!Big && !Fire)
            {
                spriteBatch.Draw(Texture, Position, sourceRectangle[currentFrame], Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
            }
            else if (Fire)
            {

                spriteBatch.Draw(Texture, Position, firesourceRectangle[currentFrame], Color.White, 0f, new Vector2(firewidth / 2, fireheight / 2), Scale, SpriteEffects.None, 0f);
            }
            else if (!Fire && Big)
            {
                spriteBatch.Draw(Texture, Position, bigsourceRectangle[currentFrame], Color.White, 0f, new Vector2(bigwidth / 2, bigheight / 2), Scale, SpriteEffects.None, 0f);

            }
        }
    }
}