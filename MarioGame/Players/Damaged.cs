using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class Damaged : IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public GraphicsDeviceManager graphics;
        public float Scale = 5f;
        private const int width = 14;
        private const int height = 16;
        private const int SX = 0;
        private const int SY = 16;
        private bool movedown;
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
 

                    Position.Y += updatedSpeed;

                
            
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(SX, SY, width, height);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }

      
    }
}
