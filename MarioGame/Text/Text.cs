/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class  Text
    {
        public Text()
        {
            

        }



        public void DrawText(SpriteFont font,SpriteBatch spriteBatch, PlayerSprite mario)
        {
           
            spriteBatch.DrawString(font, "Coins: " + mario.getCoinScoreLives()[0], new Vector2(40, 0), Color.White);
            spriteBatch.DrawString(font, "Score: " + mario.getCoinScoreLives()[1],new Vector2(400, 0), Color.White);
            spriteBatch.DrawString(font, "Lives: " + mario.getCoinScoreLives()[2], new Vector2(800, 0), Color.White);
            


        }
        public void DrawGameOver(SpriteFont font, SpriteBatch spriteBatch, PlayerSprite mario)
        {

            spriteBatch.DrawString(font,"Game Over\n" +
                "Press R to retry", new Vector2(400, 300), Color.Black);
            



        }

    }
}
*/