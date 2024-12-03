using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class BowserSprite : ISprite
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

    public BowserSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        sb = SpriteBatch;
        posX = X; posY = Y;
        texture = Texture;
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
        SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
        ChangeDirection = false;
    }

    public bool Invert { get; set; } = false; // Property to control inversion of the sprite
    public void Draw()
    {
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);

        // Apply inversion by using a sprite effect
        SpriteEffects effects = Invert ? SpriteEffects.FlipVertically : SpriteEffects.None;
        sb.Draw(texture, DestinationRectangle, SourceRectangle, Color.White, 0, Vector2.Zero, effects, 0);
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
}
