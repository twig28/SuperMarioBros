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
    private Game1 Game;
    public Vector2 Position;
    private float Scale = 3f;
    private float Speed;
    private GraphicsDeviceManager graphics;
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

        public void Update(GameTime gm, PlayerSprite mario)
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

           
            if(mario.direction)
            {
                //move for differnet status
                if (mario.mode == PlayerSprite.Mode.None || mario.mode == PlayerSprite.Mode.invincible)
                {
                    if (Position.X > 14 * Scale / 2) //check if reach to the top edge
                    {

                        Position.X -= updatedSpeed;


                    }
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    if (Position.X > 18 * Scale / 2) //check if reach to the top edge
                    {

                        Position.X -= updatedSpeed;


                    }
                }
                else if (mario.mode == PlayerSprite.Mode.Big || mario.mode == PlayerSprite.Mode.Star)
                {
                    if (Position.X > 18 * Scale / 2) //check if reach to the top edge
                    {

                        Position.X -= updatedSpeed;


                    }
                }

            }
            else
            {
                Position.X += updatedSpeed;

            }

        }




        public void Draw(SpriteBatch spriteBatch,int width, int height, float sourceScale, List<Rectangle> sourceRectangle, int pos_difference,Color c)
        {
            
           
        Position.Y -= pos_difference;
        spriteBatch.Draw(Texture, Position, sourceRectangle[currentFrame], c, 0f, new Vector2(width / 2, height / 2), sourceScale, SpriteEffects.None, 0f);

            

        }
    }
}