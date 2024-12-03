using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    internal class Koopa : IEnemy
    {
        private const double animInterval = 0.2;
        private const double DeathDuration = 0.1;
        private KoopaSprite sprite;
        private int posX;
        private int posY;
        private bool changeSpriteDirection = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;
        private double deathStartTime = 0;
        private KoopaShell KoopaShell;

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

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y, int color)
        {
            posX = X;
            posY = Y;
            sprite = new KoopaSprite(Texture, SpriteBatch, posX, posY, color);
            KoopaShell = new KoopaShell(Texture, SpriteBatch, posX, posY, color);
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
                if (changeSpriteDirection)
                {
                    sprite.ChangeDirection = true;
                    changeSpriteDirection = false;
                }

                timeElapsed = gm.TotalGameTime.TotalSeconds;
                posX += _DefaultMoveMentDirection ? -1 : 1;
                posY += 5;

                sprite.posX = posX;
                sprite.posY = posY;

                if (timeElapsed - timeElapsedSinceUpdate > animInterval)
                {
                    timeElapsedSinceUpdate = timeElapsed;
                    sprite.Update(gm);
                }
            }
        }

        public KoopaShell SpawnKoopa(GameTime gm)
        {
            this.TriggerDeath(gm, true);
            KoopaShell.setPosX = posX;
            KoopaShell.setPosY = posY;
            return KoopaShell;
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            sprite.posY = posY + 30;
            deathStartTime = gm.TotalGameTime.TotalSeconds;
            sprite.SetDeathFrame();
        }
    }
}
