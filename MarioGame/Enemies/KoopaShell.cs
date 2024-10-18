using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    internal class KoopaShell : IEnemy
    {
        private const double Speed = 9.0;  // 3x the original Koopa speed
        private KoopaShellSprite sprite;
        private int posX;
        private int posY;
        private bool isMoving = false;  // Tracks if the shell is in motion
        private bool _DefaultMoveMentDirection = true;
        private bool _alive = true;

        public int setPosX { set => posX = value; }
        public int setPosY { set => posY = value; }

        public bool DefaultMoveMentDirection
        {
            get => _DefaultMoveMentDirection;
            set => _DefaultMoveMentDirection = value;
        }

        public bool Alive
        {
            get => _alive;
            set => _alive = value;
        }

        public double getdeathStartTime => -1;  // Not needed for the shell

        public Rectangle GetDestinationRectangle() => sprite.GetDestinationRectangle();

        public KoopaShell(Texture2D texture, SpriteBatch spriteBatch, int x, int y)
        {
            posX = x;
            posY = y;
            sprite = new KoopaShellSprite(texture, spriteBatch, posX, posY);
        }

        public void Draw()
        {
            if (Alive) sprite.Draw();
        }

        public void Update(GameTime gameTime)
        {
            if (!Alive) return;

            if (isMoving)
            {
                posX += _DefaultMoveMentDirection ? (int)Speed : -(int)Speed;
            }

            posY += 5;

            sprite.posX = posX;
            sprite.posY = posY;

            sprite.Update(gameTime);
        }

        public void ChangeDirection()
        {
            _DefaultMoveMentDirection = !_DefaultMoveMentDirection;
        }

        public void Stop()
        {
            isMoving = false;
        }

        public void Start()
        {
            isMoving = true;
        }

        public void TriggerDeath(GameTime gameTime, bool stomped)
        {
            Alive = false;  // Once dead, the shell disappears.
        }
    }
}
