using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //states include dead, shell, movingRight, movingLeft
    internal class Koopa : IEnemy
    {
        private int recStartPos = 0;
        private int animInterval;
        private SpriteBatch sb;
        private Texture2D texture;
        private ISprite sprite;
        //get and set
        private double posX;
        private double posY;
        public bool Alive { get; set; }
        public bool MovingRight { get; set; }

        public Koopa(Texture2D Texture, SpriteBatch SpriteBatch, double X, double Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            sprite = new KoopaSprite(texture, sb, posX, posY);
        }

        public void Draw()
        {
            sprite.Draw();
        }

        public void Update() 
        { 

        }
    }
}
