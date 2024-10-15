using Microsoft.Xna.Framework;
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
        bool DefaultMoveMentDirection { get; set; }
        Rectangle GetDestinationRectangle();
        int setPosX { set; }
        int setPosY { set; }
        void Draw();
        void Update(GameTime gm);
        void TriggerDeath(GameTime gm, bool stomped);
    }
}
