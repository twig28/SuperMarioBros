using MarioGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class Flagpole : IBlock
{
    public Vector2 Position { get; set; }
    public bool IsSolid { get; }
    public bool IsBreakable { get; }
    public bool IsCollided { get; set; }

    public bool isFinished = false;

    private double timeElapsed = 0;

    private const double totalLoweringTime = 5.0;
    private const int flagInitialY = 0; 
    private const int flagFinalY = 500; 

    protected Texture2D PoleTexture { get; set; }
    protected Texture2D FlagTexture { get; set; }
    private Rectangle sourceRectangle = new Rectangle(38, 0, 90, 539);
    private Rectangle FlagsourceRectangle = new Rectangle(117, 707, 15, 16);
    protected Rectangle DestinationRectangle;
    protected Rectangle FlagDestinationRectangle;

    private Vector2 marioStartingPosition; 
    private int marioEndingY;
    private int marioY;

    public Flagpole(Vector2 position, Texture2D flagTexture, Texture2D poleTexture)
    {
        Position = position;
        PoleTexture = poleTexture;
        FlagTexture = flagTexture;
        DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, 75, 500);
        FlagDestinationRectangle = new Rectangle((int)position.X - 30, (int)position.Y + 30, 50, 50);
        IsCollided = false;
        marioEndingY = 450;
    }

    public void OnCollide()
    {
        if (!IsCollided)
        {
            IsCollided = true;
        }
    }

    public void setMarioStartPosition(Vector2 position)
    {
        marioStartingPosition = position;
    }

    private int CalculateLinearYPosition(double elapsedTime, int startingY, int finalY)
    {
        if (elapsedTime >= totalLoweringTime)
        {
            isFinished = true;
        }

        double t = elapsedTime / totalLoweringTime; // Normalized time [0, 1]
        int result = (int)(startingY + t * (finalY - startingY));
        return result;
    }

    public Vector2 getMarioPosition()
    {
        return new Vector2(marioStartingPosition.X, marioStartingPosition.Y);
    }

    public void Update(GameTime gameTime)
    {
        if (!IsCollided || isFinished) return;

        timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        int flagY = CalculateLinearYPosition(timeElapsed, flagInitialY, flagFinalY);
        if(flagY < 125)
        {
            flagY = 125;
        }
        FlagDestinationRectangle = new Rectangle(FlagDestinationRectangle.X, flagY, FlagDestinationRectangle.Width, FlagDestinationRectangle.Height);
    }

    public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // Draw the flagpole and the flag
        spriteBatch.Draw(PoleTexture, DestinationRectangle, sourceRectangle, Color.White);
        spriteBatch.Draw(FlagTexture, FlagDestinationRectangle, FlagsourceRectangle, Color.White);
    }
}
