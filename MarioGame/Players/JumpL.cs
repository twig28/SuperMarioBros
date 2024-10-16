using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Collisions;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class JumpL : IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        private GraphicsDeviceManager graphics;
        public Game1 Game;
       // public float Scale = 3f;
        //for jump
        float jumpSpeed = -10f;
        float gravity = 0.3f;     
        float groundLevel;
        public JumpL(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            Texture = texture;
            Position = position;
            graphics = Graphics;
            groundLevel = Graphics.PreferredBackBufferHeight - 95;
            Game = game;
        }

        public void Update(GameTime gameTime)
        {

            //for action jumping
            if (Game.player_sprite.isGrounded)
            {
                Game.player_sprite.isJumping = true;
                Game.player_sprite.isGrounded = false;
                Game.player_sprite.velocity = jumpSpeed;
                Game.player_sprite.current = PlayerSprite.SpriteType.StaticL;
            }


            if (Game.player_sprite.isJumping)
            {
                Game.player_sprite.velocity += gravity;
                Position.Y += Game.player_sprite.velocity;


            }


        }

        public void Draw(SpriteBatch spriteBatch,int width,int height,float Scale, List<Rectangle> sourceRectangle)
        {
            //check status
           // Rectangle sourceRectangle = new Rectangle(29, 0, 17, 17);
           
            if (Game.player_sprite.Fire)
            {
               
               // sourceRectangle = new Rectangle(25, 122, 18, 32);
                Position.Y -= 22;
            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
               
             //   sourceRectangle = new Rectangle(29, 52, 18, 32);
                Position.Y -= 24;

            }
            spriteBatch.Draw(Texture, Position, sourceRectangle[0], Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);

        }
    }
}
