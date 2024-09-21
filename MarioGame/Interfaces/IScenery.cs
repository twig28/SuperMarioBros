using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public interface IScenery
    {
        void Draw();
    }
}

//TODO IPlayer, IEnemy, IObject, etc.
