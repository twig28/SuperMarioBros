using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class Goomba : IEnemy
    {
        private double animInterval = 0.2;
        private GoombaSprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        private bool alive = true;
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public int getPosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int getPosY
        {
            get { return posY; }
            set { posY = value; }
        }

        private bool _movingRight = true;
        public bool MovingRight
        {
            get { return _movingRight; }
            set { _movingRight = value;}
        }

        public Goomba(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            posX = X; posY = Y;
            sprite = new GoombaSprite(Texture, SpriteBatch, posX, posY);
        }

        public void Draw()
        {
            if (alive) sprite.Draw();
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
                if (timeElapsed - timeElapsedSinceUpdate > animInterval)
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
        }

    }
}
