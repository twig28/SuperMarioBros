using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class BowserSprite : ISprite
{
    private const int SpriteWidth = 150;
    private const int SpriteHeight = 100;
    private int SourceX = 80;
    private int SourceY = 210;
    private const int SourceWidth = 35;
    private const int SourceHeight = 32;
    private SpriteBatch sb;
    private Texture2D texture;
    private Rectangle DestinationRectangle;
    private Rectangle SourceRectangle;
    private const int spacingInterval = 40;
    private int currSprite = 0;

    public bool ChangeDirection { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }

    // Property to control sprite inversion
    public bool Invert { get; set; } = false;

    public Rectangle GetDestinationRectangle() => DestinationRectangle;

    public BowserSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        sb = SpriteBatch;
        posX = X;
        posY = Y;
        texture = Texture;
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
        SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
        ChangeDirection = false;
    }

    public void Draw()
    {
        DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);

        // Use SpriteEffects to invert if needed
        SpriteEffects effects = Invert ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        sb.Draw(texture, DestinationRectangle, SourceRectangle, Color.White, 0f, Vector2.Zero, effects, 0f);
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

    // Method to toggle sprite inversion
    public void ToggleInversion()
    {
        Invert = !Invert;
    }
}