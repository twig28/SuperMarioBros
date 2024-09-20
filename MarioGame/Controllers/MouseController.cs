using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class MouseController : IController
{

        private Game1 Game;

        public MouseController(Game1 gameName)
        {
            Game = gameName;
        }

        public void HandleInputs()
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Game.Exit();
            }
        }
}