using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;

namespace MarioGame
{
    public class Crouch : IPlayer
    {
        private Texture2D Texture { get; set; }
        public Vector2 Position;
        private Game1 Game;
        public Crouch(Texture2D texture, Vector2 position, Game1 game)
        {
            Texture = texture;
            Position = position;
            Game = game;

        }
        public void Draw(SpriteBatch spriteBatch, int width, int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference, Color c)
        {

            Position.Y += pos_difference;

            spriteBatch.Draw(Texture, Position, sourceRectangle[0], c, 0f, new Vector2(width / 2, height / 2), Scale, SpriteEffects.None, 0f);


        }

        public void Update(GameTime gameTime, PlayerSprite mario)
        {
            //crounch don't need to update status
        }
    }
}

