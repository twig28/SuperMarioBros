using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class Koopa : IEnemy
    {
        private const double animInterval = 0.2;
        private KoopaSprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;
        private bool changeSpriteDirection = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        private bool alive = true;
        private bool isShell = false;
        public int setPosX { set { posX = value; } }
        public int setPosY { set { posY = value; } }
        public Rectangle GetDestinationRectangle() { return sprite.GetDestinationRectangle(); }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        private bool _movingRight = true;
        public bool MovingRight
        {
            get { return _movingRight; }
            set { _movingRight = value; changeSpriteDirection = true; }
        }

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            posX = X; posY = Y;
            sprite = new KoopaSprite(Texture, SpriteBatch, posX, posY);
        }

        public void Draw()
        {
            if (alive || isShell) sprite.Draw();
        }

        public void Update(GameTime gm)
        {
            if (alive)
            {
                timeElapsed = gm.TotalGameTime.TotalSeconds;
                if (_movingRight)
                {
                    posX++;
                }
                else
                {
                    posX--;
                }
                sprite.posX = posX;
                sprite.posY = posY;
                if (changeSpriteDirection)
                {
                    changeSpriteDirection = false;
                    //signals change direction to sprite class
                    sprite.ChangeDirection = true;
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
                if (_movingRight)
                {
                    posX = posX + 3;
                }
                else
                {
                    posX = posX - 3;
                }
                sprite.posX = posX;
                sprite.posY = posY;
                if (changeSpriteDirection)
                {
                    changeSpriteDirection = false;
                }
                sprite.Update(gm);
            }
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            alive = false;
            //handle using sprite
            if (stomped)
            {
                isShell = true;
                //because of animation of changing to shell
                sprite.ChangeToShell(gm);
                bool changed = false;
                while (!changed)
                {
                    changed = sprite.ChangeToShell(gm); ;
                }
            }

            sprite.Update(gm);
        }
    }
}
