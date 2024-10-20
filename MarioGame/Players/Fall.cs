using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Collisions;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class Fall : IPlayer
    {


        private Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed = 1f;
        private GraphicsDeviceManager graphics;
        private Game1 Game;

        public Fall(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            Texture = texture;
            Position = position;
            graphics = Graphics;
            Game = game;
        }

        public void Update(GameTime gameTime)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Game.player_sprite.isGrounded)
            {
                Game.player_sprite.isFalling = false;

            }
            if (Game.player_sprite.isFalling)
            {
                Speed += 1f;
                Position.Y += Speed;
                
            }
           
        }
        public void Draw(SpriteBatch spriteBatch,int width,int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference)
        {
            Position.Y -= pos_difference;
            spriteBatch.Draw(Texture, Position, sourceRectangle[0], Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }


    }
}
