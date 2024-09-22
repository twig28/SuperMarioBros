using MarioGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    //states include dead, movingLeft, movingRight
    internal class Goomba : IEnemy
    {
        private double animInterval;
        private SpriteBatch sb;
        private Texture2D texture;
        private ISprite sprite;
        private int posX;
        private int posY;
        private int width;
        private int height;

        public bool Alive { get; set; }
        public bool MovingRight { get; set; }

        public Goomba(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X; posY = Y;
            sprite = new GoombaSprite(texture, sb, posX, posY);
        }

        public void Draw()
        {
            sprite.Draw();
        }

        public void Update(GameTime gm)
        {

        }

        public void TriggerDeath(GameTime gm, bool stomped)
        {
            
        }

    }
}
