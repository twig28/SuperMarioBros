
namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;

    public KeyboardController(Game1 gameName)
    {
        Game = gameName;
    }
    public void HandleInputs()
    {
        //Add Logic For Spring 2 using Commands
    }
}