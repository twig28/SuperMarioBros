using MarioGame;
using MarioGame;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame;

public class KeyboardController : IController
{
    public Game1 game;

    public KeyboardController(Game1 gameName)
    {
        game = gameName;
    }
    public void HandleInputs()
    {
        //Do Stuff
    }
}

class MouseController : IController
{

    public Game1 game;

    public MouseController(Game1 gameName)
    {
        game = gameName;
    }

    public void HandleInputs()
    {
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {
            game.Exit();
        }
    }
}


