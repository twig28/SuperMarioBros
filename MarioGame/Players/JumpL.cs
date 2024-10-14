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
        //for jump
        float jumpSpeed = -10f;
        float gravity = 0.3f;     
        public float velocity = 0f;    
        public bool isJumping = false;   
        public bool isGrounded = true;   
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
                Game.player_sprite.current = PlayerSprite.SpriteType.StaticL;
            }


            if (isJumping)
            {
                velocity += gravity;
                Position.Y += velocity;


            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //check status
            Rectangle sourceRectangle = new Rectangle(29, 0, 17, 17);
            int width = 14;
            int height = 16;
            if (Game.player_sprite.Fire)
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(25, 122, 18, 32);
                Position.Y -= 40;
            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(29, 52, 18, 32);
                Position.Y -= 40;

            }
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);

        }
    }
}
