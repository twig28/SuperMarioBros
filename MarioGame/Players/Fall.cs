using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
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
        private float Scale = 3f;

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
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            int width = 14;
            int height = 16;
            if (Game.player_sprite.left)
            {
                sourceRectangle = new Rectangle(29, 0, 17, 17);
                if (Game.player_sprite.Fire)
                {
                    width = 18;
                    height = 32;
                    sourceRectangle = new Rectangle(25, 122, 18, 32);
                    Position.Y -= 22;
                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
                {
                    width = 18;
                    height = 32;
                    sourceRectangle = new Rectangle(29, 52, 18, 32);
                    Position.Y -= 24;

                }
            }
            else {
            sourceRectangle = new Rectangle(358, 0, 17, 17);
            //check status

            if (Game.player_sprite.Fire)
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(361, 122, 18, 32);
                Position.Y -= 22;

            }
            else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(358, 52, 18, 32);
                Position.Y -= 24;

            }
            }
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
        }


    }
}
