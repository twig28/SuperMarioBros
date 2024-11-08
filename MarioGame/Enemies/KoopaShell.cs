using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    internal class KoopaShell : IEnemy
    {
        private const double Speed = 9.0;
        private const double CooldownDuration = 0.1;
        private KoopaShellSprite sprite;
        private int posX;
        private int posY;
        private bool isMoving = false;
        private bool _DefaultMoveMentDirection = true;
        private bool _alive = true;
        private double aliveTime = 0;

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

        public bool getIsMoving() => isMoving;

        public double getdeathStartTime => -1;

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

            aliveTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (isMoving) posX += _DefaultMoveMentDirection ? (int)Speed : -(int)Speed;

            posY += 5;
            sprite.posX = posX;
            sprite.posY = posY;
            sprite.Update(gameTime);
        }

        public void ChangeDirection()
        {
            _DefaultMoveMentDirection = !_DefaultMoveMentDirection;
        }

        public void Start(bool direction)
        {
            if (aliveTime >= CooldownDuration)
            {
                isMoving = true;
                if (!direction)
                {
                    ChangeDirection();
                }
            }
        }

        public void TriggerDeath(GameTime gameTime, bool stomped)
        {
            Alive = false;
        }
    }
}
