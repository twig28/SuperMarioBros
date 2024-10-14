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
        public Game1 Game;
        public float Scale = 5f;
        private const int width = 17;
        private const int height = 17;
        private const int SX = 29;
        private const int SY = 0;
        private const int bigwidth = 18;
        private const int bigheight = 32;
        private const int bigSX = 29;
        private const int bigSY = 52;
        public bool Big = false;

        private const int firewidth = 19;
        private const int fireheight = 32;
        private const int fireSX = 25;
        private const int fireSY = 122;
        public bool Fire = false;
        //for jump
        float jumpSpeed = -10f;
        float gravity = 0.5f;     
        float velocity = 0f;    
        bool isJumping = false;   
        bool isGrounded = true;   
        float groundLevel;
        public JumpL(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            graphics = Graphics;
            groundLevel = Graphics.PreferredBackBufferHeight - 95;
            Game = game;
        }

        public void Update(GameTime gameTime)
        {

            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //for action jumping
            if (isGrounded)
            {
                isJumping = true;
                isGrounded = false;
                velocity = jumpSpeed;
            }

          
            if (isJumping)
            {
                velocity += gravity;
                Position.Y += velocity;

                if (Position.Y >= groundLevel)
                {
                    Position.Y = groundLevel;
                    Game.player_sprite.current = PlayerSprite.SpriteType.StaticL;

                    isGrounded = true;
                    isJumping = false;
                    velocity = 0f;
                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //check status

            if (!Big && !Fire)
            {
                Rectangle sourceRectangle = new Rectangle(SX, SY, width, height);
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
            }
            else if (Fire)
            {
                Rectangle firesourceRectangle = new Rectangle(fireSX, fireSY, firewidth, fireheight);
                spriteBatch.Draw(Texture, Position, firesourceRectangle, Color.White, 0f, new Vector2(firewidth / 2, fireheight / 2), Scale, SpriteEffects.None, 0f);
            }
            else if (!Fire && Big)
            {
                Rectangle bigsourceRectangle = new Rectangle(bigSX, bigSY, bigwidth, bigheight);
                spriteBatch.Draw(Texture, Position, bigsourceRectangle, Color.White, 0f, new Vector2(bigwidth / 2, bigheight / 2), Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
