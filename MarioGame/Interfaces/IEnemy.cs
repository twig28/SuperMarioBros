﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //would have texture and position properties
    public interface IEnemy
    {
        void Draw();
        void ChangeState();
    }
}
