using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Score
{
    internal class Score
    {
        private static SpriteFont font;
        private Score()
        {
        }
        public static void Draw(Game1 game, SpriteBatch spriteBatch, int score)
        {
            Score.font = game.Content.Load<SpriteFont>("File");
            string text = "Score: " +  score;
            Vector2 textSize = font.MeasureString(text);
            int x = 0;
                //game.player_sprite.GetDestinationRectangle().X - (int)textSize.X / 2;
            const int y = 0;
            spriteBatch.DrawString(font, text, new(x, y), Color.Black);
        }
    }
}

