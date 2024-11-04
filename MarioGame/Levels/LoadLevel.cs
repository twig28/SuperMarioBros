using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MarioGame.Blocks;
using System.IO;
using System;
using MarioGame.Scenery;

namespace MarioGame.Levels
{
    internal class LoadLevels
    {
        public static void LoadLevel(
            Game1 game,
            List<IBlock> blocks,
            List<IEnemy> enemies,
            List<IItem> items,
            List<IScenery> scenery,
            int level)
        {
            // Load resources
            SpriteFont font = game.Content.Load<SpriteFont>("File");
            Texture2D enemyTextures = game.Content.Load<Texture2D>("smb_enemies_sheet");
            Texture2D itemTextures = game.Content.Load<Texture2D>("smb_items_sheet");
            Texture2D groundBlockTexture = game.Content.Load<Texture2D>("resizedGroundBlock");
            Texture2D blockTexture = game.Content.Load<Texture2D>("InitialBrickBlock");
            Texture2D multipleBlockTextures = game.Content.Load<Texture2D>("blocks");
            Texture2D sceneryTextures = game.Content.Load<Texture2D>("smb1_scenery_sprites");
            Texture2D signTexture = game.Content.Load<Texture2D>("Super_Mario_Bros._NES_Logo");
            Texture2D stairBlockTexture = game.Content.Load<Texture2D>("Hard_Block_SMB");
            List <(string ObjectType, int X, int Y)> entities;


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
                    case "StairBlock":
                        blocks.Add(new StairBlock(position, stairBlockTexture));
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
                        items.Add(new Coin(itemTextures, position));
                        break;
                    case "FireFlower":
                        items.Add(new FireFlower(itemTextures, position));
                        break;
                    case "Mushroom":
                        items.Add(new Mushroom(itemTextures, position));
                        break;
                    case "Star":
                        items.Add(new Star(itemTextures, position));
                        break;
                    case "Sign":
                        scenery.Add(new SuperMarioSign(signTexture, entity.X, entity.Y));
                        break;
                }
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
