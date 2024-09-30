using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class StaticL : IPlayer
    {


        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Scale = 5f;
        private const int width = 14;
        private const int height = 16;
        private const int SX = 180;
        private const int SY = 0;
        public bool Big = false;
        private const int bigwidth = 18;
        private const int bigheight = 32;
        private const int bigSX = 179;
        private const int bigSY = 52;
        private const int firewidth = 16;
        private const int fireheight = 32;
        private const int fireSX = 180;
        private const int fireSY = 122;
        public bool Fire = false;
        public StaticL(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;

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

        public void Update(GameTime gameTime)
        {
            //static don't need to update status
        }
    }
}
