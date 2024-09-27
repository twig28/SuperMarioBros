
using System.Threading;
using System.Threading.Tasks;
using MarioGame.Items;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.Controllers;

public class KeyboardController : IController
{
    public Game1 Game;
    private KeyboardState ks;


    public KeyboardController(Game1 gameName)
    {
        Game = gameName;
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
        
        if (ks.IsKeyDown(Keys.D)|| ks.IsKeyDown(Keys.Right))
        {
                Game.current = Game1.SpriteType.Motion;

        }

        if (ks.IsKeyDown(Keys.A)|| ks.IsKeyDown(Keys.Left))
        {
            Game.current = Game1.SpriteType.MotionL;

        }

        if (ks.IsKeyDown(Keys.W)|| ks.IsKeyDown(Keys.Up))
        {
            Game.current = Game1.SpriteType.Jump;

        }

        if (ks.IsKeyDown(Keys.E) || ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
        {
            Game.current = Game1.SpriteType.Damaged;

        }
        if (ks.IsKeyDown(Keys.X))
        {
            Game.Staplayer.Big = true;
            Game.StaLplayer.Big = true;
            Game.MRplayer.Big = true;
            Game.MLplayer.Big = true;
        }
        if (ks.IsKeyDown(Keys.M))
        {
            Game.Staplayer.Big = false;
            Game.StaLplayer.Big = false;
            Game.MRplayer.Big = false;
            Game.MLplayer.Big = false;

        }



        if (ks.IsKeyDown(Keys.R))
        {
            Game.ResetGame();
            Game.current = Game1.SpriteType.Static;
            Game.UPlayerPosition = Game.PlayerPosition;
        }



    }
}