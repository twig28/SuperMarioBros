using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace MarioGame.Sprites
{
    internal class SpriteBase : ISprite
    {
        protected int sourceWidth = 35;
        protected int sourceHeight = 32;

        protected int spriteWidth = 150;
        protected int spriteHeight = 100;

        protected int sourceX = 80;
        protected int sourceY = 210;

        protected SpriteBatch spriteBatch;
        protected Texture2D texture;

        protected int spacingInterval = 40;
        protected int currSprite = 0;

        public Vector2 position { get; set; }

        protected Rectangle destinationRectangle;
        protected Rectangle sourceRectangle;

        protected int spriteCnt = 2;

        public bool bInvert { get; set; } = false; // Property to control inversion of the sprite

        public SpriteBase(Texture2D _texture, SpriteBatch _spriteBatch, Vector2 _position)
        {
            spriteBatch = _spriteBatch;
            texture = _texture;
            position = _position;
        }

        protected void Init()
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);
            sourceRectangle = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
        }

        public virtual void Update(GameTime gameTime)
        {
            currSprite = (currSprite + 1) % spriteCnt;
            sourceRectangle.X = sourceX + currSprite * spacingInterval;
        }

        public virtual void Draw()
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);

            SpriteEffects effects = bInvert ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, effects, 0);
        }

        public virtual Rectangle GetDestinationRectangle()
        {
            return destinationRectangle;
        }
    }
}
