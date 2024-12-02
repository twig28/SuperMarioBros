using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class Bowser : IEnemy
{
    private double animInterval = 0.2;
    private GoombaSprite sprite;
    private int posX;
    private int posY;
    private double timeElapsed = 0;
    private double timeElapsedSinceUpdate = 0;

    private bool _alive = true;
    private double deathStartTime = 0;
    private const double DeathDuration = 2.0;

    private bool _DefaultMoveMentDirection = false;

    public int setPosX { set { posX = value; } }
    public int setPosY { set { posY = value; } }

    public bool DefaultMoveMentDirection
    {
        get { return _DefaultMoveMentDirection; }
        set { _DefaultMoveMentDirection = value; }
    }

    public bool Alive
    {
        get { return _alive; }
        set { _alive = value; }
    }

    public Rectangle GetDestinationRectangle() { return sprite.GetDestinationRectangle(); }
    public double getdeathStartTime => deathStartTime;


    public Bowser(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y, int pallette)
    {
        posX = X; posY = Y;
        sprite = new GoombaSprite(Texture, SpriteBatch, posX, posY, pallette);
    }

    public void Draw()
    {
        if (Alive) sprite.Draw();
    }

    private bool isInvertedDeath = false; // Flag for inverted death animation

    public void TriggerDeath(GameTime gm, bool stomped)
    {
        if (stomped)
        {
            // Stomped death behavior (remain in the same place)
            deathStartTime = gm.TotalGameTime.TotalSeconds;
            sprite.posY = posY + 40;
            sprite.SetDeathFrame();
        }
        else
        {
            // Fireball death behavior (inverted fall animation)
            deathStartTime = gm.TotalGameTime.TotalSeconds;
            isInvertedDeath = true;      // Enable inverted fall
            sprite.Invert = true;         // Invert the sprite to show it flipped
        }
    }

    public void Update(GameTime gm)
    {
        if (deathStartTime > 0)
        {
            if (isInvertedDeath)
            {
                // Check if 3 seconds have passed since the death animation started
                if (gm.TotalGameTime.TotalSeconds - deathStartTime < 3.0)
                {
                    posY += 10; 
                    sprite.posX = posX;
                    sprite.posY = posY;
                }
                else
                {
                    // End the death animation after 3 seconds
                    Alive = false;
                    sprite.Invert = false; // Reset inversion after animation ends
                    isInvertedDeath = false;
                }
            }
            else
            {
                // Stomped death behavior remains in place
                if (gm.TotalGameTime.TotalSeconds - deathStartTime >= DeathDuration)
                {
                    Alive = false;
                    return;
                }
            }
        }
        else
        {
            // Regular movement and animation logic
            timeElapsed = gm.TotalGameTime.TotalSeconds;
            posX += _DefaultMoveMentDirection ? 1 : -1;
            posY += 6;  // Gravity for normal movement

            sprite.posX = posX;
            sprite.posY = posY;

            if (timeElapsed - timeElapsedSinceUpdate > animInterval)
            {
                timeElapsedSinceUpdate = timeElapsed;
                sprite.Update(gm);
            }
        }
    }
}
