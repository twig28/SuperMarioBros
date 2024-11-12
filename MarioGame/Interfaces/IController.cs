using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //(most likely) need to use ICommand Structure
    public interface IController
    {
        void HandleInputs(PlayerSprite mario);
        bool IsKeyPressed(Keys key, KeyboardState currentKeyboardState);
    }
}
