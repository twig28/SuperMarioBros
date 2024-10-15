using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Sprites;

namespace MarioGame
{
    internal class Piranha : IEnemy
    {
        private double movingInterval = 2;
        private double animInterval = 0.4;
        private PiranhaSprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;
        private double timeElapsed = 0;
        private double timeElapsedSinceUpdate = 0;
        private double timeElapsedSinceUpdateAnim = 0;
        private bool alive = true;

        private bool _DefaultMoveMentDirection = false;
        public bool DefaultMoveMentDirection
        {
            get { return _DefaultMoveMentDirection; }
            set { _DefaultMoveMentDirection = value; }
        }
        public int setPosX{set { posX = value; }}

        public int setPosY { set { posY = value; }}

        public Rectangle GetDestinationRectangle() { return sprite.GetDestinationRectangle(); }

        public Piranha(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            posX = X; posY = Y;
            sprite = new PiranhaSprite(Texture, SpriteBatch, posX, posY);
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
                if (movingInterval == 2)
                {
                    if (_DefaultMoveMentDirection)
                    {
                        posY++;
                    }
                    else
                    {
                        posY--;
                    }
                    sprite.posX = posX;
                    sprite.posY = posY;
                }
                else if (timeElapsed - timeElapsedSinceUpdateAnim > animInterval)
                {
                    timeElapsedSinceUpdateAnim = timeElapsed;
                    sprite.Update(gm);
                }
                if (timeElapsed - timeElapsedSinceUpdate > movingInterval)
                {
                    timeElapsedSinceUpdate = timeElapsed;
                    //Going Up and Down -> Active
                    if (movingInterval == 2)
                    {
                        movingInterval = 8;
                        //Send change direction to Sprite
                        sprite.ChangeDirection = true;
                    }
                    else
                    {
                        movingInterval = 2;
                        if (_DefaultMoveMentDirection)
                        {
                            _DefaultMoveMentDirection = false;
                        }
                        else
                        {
                            _DefaultMoveMentDirection = true;
                        }
                    }
                }
                //Active -> Going Up and Down
            }
        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            alive = false;
            sprite.Update(gm);
        }
    }
}
