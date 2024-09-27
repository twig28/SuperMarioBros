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
        public float Scale = 5f;
        private const int width = 14;
        private const int height = 16;
        private const int SX = 210;
        private const int SY = 0;
        private const int bigwidth = 18;
        private const int bigheight = 32;
        private const int bigSX = 208;
        private const int bigSY = 52;
        public bool Big = false;
        public Static(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Big)
            {
                Rectangle sourceRectangle = new Rectangle(SX, SY, width, height);
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);
            }
            else
            {
                Rectangle bigsourceRectangle = new Rectangle(bigSX, bigSY, bigwidth, bigheight);
                spriteBatch.Draw(Texture, Position, bigsourceRectangle, Color.White, 0f, new Vector2(bigwidth / 2, bigheight / 2), Scale, SpriteEffects.None, 0f);
            }

                    
        }

        public void Update(GameTime gameTime)
        {
            //static don't need to update status
        }
    }
}
