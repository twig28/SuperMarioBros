using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    public class MotionPlayerLeft : IPlayer
    {
        private Texture2D Texture { get; set; }
        public Vector2 Position;
        private Game1 Game;
        private float Scale = 3f;
        private float Speed;
        private GraphicsDeviceManager graphics;
        private int currentFrame;
        private int totalFrames;
        private float timePerFrame = 0.2f; // Time per frame in seconds
        private float timeCounter = 0f;
        public MotionPlayerLeft(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
            currentFrame = 0;
            totalFrames = 3;
            Game = game;
         

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
            //move for differnet status
            if (!Game.player_sprite.Big && !Game.player_sprite.Fire)
            {
                if (Position.X > 14 * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }
            else if (Game.player_sprite.Fire)
            {
                if (Position.X > 18 * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
                if (Position.X > 18 * Scale / 2) //check if reach to the top edge
                {

                    Position.X -= updatedSpeed;


                }
            }






        }

        public void Draw(SpriteBatch spriteBatch, int width, int height, float sourceScale, List<Rectangle> sourceRectangle)
        {
           
           // List<Rectangle> sourceRectangle = new List<Rectangle>();
            //check status

            if (Game.player_sprite.Fire)
            {
               /*
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(151, 122, 18, 32));
                sourceRectangle.Add(new Rectangle(128, 122, 18, 32));
                sourceRectangle.Add(new Rectangle(102, 122, 18, 32));
               */
                Position.Y = Position.Y - 22;


            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
               /*
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(150, 52, 18, 32));
                sourceRectangle.Add(new Rectangle(120, 52, 18, 32));
                sourceRectangle.Add(new Rectangle(89, 52, 18, 32));
               */
                Position.Y = Position.Y - 24;
            }
            else
            {
                /*
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(150, 0, 14, 16));
                sourceRectangle.Add(new Rectangle(120, 0, 14, 16));
                sourceRectangle.Add(new Rectangle(88, 0, 14, 16));
                */
            }
            spriteBatch.Draw(Texture, Position, sourceRectangle[currentFrame], Color.White, 0f, new Vector2(width / 2, height / 2), sourceScale, SpriteEffects.None, 0f);

        }
    }
}