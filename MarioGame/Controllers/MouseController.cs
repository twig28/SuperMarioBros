using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class MouseController : IController
{

        private Game1 Game;

        public MouseController(Game1 gameName)
        {
            Game = gameName;
        }

        public void HandleInputs(PlayerSprite mario)
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Game.Exit();
            }
            if(Mouse.GetState().LeftButton == ButtonState.Pressed) {

            }
        }

    public bool IsKeyPressed(Keys key, KeyboardState currentKeyboardState)
    {
        return false;
    }
}