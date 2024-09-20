﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Interfaces
{
    public interface IItem
    {
        void Draw();
        void Update();
        void ChangeState();
    }
}
