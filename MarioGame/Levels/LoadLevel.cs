using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MarioGame.Blocks;

namespace MarioGame.Levels
{
    internal class LoadLevels
    {
        public static void LoadLevel(
            Game1 game,
            List<IBlock> blocks,
            List<IEnemy> enemies,
            ItemContainer items, int level)
        {
            // Load resources
            SpriteFont font = game.Content.Load<SpriteFont>("File");
            Texture2D enemyTextures = game.Content.Load<Texture2D>("smb_enemies_sheet");
            Texture2D itemTextures = game.Content.Load<Texture2D>("smb_items_sheet");
            Texture2D groundBlockTexture = game.Content.Load<Texture2D>("resizedGroundBlock");
            Texture2D blockTexture = game.Content.Load<Texture2D>("InitialBrickBlock");
            Texture2D multipleBlockTextures = game.Content.Load<Texture2D>("blocks");
            Texture2D sceneryTextures = game.Content.Load<Texture2D>("smb1_scenery_sprites");

            //Populate items (Items need to have a position in the constructor, they are currently hard coded)
            //items.AddItem(new Coin(itemTextures, new Vector2(500, 300)));
            //items.AddItem(new Mushroom(itemTextures, new Vector2(600, 300)));

            // Populate enemies
            enemies.Add(new Goomba(enemyTextures, game._spriteBatch, 500, 200));
            enemies.Add(new Koopa(enemyTextures, game._spriteBatch, 600, 500));
            enemies.Add(new Piranha(enemyTextures, game._spriteBatch, 1090, 538));

            // Populate blocks
            blocks.Add(new Block(new Vector2(500, 450), blockTexture));
            blocks.Add(new GroundBlock(new Vector2(900, game.GraphicsDevice.Viewport.Height - 120), groundBlockTexture));
            blocks.Add(new MysteryBlock(new Vector2(560, 200), multipleBlockTextures));
            blocks.Add(new MysteryBlock(new Vector2(560, 450), multipleBlockTextures));
            blocks.Add(new MediumPipe(new Vector2(1075, 500), sceneryTextures));

            // Create a row of blocks along the bottom of the screen
            for (int i = 0; i <= game.GraphicsDevice.Viewport.Width - 120; i += 60)
            {
                blocks.Add(new GroundBlock(new Vector2(i, game.GraphicsDevice.Viewport.Height - 60), groundBlockTexture));
            }
        }
    }
}
