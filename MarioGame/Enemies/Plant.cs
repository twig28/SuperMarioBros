﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class Plant : IEnemy
    {
        public bool Alive { get; set; }
        public bool MovingRight { get; set; }
        public void ChangeState()
        {

        }
        public void Draw()
        {

        }

        public void Update(GameTime gm)
        {

        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {

        }
    }
}
