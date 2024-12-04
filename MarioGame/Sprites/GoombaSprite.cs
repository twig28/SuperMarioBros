using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioGame.Sprites;

internal class GoombaSprite : SpriteBase
{
    int color = 1;
    public bool ChangeDirection { get; set; }

    public GoombaSprite(Texture2D _texture, SpriteBatch _spriteBatch, Vector2 _position, int pallette) : base(_texture, _spriteBatch, _position)
    {
        sourceWidth = 16;
        sourceHeight = 17;

        sourceX = 0;
        sourceY = 4;

        spriteWidth = 50;
        spriteHeight = 65;

        spacingInterval = 30;

        color = pallette;
        if(color == 2)
        {
            sourceY = 33;
        }

        Init();
        ChangeDirection = false;
    }

    public void SetDeathFrame()
    {
        if(color == 2)
        {
            sourceRectangle = new Rectangle(60, 34, sourceWidth, sourceHeight + 2);
        }
        else
        {
            sourceRectangle = new Rectangle(60, 7, sourceWidth, sourceHeight + 2);
        }
        currSprite = 2;
    }
}
