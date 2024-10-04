﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //would have texture and position properties
    public interface IEnemy
    {    
        bool Alive { get; set; }
        bool MovingRight { get; set; }
        int getPosX { get; set; }
        int getPosY { get; set; }
        void Draw();
        void Update(GameTime gm);
        void TriggerDeath(GameTime gm, bool stomped);
    }
}
