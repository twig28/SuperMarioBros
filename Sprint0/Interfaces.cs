using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public interface IController
    {
        void HandleInputs();
    }

    class KeyboardController : IController
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

class TextSprite
{
    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        sb.Begin();
        sb.DrawString(font, "Nothing Here Yet, Time to Work on Sprint 2 :(", new Vector2(100, 360), Color.Black);
        sb.End();
    }
}

//TODO IPlayer, IEnemy, IObject, etc.
