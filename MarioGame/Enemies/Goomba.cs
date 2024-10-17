using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class Goomba : IEnemy
{
    private double animInterval = 0.2;
    private GoombaSprite sprite;
    private int posX;
    private int posY;
    private double timeElapsed = 0;
    private double timeElapsedSinceUpdate = 0;

    private bool _alive = true;
    private double deathStartTime = -1; 
    private const double DeathDuration = 2.0; 

    private bool _DefaultMoveMentDirection = true;

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


    public Goomba(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        posX = X; posY = Y;
        sprite = new GoombaSprite(Texture, SpriteBatch, posX, posY);
    }

    public void Draw()
    {
        if (Alive) sprite.Draw();
    }

    public void Update(GameTime gm)
    {
        if (deathStartTime > 0)
        {
            // Check if 2 seconds have passed since the death animation started
            if (gm.TotalGameTime.TotalSeconds - deathStartTime >= DeathDuration)
            {
                Alive = false;
                return;  
            }
        }

        else
        {
            timeElapsed = gm.TotalGameTime.TotalSeconds;
            posX += _DefaultMoveMentDirection ? 1 : -1; 
            posY += 6;  //Gravity 

            sprite.posX = posX;
            sprite.posY = posY;

            if (timeElapsed - timeElapsedSinceUpdate > animInterval)
            {
                timeElapsedSinceUpdate = timeElapsed;
                sprite.Update(gm);
            }
        }
    }

    public void TriggerDeath(GameTime gm, bool stomped)
    {
        deathStartTime = gm.TotalGameTime.TotalSeconds;
        sprite.posY = posY + 40;
        sprite.SetDeathFrame();  
    }
}
