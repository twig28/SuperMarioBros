using MarioGame;
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
        
        //constructor with initial position and texture/spritebatch

        //get and set
        private double posX;
        private double posY;

        //public Goomba()
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
