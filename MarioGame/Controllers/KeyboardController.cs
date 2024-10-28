using System.Threading;
using System.Threading.Tasks;
using MarioGame.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;
    private KeyboardState previousKeyState;
    private KeyboardState currentKeyState;

    // Add control variables for firing fireballs
    public bool keyboardPermitZ = false;
    public bool keyboardPermitN = false;

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

       

        if (currentKeyState.IsKeyDown(Keys.Q))
        {
            Game.Exit();
        }

        if (Game.player_sprite.current != PlayerSprite.SpriteType.Damaged)
        {
            // Move this to mario class
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
            if (currentKeyState.IsKeyDown(Keys.Right) || currentKeyState.IsKeyDown(Keys.D))
            {
                if (Game.player_sprite.current != PlayerSprite.SpriteType.Falling)
                {
                    if (Game.player_sprite.current == PlayerSprite.SpriteType.Jump || Game.player_sprite.current == PlayerSprite.SpriteType.JumpL)
                    {
                      //  if (Game.player_sprite.UPlayerPosition.X < 1280 - (14 * 3f / 2) )
                      //  {
                            Game.player_sprite.UPlayerPosition.X += 5f;
                       // }

                    }
                    else
                    {
                        Game.player_sprite.current = PlayerSprite.SpriteType.Motion;
                    }
                }
                Game.player_sprite.left = false;


            }

            if (currentKeyState.IsKeyDown(Keys.Left) || currentKeyState.IsKeyDown(Keys.A))
            {
                if (Game.player_sprite.current != PlayerSprite.SpriteType.Falling)
                {
                    if (Game.player_sprite.current == PlayerSprite.SpriteType.Jump || Game.player_sprite.current == PlayerSprite.SpriteType.JumpL)
                    {
                        if (Game.player_sprite.UPlayerPosition.X > 18 * 3f / 2)
                        {

                            Game.player_sprite.UPlayerPosition.X -= 5f;
                        }
                    }
                    else
                    {
                        Game.player_sprite.current = PlayerSprite.SpriteType.MotionL;
                    }
                }
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

            if (currentKeyState.IsKeyDown(Keys.E))
            {
                Game.player_sprite.current = PlayerSprite.SpriteType.Damaged;
            }
        }
            if (currentKeyState.IsKeyDown(Keys.X))
            {
               // Game.player_sprite.Big = true;
                Game.player_sprite.Star = true;
            }

            if (currentKeyState.IsKeyDown(Keys.M))
            {
                Game.player_sprite.Fire = true;
                Game.Fire = true;
            }

            if (currentKeyState.IsKeyDown(Keys.J))
            {
                Game.player_sprite.Fire = false;
                Game.player_sprite.Big = false;
                Game.player_sprite.Star = false;
                Game.Fire = false;
            }

            if (currentKeyState.IsKeyDown(Keys.R))
            {
                Game.ResetGame();
            Game.player_sprite.Reset();
            
            }

            // Control the fireball
            // To the left
            if (IsKeyPressed(Keys.Z, currentKeyState) && Game.Fire == true)
            {
                keyboardPermitZ = true;
            }

            // To the right
            if (IsKeyPressed(Keys.N, currentKeyState) && Game.Fire == true)
            {
                keyboardPermitN = true;
            }

            if (IsKeyPressed(Keys.D2, currentKeyState))
            {
                Game.ChangeCurrLevel(2);
            Game.Fire = false;
            }

            if (IsKeyPressed(Keys.D1, currentKeyState))
            {
                Game.ChangeCurrLevel(1);
            Game.Fire = false; 
            }

    }
}
