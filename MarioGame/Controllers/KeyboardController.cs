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

    public void HandleInputs(PlayerSprite mario)
    {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        if (currentKeyState.IsKeyDown(Keys.Q))
        {
            Game.Exit();
        }

        if (mario.current != PlayerSprite.SpriteType.Damaged)
        {
            // Move this to mario class
            if (mario.current == PlayerSprite.SpriteType.MotionL)
            {
                mario.current = PlayerSprite.SpriteType.StaticL;
                mario.direction = true;
            }
            else if (mario.current == PlayerSprite.SpriteType.Motion)
            {
                mario.current = PlayerSprite.SpriteType.Static;
                mario.direction = false;
            }
            if (currentKeyState.IsKeyDown(Keys.Right) || currentKeyState.IsKeyDown(Keys.D))
            {

                if (mario.current == PlayerSprite.SpriteType.Jump || mario.current == PlayerSprite.SpriteType.JumpL || mario.current == PlayerSprite.SpriteType.Falling)
                {
                    //  if (Game.player_sprite.UPlayerPosition.X < 1280 - (14 * 3f / 2) )
                    //  {
                    mario.UPlayerPosition.X += 5f;
                    // }
                }
                else
                {
                    mario.current = PlayerSprite.SpriteType.Motion;
                }
                mario.direction = false;
            }

            if (currentKeyState.IsKeyDown(Keys.Left) || currentKeyState.IsKeyDown(Keys.A))
            {

                if (mario.current == PlayerSprite.SpriteType.Jump || mario.current == PlayerSprite.SpriteType.JumpL || mario.current == PlayerSprite.SpriteType.Falling)
                {
                    if (mario.UPlayerPosition.X > 18 * 3f / 2)
                    {

                        mario.UPlayerPosition.X -= 5f;
                    }
                }
                else
                {
                    mario.current = PlayerSprite.SpriteType.MotionL;
                }

                mario.direction = true;

            }
            if (currentKeyState.IsKeyDown(Keys.W) || currentKeyState.IsKeyDown(Keys.Up))
            {

                if (mario.current == PlayerSprite.SpriteType.MotionL || mario.current == PlayerSprite.SpriteType.StaticL)
                {
                    mario.current = PlayerSprite.SpriteType.JumpL;
                    Game1.Instance.GetSoundLib().PlaySound("jump");
                    mario.direction = true;
                }
                else if (mario.current == PlayerSprite.SpriteType.Motion || mario.current == PlayerSprite.SpriteType.Static)
                {
                    mario.current = PlayerSprite.SpriteType.Jump;
                    mario.direction = false;
                    Game1.Instance.GetSoundLib().PlaySound("jump");

                }
            }

            //    if (currentKeyState.IsKeyDown(Keys.E))
            //    {
            //        mario.current = PlayerSprite.SpriteType.Damaged;
            //    }
            //}
                if (currentKeyState.IsKeyDown(Keys.X))
                {
                mario.mode = PlayerSprite.Mode.Star;

                }
            if (currentKeyState.IsKeyDown(Keys.V))
            {
                if (mario.mode != PlayerSprite.Mode.Fire && mario.mode != PlayerSprite.Mode.Star)
                {
                    mario.mode = PlayerSprite.Mode.Big;
                }

            }

            if (currentKeyState.IsKeyDown(Keys.M))
                {
                if (mario.mode != PlayerSprite.Mode.Star)
                {
                    mario.mode = PlayerSprite.Mode.Fire;
                }
                }

                if (currentKeyState.IsKeyDown(Keys.J))
                {

                mario.mode = PlayerSprite.Mode.None;
               }

            if (currentKeyState.IsKeyDown(Keys.R))
            {
                Game.ResetGame();
                mario.Reset();
            }

            // Control the fireball
            // To the left
            if (IsKeyPressed(Keys.Z, currentKeyState) && mario.mode == PlayerSprite.Mode.Fire == true)
            {
                keyboardPermitZ = true;
            }

            // To the right
            if (IsKeyPressed(Keys.N, currentKeyState) && mario.mode == PlayerSprite.Mode.Fire == true)
            {
                keyboardPermitN = true;
            }

            if (IsKeyPressed(Keys.D3, currentKeyState))
            {
                Game.SetLevel(5);
            }

            if (IsKeyPressed(Keys.D2, currentKeyState))
            {
                Game.SetLevel(2);

            }

            if (IsKeyPressed(Keys.D1, currentKeyState))
            {
                Game.SetLevel(1);
            }

            if (currentKeyState.IsKeyDown(Keys.S) || currentKeyState.IsKeyDown(Keys.Down))
            {
                mario.current = PlayerSprite.SpriteType.Crounch;
            }
           

            if (IsKeyPressed(Keys.O, currentKeyState))
            {
                Game1.Instance.CustomColorLoad(1);
            }
            if (IsKeyPressed(Keys.P, currentKeyState))
            {
                Game1.Instance.CustomColorLoad(2);
            }
        }
    }
}
