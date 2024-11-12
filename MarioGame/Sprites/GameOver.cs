using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    internal class GameOver
    {
        private static SpriteFont font;
        private  GameOver()
        {
        }
        public static void Draw(Game1 game, SpriteBatch spriteBatch, PlayerSprite mario)
        {
            GameOver.font = game.Content.Load<SpriteFont>("text");
            string text = "Game Over\n" +
                          "Press R to retry";
            Vector2 textSize = font.MeasureString(text);
            int x = mario.GetDestinationRectangle().X - (int)textSize.X/2;
            const int y = 100;
            spriteBatch.DrawString(font, text, new(x,y), Color.Black);
        }
    }
}
