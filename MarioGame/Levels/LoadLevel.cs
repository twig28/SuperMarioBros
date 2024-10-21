using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MarioGame.Blocks;
using System.IO;
using System;

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
            List<(string ObjectType, int X, int Y)> entities;


            if (level == 1)
            {
                // Load CSV Data
                string filePath = Path.Combine("..", "..", "..", "Levels", "Level1.csv");
                entities = LoadEntitiesFromCSV(filePath);


            }
            else
            {
                string filePath = Path.Combine("..", "..", "..", "Levels", "Level2.csv");
                entities = LoadEntitiesFromCSV(filePath);
            }

                foreach (var entity in entities)
                {
                    Vector2 position = new Vector2(entity.X, entity.Y);

                    switch (entity.ObjectType)
                    {
                        case "BaseBlock":
                            blocks.Add(new GroundBlock(position, groundBlockTexture));
                            break;
                        case "MysteryBlock":
                            blocks.Add(new MysteryBlock(position, multipleBlockTextures));
                            break;
                        case "BrickBlock":
                            blocks.Add(new Block(position, blockTexture));
                            break;
                        case "Pipe":
                            blocks.Add(new MediumPipe(position, sceneryTextures));
                            break;
                        case "Goomba":
                            enemies.Add(new Goomba(enemyTextures, game._spriteBatch, entity.X, entity.Y));
                            break;
                        case "Koopa":
                            enemies.Add(new Koopa(enemyTextures, game._spriteBatch, entity.X, entity.Y));
                            break;
                        case "Piranha":
                            enemies.Add(new Piranha(enemyTextures, game._spriteBatch, entity.X, entity.Y));
                            break;
                        case "Coin":
                            //items.AddItem(new Coin(itemTextures, position));
                            break;
                    }
                }

                for (int i = 0; i <= game.GraphicsDevice.Viewport.Width - 120; i += 60)
                {
                    blocks.Add(new GroundBlock(new Vector2(i, game.GraphicsDevice.Viewport.Height - 60), groundBlockTexture));
                }

            }

        private static List<(string ObjectType, int X, int Y)> LoadEntitiesFromCSV(string filePath)
        {
            var entities = new List<(string, int, int)>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    string objectType = values[0];
                    int xPosition = int.Parse(values[1]);
                    int yPosition = int.Parse(values[2]);

                    entities.Add((objectType, xPosition, yPosition));
                }
            }

            return entities;
        }
    }
}
