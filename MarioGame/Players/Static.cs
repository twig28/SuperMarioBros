using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class IPlayer : ISprite
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Scale = 0.2f;

        public IPlayer(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            //static don't need to update status
        }
    }
}
