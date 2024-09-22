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
        private double animInterval;
        private KoopaSprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;
        private bool changeSpriteDirection = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        private bool alive = true;
        public bool Alive 
        {
            get { return alive; }
            set { alive = value; }
        }

        private bool _movingRight = true;
        public bool MovingRight
        {
            get { return _movingRight;}
            set { _movingRight = value; changeSpriteDirection = true; }
        }

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            posX = X; posY = Y;
            sprite = new KoopaSprite(Texture, SpriteBatch, posX, posY);
        }

        public void Draw()
        {
            if(alive) sprite.Draw();
        }

        public void Update(GameTime gm)
        {
            if (alive) { 
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
                if (timeElapsed - timeElapsedSinceUpdate > 0.2)
                {
                    timeElapsedSinceUpdate = timeElapsed;
                    sprite.Update(gm);
                }
            }
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            alive = false;
            sprite.Update(gm);
            //handle using sprite
        }
    }
}
