using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame { 
public class MotionPlayer : IPlayer
    {
    public Texture2D Texture { get; set; }
        public Game1 Game;
        public Vector2 Position;
    private float Scale = 5f;
    private float Speed;
    public GraphicsDeviceManager graphics;
    private int currentFrame;
    private int totalFrames;
    private float timePerFrame = 0.2f; // Time per frame in seconds
    private float timeCounter = 0f;
        public MotionPlayer(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
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
            //for running with different status
            if (!Game.player_sprite.Big && !Game.player_sprite.Fire)
            {
                if (Position.X < graphics.PreferredBackBufferWidth - (14 * Scale / 2))//check if reach to the bottom edge
                {
                    Position.X += updatedSpeed;

                }
            }
            else if (Game.player_sprite.Fire)
            {
                if (Position.X < graphics.PreferredBackBufferWidth - (18 * Scale / 2))//check if reach to the bottom edge
                {
                    Position.X += updatedSpeed;

                }
            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
                if (Position.X < graphics.PreferredBackBufferWidth - (18 * Scale / 2))//check if reach to the bottom edge
                {
                    Position.X += updatedSpeed;

                }
            }


        }




        public void Draw(SpriteBatch spriteBatch)
        {
            int width;
            int height;
            //check status
            List<Rectangle> sourceRectangle = new List<Rectangle>();

            if (Game.player_sprite.Fire)
            {
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(237, 122, 18, 32));
                sourceRectangle.Add(new Rectangle(263, 122, 18, 32));
                sourceRectangle.Add(new Rectangle(287, 122, 18, 32));
                width = 18;
                height = 32;
                Position.Y = Position.Y - 40;


            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(238, 52, 18, 32));
                sourceRectangle.Add(new Rectangle(270, 52, 18, 32));
                sourceRectangle.Add(new Rectangle(299, 52, 18, 32));
                width = 18;
                height = 32;
                Position.Y = Position.Y - 40;

            }
            else
            {
             width = 14;
             height = 16;
             sourceRectangle.Clear();
             sourceRectangle.Add(new Rectangle(240, 0, 14, 16));
             sourceRectangle.Add(new Rectangle(270, 0, 14, 16));
             sourceRectangle.Add(new Rectangle(300, 0, 14, 16));
            }
        spriteBatch.Draw(Texture, Position, sourceRectangle[currentFrame], Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);

            

        }
    }
}