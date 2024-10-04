
using System.Threading;
using System.Threading.Tasks;
using MarioGame.Items;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;
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

    public bool IsKeyPressed(Keys key, KeyboardState currentKeyboardState)
    {
        return currentKeyboardState.IsKeyDown(key) && previousKeyState.IsKeyUp(key);
    }

    public void HandleInputs()
    {
        previousKeyState = currentKeyState;

        currentKeyState = Keyboard.GetState();

        if (Game.current == Game1.SpriteType.MotionL)
        {
            Game.current = Game1.SpriteType.StaticL;
        }
        else if (Game.current == Game1.SpriteType.Motion)
        {
            Game.current = Game1.SpriteType.Static;
        }

        if (currentKeyState.IsKeyDown(Keys.Escape))
        {
            Game.Exit();
        }
        if (currentKeyState.IsKeyDown(Keys.Q))
        {
            Game.Exit();
        }
        if (IsKeyPressed(Keys.U, currentKeyState))
        {
            Item.lastItem();
        }
        if (IsKeyPressed(Keys.I, currentKeyState))
        {
            Item.nextItem();
        }
        if (IsKeyPressed(Keys.O, currentKeyState))
        {
            Game.changeEnemy(false);
        }
        if (IsKeyPressed(Keys.P, currentKeyState))
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
            else if (Game.current == Game1.SpriteType.Motion || Game.current == Game1.SpriteType.Static)
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
            Game.JumpLplayer.Big = true;
            Game.Jumpplayer.Big = true;
        }

        if (currentKeyState.IsKeyDown(Keys.M))
        {
            Game.Staplayer.Fire = true;
            Game.StaLplayer.Fire = true;
            Game.MRplayer.Fire = true;
            Game.MLplayer.Fire = true;
            Game.Jumpplayer.Fire = true;
            Game.JumpLplayer.Fire = true;
            Game.Fire = true;
        }

        if (currentKeyState.IsKeyDown(Keys.J))
        {
            Game.Staplayer.Fire = false;
            Game.StaLplayer.Fire = false;
            Game.MRplayer.Fire = false;
            Game.MLplayer.Fire = false;
            Game.Jumpplayer.Fire = false;
            Game.JumpLplayer.Fire = false;
            Game.Staplayer.Big = false;
            Game.StaLplayer.Big = false;
            Game.MRplayer.Big = false;
            Game.MLplayer.Big = false;
            Game.Jumpplayer.Big = false;
            Game.JumpLplayer.Big = false;
            Game.Fire = false;
        }

        if (currentKeyState.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
            Game.current = Game1.SpriteType.Static;
            Game.UPlayerPosition = Game.PlayerPosition;
        }

        //keyboard control for fireballs 
        if (IsKeyPressed(Keys.Z, currentKeyState) && Game.Fire == true) // push to attack enemy on the left
        {
            Game.keyboardPermitZ = true;
            Game.zPressed = true;
        }

        if (IsKeyPressed(Keys.N, currentKeyState) && Game.Fire == true) // push to attack enemy on the right
        {
            Game.keyboardPermitN = true;
            Game.nPressed = true;
        }
    }

}
