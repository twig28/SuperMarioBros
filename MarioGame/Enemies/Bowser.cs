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

    private double jumpTimer = 0;
    private bool isJumping = false;
    private float verticalVelocity = 0;
    private const float gravity = 6f;
    private const float initialJumpVelocity = -10f;

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
    public Rectangle GetDestinationRectangle() => sprite.GetDestinationRectangle();

    public double getdeathStartTime => deathStartTime;

    public Bowser(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
    {
        posX = X;
        posY = Y;
        sprite = new BowserSprite(Texture, SpriteBatch, new Vector2(posX, posY));
    }

    public void Draw()
    {
        if (Alive) sprite.Draw();
    }

    public void ground()
    {
        verticalVelocity = 0;
        isJumping = false;
        jumpTimer = 0;
    }

    public void TriggerDeath(GameTime gm, bool stomped)
    { }

    public void changeDirection()
    {
        _DefaultMoveMentDirection = !_DefaultMoveMentDirection;
        sprite.bInvert = !sprite.bInvert;
    }

    public void detectMarioChange(Rectangle marioRect)
    {
        if (marioRect.X < this.sprite.GetDestinationRectangle().X && _DefaultMoveMentDirection)
        {
            changeDirection();
        }
        else if (marioRect.X > this.sprite.GetDestinationRectangle().X && !_DefaultMoveMentDirection)
        {
            changeDirection();
        }
    }

    public void Update(GameTime gm)
    {
        timeElapsed = gm.TotalGameTime.TotalSeconds;

        jumpTimer += gm.ElapsedGameTime.TotalSeconds;
        if (jumpTimer >= 3 && !isJumping)
        {
            isJumping = true;
            verticalVelocity = initialJumpVelocity;
            jumpTimer = 0;
        }

        if (isJumping)
        {
            verticalVelocity += gravity * (float)gm.ElapsedGameTime.TotalSeconds;
            posY += (int)verticalVelocity;
        }

        posX += _DefaultMoveMentDirection ? 1 : -1;
        sprite.position = new Vector2(posX, posY);

        if (timeElapsed - timeElapsedSinceUpdate > animInterval)
        {
            timeElapsedSinceUpdate = timeElapsed;
            sprite.Update(gm);
        }
    }
}
