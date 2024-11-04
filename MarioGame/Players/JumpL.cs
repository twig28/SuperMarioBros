using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class JumpL : IPlayer
    {


        private Texture2D Texture { get; set; }
        public Vector2 Position;
        private Game1 Game;
        private float jumpSpeed = -12f;
        private float gravity = 0.3f;     
        public JumpL(Texture2D texture, Vector2 position, Game1 game)
        {
            Texture = texture;
            Position = position;
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

        public void Draw(SpriteBatch spriteBatch,int width,int height,float Scale, List<Rectangle> sourceRectangle, int pos_difference,Color c)
        {
            
            Position.Y -= pos_difference;
            spriteBatch.Draw(Texture, Position, sourceRectangle[0], c, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);

        }
    }
}
