
using System.Threading;
using System.Threading.Tasks;
using MarioGame.Items;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;
    private KeyboardState ks;
     private KeyboardState previousState;

    public KeyboardController(Game1 gameName)
    {
        Game = gameName;
    }
    public void HandleInputs()
    {
        Game.current = Game1.SpriteType.Static;
        //Do Stuff
        ks = Keyboard.GetState();
        if (ks.IsKeyDown(Keys.Escape))
        {
            Game.Exit();
        }
        if (ks.IsKeyDown(Keys.U))
        {
            Item.lastItem();
        }
        if (ks.IsKeyDown(Keys.I))
        { 
            Item.nextItem();
        }
        if (ks.IsKeyDown(Keys.O))
        {
            Game.changeEnemy(false);
        }
        if (ks.IsKeyDown(Keys.P))
        {
            Game.changeEnemy(true);
        }
        
            if (ks.IsKeyDown(Keys.Right))
            {
                Game.current = Game1.SpriteType.Motion;

            }

            if (ks.IsKeyDown(Keys.Left))
            {
                Game.current = Game1.SpriteType.MotionL;

            }
        if (ks.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
        }

           
        if (ks.IsKeyDown(Keys.Q) && previousState.IsKeyUp(Keys.Q)) 
        {
            Game.keyboardPermitQ = true;
            Game.qPressed = true;
        }

        
        if (ks.IsKeyUp(Keys.Q))
        {
            Game.qPressed = false;
        }

        
        if (ks.IsKeyDown(Keys.E) && previousState.IsKeyUp(Keys.E))  
        {
            Game.keyboardPermitE = true;
            Game.ePressed = true;
        }

        
        if (ks.IsKeyUp(Keys.E))
        {
            Game.ePressed = false;
        }

     
        previousState = ks;

    }
}