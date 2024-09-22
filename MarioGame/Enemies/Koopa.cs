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
    //states include dead, shell, movingRight, movingLeft
    internal class Koopa : IEnemy
    {
        private double animInterval;
        private SpriteBatch sb;
        private Texture2D texture;
        private KoopaSprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;
        private bool changeSpriteDirection = false;
        private double timeElapsed = 0;
        private double timeElapsedNew = 0;

        public bool Alive { get; set; }

        private bool _movingRight = true;
        public bool MovingRight
        {
            get { return _movingRight;}
            set { _movingRight = value; changeSpriteDirection = true; }
        }

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            sprite = new KoopaSprite(Texture, sb, posX, posY);
        }

        public void Draw()
        {
            sprite.Draw();
        }

        public void Update(GameTime gm) 
        {
            timeElapsed = gm.TotalGameTime.TotalSeconds;
            Console.WriteLine(timeElapsed);
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
                sprite.ChangeDirection = true;
            }
            if (timeElapsed - timeElapsedNew > 0.2)
            {
                timeElapsedNew = timeElapsed;
                sprite.Update(gm);
            }
        }
    }
}
