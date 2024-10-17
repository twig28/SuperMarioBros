using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    internal class Koopa : IEnemy
    {
        private const double animInterval = 0.2;
        private const double DeathDuration = 2.0;
        private KoopaSprite sprite;
        private int posX;
        private int posY;
        private bool changeSpriteDirection = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;
        private double deathStartTime = -1;

        private bool _alive = true;
        private bool isShell = false;

        public int setPosX { set => posX = value; }
        public int setPosY { set => posY = value; }

        public Rectangle GetDestinationRectangle() => sprite.GetDestinationRectangle();

        public bool IsShell => isShell;
        public double getdeathStartTime => deathStartTime;

        public bool DefaultMoveMentDirection
        {
            get => _DefaultMoveMentDirection;
            set { _DefaultMoveMentDirection = value; changeSpriteDirection = true; }
        }

        public bool Alive
        {
            get => _alive;
            set => _alive = value;
        }

        private bool _DefaultMoveMentDirection = true;

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            posX = X;
            posY = Y;
            sprite = new KoopaSprite(Texture, SpriteBatch, posX, posY);
        }

        public void Draw()
        {
            if (Alive || isShell) sprite.Draw();
        }

        public void Update(GameTime gm)
        {
            if (deathStartTime > 0)
            {
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
                posY += 5;

                sprite.posX = posX;
                sprite.posY = posY;

                if (changeSpriteDirection)
                {
                    sprite.ChangeDirection = true;
                    changeSpriteDirection = false;
                }

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
            sprite.SetDeathFrame();
        }
    }
}
