
namespace MarioGame.Classes;

public class KeyboardController : IController
{
    public Game1 Game;

    public KeyboardController(Game1 gameName)
    {
        Game = gameName;
    }
    public void HandleInputs()
    {
        //Do Stuff
    }
}