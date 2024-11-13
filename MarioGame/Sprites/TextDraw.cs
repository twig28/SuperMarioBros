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
            
            spriteBatch.DrawString(font, text, new Vector2(400, 300), Color.Black);
        }
        public static void DrawText(SpriteFont font, SpriteBatch spriteBatch, PlayerSprite mario, int level)
        {

            spriteBatch.DrawString(font, "Coins: \n" 
                                 + "    " + mario.coin, new Vector2(40, 0), Color.White);
            spriteBatch.DrawString(font, "Score:\n " + "    " + mario.score, new Vector2(240, 0), Color.White);
            spriteBatch.DrawString(font, "Lives:\n " + "    "+ mario.lives, new Vector2(440, 0), Color.White);
            spriteBatch.DrawString(font, "World:\n " + "    "+ level, new Vector2(640, 0), Color.White);




        }


    }
}
