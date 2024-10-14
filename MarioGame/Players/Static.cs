    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class Static: IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public Game1 Game;
        public float Scale = 5f;
       
        public Static(Texture2D texture, Vector2 position,Game1 game)
        {
            Texture = texture;
            Position = position;
            Game = game;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //check status

           int width = 14;
           int height = 16;
           Rectangle sourceRectangle = new Rectangle(210, 0, 14, 16);

            if (Game.player_sprite.Fire)
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(209, 122, 18, 32);
            }
            else if(!Game.player_sprite.Fire && Game.player_sprite.Big) 
            {
                width = 18;
                height = 32;
                sourceRectangle = new Rectangle(208, 52, 18, 32);
            }
           
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);


        }

        public void Update(GameTime gameTime)
        {
            //static don't need to update status
        }
    }
}
