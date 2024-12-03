using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class Bowser : IEnemy
{
    private double animInterval = 0.2;
    private BowserSprite sprite;
    private int posX;
    private int posY;
    private double timeElapsed = 0;
    private double timeElapsedSinceUpdate = 0;

    private bool _alive = true;
    private double deathStartTime = 0;

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


    public Bowser(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        posX = X; posY = Y;
        sprite = new BowserSprite(Texture, SpriteBatch, posX, posY);
    }

    public void Draw()
    {
        if (Alive) sprite.Draw();
    }

    private bool isInvertedDeath = false; // Flag for inverted death animation

    public void TriggerDeath(GameTime gm, bool stomped)
    {

    }

    public void Update(GameTime gm)
    {
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
