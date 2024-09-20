using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //states include dead, movingLeft, movingRight
    internal class Goomba : IEnemy
    {
        private int recStartPos = 0;
        private int animInterval;
        private Texture2D texture;
        private SpriteBatch sb;
        //get and set
        private double posX;
        private double posY;


        //constructor with initial position and texture/spritebatch
        public Goomba(Texture2D Texture, SpriteBatch SpriteBatch, double X, double Y)
        {
            texture = Texture;
            sb = SpriteBatch;
            posX = X; posY = Y;
        }

        public void Draw()
        {

        }

        public void ChangeState()
        {

        }

        public void Death()
        {

        }

    }
}
