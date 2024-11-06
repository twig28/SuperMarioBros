using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class GoombaSprite : ISprite
{
    private const int SpriteWidth = 50;
    private const int SpriteHeight = 65;
    private int SourceX = 0;
    private int SourceY = 4;
    private const int SourceWidth = 16;
    private const int SourceHeight = 17;
    private SpriteBatch sb;
    private Texture2D texture;
    private Rectangle DestinationRectangle;
    private Rectangle SourceRectangle;
    private const int spacingInterval = 30;
    private int currSprite = 0;

    public bool ChangeDirection { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }

    public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

    public GoombaSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        sb = SpriteBatch;
        posX = X; posY = Y;
        texture = Texture;
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
        SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
        ChangeDirection = false;
    }

    public void Draw()
    {
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
        sb.Draw(texture, DestinationRectangle, SourceRectangle, Color.White);
    }

    public void Update(GameTime gm)
    {
        if (currSprite == 0)
        {
            SourceRectangle.X += spacingInterval;
            currSprite = 1;
        }
        else
        {
            SourceRectangle.X -= spacingInterval;
            currSprite = 0;
        }
    }

    public void SetDeathFrame()
    {
        // Switch to the death frame on the texture (X = 60, Y = 7)
        SourceRectangle = new Rectangle(60, 7, SourceWidth, SourceHeight + 2);
        currSprite = 2;
    }
}
