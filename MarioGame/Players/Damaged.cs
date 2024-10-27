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
    public class Damaged : IPlayer
    {


        private Texture2D Texture { get; set; }
        public Vector2 Position;
        private float Speed;
        private GraphicsDeviceManager graphics;
        public Damaged(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
        }

        public void Update(GameTime gameTime)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Position.Y += 5*updatedSpeed;

                
            
            
        }
        public void Draw(SpriteBatch spriteBatch,int width, int height,float Scale, List<Rectangle> sourceRectangle, int pos_difference,Color c)
        {
            spriteBatch.Draw(Texture, Position, sourceRectangle[0], Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }

      
    }
}
