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
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;

        private bool alive = true;
        public int setPosX { set { posX = value; } }
        public int setPosY { set { posY = value; } }

        public Rectangle GetDestinationRectangle() { return sprite.GetDestinationRectangle(); }

        private bool _DefaultMoveMentDirection = true;
        public bool DefaultMoveMentDirection
        {
            get { return _DefaultMoveMentDirection; }
            set { _DefaultMoveMentDirection = value;}
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
                if (_DefaultMoveMentDirection)
                {
                    posX++;
                }
                else
                {
                    posX--;
                }
                //gravity
                posY = posY + 6;
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
            //Add Goomba Death Animation using Timer
        }

    }
}
