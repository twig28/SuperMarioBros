
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

        if (Game.player_sprite.current == PlayerSprite.SpriteType.MotionL)
        {
            Game.player_sprite.current = PlayerSprite.SpriteType.StaticL;
            Game.player_sprite.left = true;
        }
        else if (Game.player_sprite.current == PlayerSprite.SpriteType.Motion)
        {
            Game.player_sprite.current = PlayerSprite.SpriteType.Static;
            Game.player_sprite.left = false;
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

        if (currentKeyState.IsKeyDown(Keys.Right) || currentKeyState.IsKeyDown(Keys.D))
        {
            Game.player_sprite.current = PlayerSprite.SpriteType.Motion;
            Game.player_sprite.left = false ;
        }

        if (currentKeyState.IsKeyDown(Keys.Left) || currentKeyState.IsKeyDown(Keys.A))
        {
            Game.player_sprite.current = PlayerSprite.SpriteType.MotionL;
            Game.player_sprite.left = true;
        }
        if (currentKeyState.IsKeyDown(Keys.W) || currentKeyState.IsKeyDown(Keys.Up))
        {
            if (Game.player_sprite.current == PlayerSprite.SpriteType.MotionL || Game.player_sprite.current == PlayerSprite.SpriteType.StaticL)
            {
                Game.player_sprite.current = PlayerSprite.SpriteType.JumpL;
                Game.player_sprite.left = true;
            }
            else if (Game.player_sprite.current == PlayerSprite.SpriteType.Motion || Game.player_sprite.current == PlayerSprite.SpriteType.Static)
            {
                Game.player_sprite.current = PlayerSprite.SpriteType.Jump;
                Game.player_sprite.left = false;
            }
        }

        if (currentKeyState.IsKeyDown(Keys.E) || currentKeyState.IsKeyDown(Keys.S) || currentKeyState.IsKeyDown(Keys.Down))
        {
            Game.player_sprite.current = PlayerSprite.SpriteType.Damaged;
        }
        if (currentKeyState.IsKeyDown(Keys.X))
        {
           
            Game.player_sprite.Big = true;
        }

        if (currentKeyState.IsKeyDown(Keys.M))
        {
            
            Game.player_sprite.Fire = true;
            Game.Fire = true;
        }

        if (currentKeyState.IsKeyDown(Keys.J))
        {
           
            Game.player_sprite.Fire=false;
            Game.player_sprite.Big = false;
            Game.Fire = false;
        }

        if (currentKeyState.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
            Game.player_sprite.current = PlayerSprite.SpriteType.Static;
            Game.player_sprite.UPlayerPosition = Game.player_sprite.PlayerPosition;
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
