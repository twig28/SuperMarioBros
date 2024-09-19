using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    public interface IItem
    {
        void Draw();
        void ChangeState();
    }
}
