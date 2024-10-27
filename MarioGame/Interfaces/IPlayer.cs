﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame;
using Microsoft.Xna.Framework;

namespace MarioGame
{

    //would have texture, position, state, and more properties
    public interface IPlayer
    {
        void Update(GameTime gm);
        void Draw(SpriteBatch spriteBatch,int width, int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference,Color c);
    }
}

