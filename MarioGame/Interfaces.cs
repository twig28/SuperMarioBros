using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //(most likely) need to use ICommand Structure
    public interface IController
    {
        void HandleInputs();
    }

    //would have texture, position, state, and more properties
    public interface IPlayer
    {
        void ChangeState();
        void Update();
        void Draw();
    }

    //would have texture and position properties
    public interface IBlock
    {
        void Draw();
        void ChangeState();
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
}

//TODO IPlayer, IEnemy, IObject, etc.
