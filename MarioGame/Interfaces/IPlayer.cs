using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public interface IPlayer
    {
        void ChangeState();
        void Update();
        void Draw();
    }
}
