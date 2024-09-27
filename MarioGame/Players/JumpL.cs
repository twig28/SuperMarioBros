using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class JumpL : IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public GraphicsDeviceManager graphics;
        public float Scale = 5f;
        private const int width = 17;
        private const int height = 17;
        private const int SX = 29;
        private const int SY = 0;
        private bool jumporfall = true;
        public JumpL(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
        }

        public void Update(GameTime gameTime)
        {
            float jumpheight = height;

            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (jumporfall)
            {
                Position.Y -= updatedSpeed;

                if (Position.Y < height * Scale / 2 || Position.Y < jumpheight) //check if reach to the top edge
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
            Rectangle sourceRectangle = new Rectangle(SX, SY, width, height);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }
    }
}
