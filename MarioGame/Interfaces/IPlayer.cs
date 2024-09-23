using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame;
using Microsoft.Xna.Framework;

namespace MarioGame
{
    internal interface IPlayer
    {
        void Update(GameTime gm);
        void Draw(SpriteBatch spriteBatch);
    }
}
