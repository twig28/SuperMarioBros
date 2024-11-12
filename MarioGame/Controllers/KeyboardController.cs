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

        if (mario.current!= PlayerSprite.SpriteType.Damaged)
        {
            // Move this to mario class
            if (mario.current == PlayerSprite.SpriteType.MotionL)
            {
                mario.current = PlayerSprite.SpriteType.StaticL;
                mario.left = true;
            }
            else if (mario.current == PlayerSprite.SpriteType.Motion)
            {
                mario.current = PlayerSprite.SpriteType.Static;
                mario.left = false;
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

                        mario.left = false;


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

                mario.left = true;

            }
            if (currentKeyState.IsKeyDown(Keys.W) || currentKeyState.IsKeyDown(Keys.Up))
            {
                 
                if (mario.current == PlayerSprite.SpriteType.MotionL || mario.current == PlayerSprite.SpriteType.StaticL)
                {
                    mario.current = PlayerSprite.SpriteType.JumpL;
                    Game1.Instance.GetSoundLib().PlaySound("fireball");
                    mario.left = true;
                }
                else if (mario.current == PlayerSprite.SpriteType.Motion || mario.current == PlayerSprite.SpriteType.Static)
                {
                    mario.current = PlayerSprite.SpriteType.Jump;
                    mario.left = false;
                    Game1.Instance.GetSoundLib().PlaySound("fireball");

                }
            }

            if (currentKeyState.IsKeyDown(Keys.E))
            {
                mario.current = PlayerSprite.SpriteType.Damaged;
            }
        }
            if (currentKeyState.IsKeyDown(Keys.X))
            {
            mario.Big = false;
            mario.Fire = false;
            mario.Star = true;
                
            }
        if (currentKeyState.IsKeyDown(Keys.V))
        {
            mario.Big = true;

        }

        if (currentKeyState.IsKeyDown(Keys.M))
            {
            mario.Big = false;
            mario.Fire = true;
                Game.Fire = true;
            }

            if (currentKeyState.IsKeyDown(Keys.J))
            {
            mario.Fire = false;
            mario.Big = false;
            mario.Star = false;
                Game.Fire = false;
            }

            if (currentKeyState.IsKeyDown(Keys.R))
            {
                Game.ResetGame();
            mario.Reset();
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
                Game.SetLevel(2);
            Game.Fire = false;
            }

            if (IsKeyPressed(Keys.D1, currentKeyState))
            {
                Game.SetLevel(1);
            Game.Fire = false; 
            }

            if (currentKeyState.IsKeyDown(Keys.S) || currentKeyState.IsKeyDown(Keys.Down))
        {
            mario.crouched = true;
        }
        else
        {
            mario.crouched = false;
        }

    }
}
