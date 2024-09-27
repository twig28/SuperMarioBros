
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
        if (Game.current == Game1.SpriteType.MotionL)
        {
            Game.current = Game1.SpriteType.StaticL;

        }
        else if (Game.current == Game1.SpriteType.Motion)
        {
            Game.current = Game1.SpriteType.Static;

        }

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
        if (IsKeyHitted(Keys.I))
        {
            Item.nextItem();
        }
        if (IsKeyHitted(Keys.O))
        {
            Game.changeEnemy(false);
        }
        if (IsKeyHitted(Keys.P))
        {
            Game.changeEnemy(true);
        }

        if (currentKeyState.IsKeyDown(Keys.Right) || currentKeyState.IsKeyDown(Keys.D))
        {
            Game.current = Game1.SpriteType.Motion;

        }

        if (currentKeyState.IsKeyDown(Keys.Left) || currentKeyState.IsKeyDown(Keys.A))
        {
            Game.current = Game1.SpriteType.MotionL;

        }
        if (currentKeyState.IsKeyDown(Keys.W) || currentKeyState.IsKeyDown(Keys.Up))
        {
            if (Game.current == Game1.SpriteType.MotionL || Game.current == Game1.SpriteType.StaticL)
            {
                Game.current = Game1.SpriteType.JumpL;
            }
            else if(Game.current == Game1.SpriteType.Motion || Game.current == Game1.SpriteType.Static)
            {
                Game.current = Game1.SpriteType.Jump;
            }

        }

        if (currentKeyState.IsKeyDown(Keys.E) || currentKeyState.IsKeyDown(Keys.S) || currentKeyState.IsKeyDown(Keys.Down))
        {
            Game.current = Game1.SpriteType.Damaged;

        }
        if (currentKeyState.IsKeyDown(Keys.X))
        {
            Game.Staplayer.Big = true;
            Game.StaLplayer.Big = true;
            Game.MRplayer.Big = true;
            Game.MLplayer.Big = true;
        }
        if (currentKeyState.IsKeyDown(Keys.M))
        {
            Game.Staplayer.Big = false;
            Game.StaLplayer.Big = false;
            Game.MRplayer.Big = false;
            Game.MLplayer.Big = false;

        }

        if (currentKeyState.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
            Game.current = Game1.SpriteType.Static;
            Game.UPlayerPosition = Game.PlayerPosition;
        }

        //keyboard control for fireballs 
        if (IsKeyHitted(Keys.Z))//push to attack enemy in the left
        {
            Game.keyboardPermitZ = true;
            Game.zPressed = true;
        }


        if (IsKeyHitted(Keys.N))//push to attack enemy in the right
        {
            Game.keyboardPermitN = true;
            Game.nPressed = true;
        }
        previousKeyState = currentKeyState;
    }
}
