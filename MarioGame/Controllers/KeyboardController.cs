
using System.Threading;
using System.Threading.Tasks;
using MarioGame.Items;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;
    private KeyboardState ks;
    private KeyboardState previousKeyState;
    private KeyboardState currentKeyState;
    public KeyboardState GetState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }


    public KeyboardController(Game1 gameName)
    {
        Game = gameName;
    }

    public bool IsKeyHitted(Keys key)
    {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
    public void HandleInputs()
    {
        Game.current = Game1.SpriteType.Static;
        //Do Stuff
        GetState();
        if (currentKeyState.IsKeyDown(Keys.Escape))
        {
            Game.Exit();
        }
        if (IsKeyHitted(Keys.U))
        {
            Item.lastItem();
        }
        if (currentKeyState.IsKeyDown(Keys.I))
        { 
            Item.nextItem();
        }
        if (currentKeyState.IsKeyDown(Keys.O))
        {
            Game.changeEnemy(false);
        }
        if (currentKeyState.IsKeyDown(Keys.P))
        {
            Game.changeEnemy(true);
        }
        
            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                Game.current = Game1.SpriteType.Motion;

            }

            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                Game.current = Game1.SpriteType.MotionL;

            }
        if (currentKeyState.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
        }



    }
}