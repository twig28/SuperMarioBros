using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public interface IController
    {
        void HandleInputs();
    }

    class KeyboardController : IController
    {
        public Game1 game;

        public KeyboardController(Game1 gameName)
        {
            game = gameName;
        }
        public void HandleInputs()
        {
            //Do Stuff
        }
    }
}

class MouseController : IController
{

    public Game1 game;

    public MouseController(Game1 gameName)
    {
        game = gameName;
    }

    public void HandleInputs()
    {
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {
            game.Exit();
        }
    }
}

public interface ISprite
    {
        void Draw(SpriteBatch sb, Texture2D texture, Boolean changeFrame);
    }

class StaticStationSprite : ISprite
{
    public void Draw(SpriteBatch sb, Texture2D texture, Boolean changeFrame)
    {
        sb.Begin();
        sb.Draw(texture, new Rectangle(100, 150, 150, 150), new Rectangle(200, 50, 30, 35), Color.White);
        sb.End();
    }
}

class StaticMovingSprite : ISprite
{
    int y = 200;
    Rectangle destinationRectangle;
    public void Draw(SpriteBatch sb, Texture2D texture, Boolean changeFrame)
    {
        destinationRectangle = new Rectangle(100, y, 150, 150);
        y--;
        if (y < 50) y = 200;
        sb.Begin();
        sb.Draw(texture, destinationRectangle, new Rectangle(200, 50, 30, 35), Color.White);
        sb.End();
    }
}

class AnimatedStationSprite : ISprite
{
    int currFrame = 1;
    Rectangle sourceRectangle;
    public void Draw(SpriteBatch sb, Texture2D texture, Boolean changeFrame)
    {
        if (changeFrame)
        {
            if (currFrame == 1) { sourceRectangle = new Rectangle(235, 50, 30, 35); }
            else if (currFrame == 2) { sourceRectangle = new Rectangle(262, 50, 30, 35); }
            else
            {
                sourceRectangle = new Rectangle(285, 50, 30, 35);
                currFrame = 0;
            }
            currFrame++;
        }
            sb.Begin();
            sb.Draw(texture, new Rectangle(100, 150, 150, 150), sourceRectangle, Color.White);
            sb.End();
        }
}

class AnimatedMovingSprite : ISprite
{
    int currFrame = 1;
    int x = 50;
    Rectangle sourceRectangle;
    Rectangle destinationRectangle;
    public void Draw(SpriteBatch sb, Texture2D texture, Boolean changeFrame)
    {
        if (changeFrame)
        {
            if (currFrame == 1) { sourceRectangle = new Rectangle(235, 50, 30, 35); }
            else if (currFrame == 2) { sourceRectangle = new Rectangle(262, 50, 30, 35); }
            else
            {
                sourceRectangle = new Rectangle(285, 50, 30, 35);
                currFrame = 0;
            }
            currFrame++;
        }
        destinationRectangle = new Rectangle(x, 150, 150, 150);
        x++;
        if (x > 400) x = 50;
        sb.Begin();
        sb.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        sb.End();
    }
}

class TextSprite
{
    public void Draw(SpriteBatch sb, SpriteFont font)
    {
        sb.Begin();
        sb.DrawString(font, "Nothing Here Yet, Time to Work on Sprint 2 :(", new Vector2(100, 360), Color.Black);
        sb.End();
    }
}
