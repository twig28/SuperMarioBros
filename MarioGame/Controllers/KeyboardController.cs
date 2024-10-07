
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
            Game.player_sprite.Staplayer.Big = true;
            Game.player_sprite.StaLplayer.Big = true;
            Game.player_sprite.MRplayer.Big = true;
            Game.player_sprite.MLplayer.Big = true;
            Game.player_sprite.JumpLplayer.Big = true;
            Game.player_sprite.Jumpplayer.Big = true;
        }

        if (currentKeyState.IsKeyDown(Keys.M))
        {
            Game.player_sprite.Staplayer.Fire = true;
            Game.player_sprite.StaLplayer.Fire = true;
            Game.player_sprite.MRplayer.Fire = true;
            Game.player_sprite.MLplayer.Fire = true;
            Game.player_sprite.Jumpplayer.Fire = true;
            Game.Fire = true;
        }

        if (currentKeyState.IsKeyDown(Keys.J))
        {
            Game.player_sprite.Staplayer.Fire = false;
            Game.player_sprite.StaLplayer.Fire = false;
            Game.player_sprite.MRplayer.Fire = false;
            Game.player_sprite.MLplayer.Fire = false;
            Game.player_sprite.Jumpplayer.Fire = false;
            Game.player_sprite.JumpLplayer.Fire = false;
            Game.player_sprite.Staplayer.Big = false;
            Game.player_sprite.StaLplayer.Big = false;
            Game.player_sprite.MRplayer.Big = false;
            Game.player_sprite.MLplayer.Big = false;
            Game.player_sprite.Jumpplayer.Big = false;
            Game.player_sprite.JumpLplayer.Big = false;
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
