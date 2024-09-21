using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame;
using System.Drawing;

namespace MarioGame
{
    internal interface ISprite
    {
        Texture2D Texture { get; set; }
        Rectangle DestinationRectangle { get; set;}
        void Update();
        void Draw();
    }
}
