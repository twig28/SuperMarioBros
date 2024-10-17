using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame
{
    internal class Koopa : IEnemy
    {
        private const double animInterval = 0.2;
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
            if (Alive)
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
            else if (isShell)
            {
                timeElapsed = gm.TotalGameTime.TotalSeconds;
                posX += _DefaultMoveMentDirection ? 3 : -3;

                sprite.posX = posX;
                sprite.posY = posY;

                sprite.Update(gm);
            }
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            Alive = false; 

            if (stomped)
            {
                isShell = true;

                // Transition to shell sprite animation
                bool changed = false;
                while (!changed)
                {
                    changed = sprite.ChangeToShell(gm);
                }
            }

            sprite.Update(gm);
        }
    }
}
