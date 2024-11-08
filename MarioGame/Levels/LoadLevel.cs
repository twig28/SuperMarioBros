using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MarioGame.Blocks;
using System.IO;
using System;
using MarioGame.Scenery;
using System.Reflection.PortableExecutable;

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
            string filePath = "";

            filePath = Path.Combine("..", "..", "..", "Levels", $"Level{level}.csv");

            var (colorPalette, marioPosition, entities, pipeDestinations) = LoadEntitiesFromCSV(filePath);

            // Set Mario's starting position
            //game.player_sprite.SetPosition(marioPosition);

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
                    case "PipeDestination":
                        var pipe = new Pipe(position, sceneryTextures);
                        if (pipeDestinations.Count > 0)
                        {
                            var destination = pipeDestinations[0];
                            pipe.setIsEntrance(destination.LevelDest, destination.X, destination.Y);
                            pipeDestinations.RemoveAt(0); 
                        }
                        blocks.Add(pipe);
                        break;
                    case "Pipe":
                        blocks.Add(new Pipe(position, sceneryTextures));
                        break;
                    case "LongPipeDestination":
                        var longPipe = new Pipe(position, sceneryTextures);
                        longPipe.makePipeLong();
                        if (pipeDestinations.Count > 0)
                        {
                            var destination = pipeDestinations[0];
                            longPipe.setIsEntrance(destination.LevelDest, destination.X, destination.Y);
                            pipeDestinations.RemoveAt(0); 
                        }
                        blocks.Add(longPipe);
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
                    case "LargeHill":
                        scenery.Add(new LargeHill(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "SmallHill":
                        scenery.Add(new SmallHill(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "SmallBush":
                        scenery.Add(new SmallBush(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "LargeBush":
                        scenery.Add(new LargeBush(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "SmallCloud":
                        scenery.Add(new SmallCloud(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "LargeCloud":
                        scenery.Add(new LargeCloud(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "Castle":
                        //scenery.Add(new Castle(signTexture, entity.X, entity.Y));
                        break;
                    case "Flag":
                        blocks.Add(new Flagpole(position, sceneryTextures));
                        break;
                    case "LPipe":
                        blocks.Add(new LPipe(position, sceneryTextures));
                        break;
                }

            }
        }

        private static (string colorPalette, Vector2 marioPosition, List<(string ObjectType, int X, int Y)> entities, List<(int LevelDest, int X, int Y)> pipeDestinations) LoadEntitiesFromCSV(string filePath)
        {
            string colorPalette = "";
            Vector2 marioPosition = Vector2.Zero;
            var entities = new List<(string, int, int)>();
            var pipeDestinations = new List<(int LevelDest, int X, int Y)>();

            using (var reader = new StreamReader(filePath))
            {
                // Read first two lines for ColorPalette and MarioPosition
                var colorPaletteLine = reader.ReadLine();
                if (colorPaletteLine != null)
                {
                    var colorValues = colorPaletteLine.Split(',');
                    if (colorValues[0] == "ColorPalette")
                    {
                        colorPalette = colorValues[1];
                    }
                }

                var marioPositionLine = reader.ReadLine();
                if (marioPositionLine != null)
                {
                    var marioValues = marioPositionLine.Split(',');
                    if (marioValues[0] == "MarioPosition")
                    {
                        int marioX = int.Parse(marioValues[1]);
                        int marioY = int.Parse(marioValues[2]);
                        marioPosition = new Vector2(marioX, marioY);
                    }
                }
                reader.ReadLine();

                // Read remaining lines for entities
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values[0] == "PipeDestination")
                    {
                        // Parse and add the PipeDestination to the list
                        string objectType = values[0];
                        int xPosition = int.Parse(values[1]);
                        int yPosition = int.Parse(values[2]);

                        entities.Add((objectType, xPosition, yPosition));
                        int levelDest = int.Parse(values[3]);
                        int xPos = int.Parse(values[4]);
                        int yPos = int.Parse(values[5]);
                        pipeDestinations.Add((levelDest, xPos, yPos));
                    }
                    else
                    {
                        // Handle other entities
                        string objectType = values[0];
                        int xPosition = int.Parse(values[1]);
                        int yPosition = int.Parse(values[2]);

                        entities.Add((objectType, xPosition, yPosition));
                    }
                }
            }

            return (colorPalette, marioPosition, entities, pipeDestinations);

        }
    }
}
