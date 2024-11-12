using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    internal class TextDraw
    {
        private static SpriteFont font;
        private  TextDraw()
        {
        }
        public static void Draw(SpriteFont font, SpriteBatch spriteBatch, PlayerSprite mario)
        {
            string text = "Game Over\n" +
                          "Press R to retry";
            Vector2 textSize = font.MeasureString(text);
            float x = mario.UPlayerPosition.X;
            const int y = 100;
            spriteBatch.DrawString(font, text, new(x,y), Color.Black);
        }
        public static void DrawText(SpriteFont font, SpriteBatch spriteBatch, PlayerSprite mario)
        {

            spriteBatch.DrawString(font, "Coins: " + mario.coin, new Vector2(40, 0), Color.White);
            spriteBatch.DrawString(font, "Score: " + mario.score, new Vector2(400, 0), Color.White);
            spriteBatch.DrawString(font, "Lives: " + mario.lives, new Vector2(800, 0), Color.White);



        }


    }
}
