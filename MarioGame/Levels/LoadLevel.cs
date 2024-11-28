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
using System.Diagnostics;

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
    PlayerSprite mario,
    int level, SpriteBatch _spriteBatch, int customColor)
        {
            SpriteFont font = game.Content.Load<SpriteFont>("File");
            Texture2D enemyTextures = game.Content.Load<Texture2D>("smb_enemies_sheet");
            Texture2D itemTextures = game.Content.Load<Texture2D>("smb_items_sheet");
            Texture2D groundBlockTexture = game.Content.Load<Texture2D>("resizedGroundBlock");
            Texture2D blockTexture = game.Content.Load<Texture2D>("InitialBrickBlock");
            Texture2D multipleBlockTextures = game.Content.Load<Texture2D>("blocks");
            Texture2D sceneryTextures = game.Content.Load<Texture2D>("smb1_scenery_sprites");
            Texture2D signTexture = game.Content.Load<Texture2D>("Super_Mario_Bros._NES_Logo");
            Texture2D stairBlockTexture = game.Content.Load<Texture2D>("Hard_Block_SMB");

            //In this game Level 0-1 corresponds to 1-1 cells and Level2-4 corresponds to 1-2 cells, Level5 is custom final level
            string filePath = Path.Combine("..", "..", "..", "Levels", $"Level{level}.csv");

            var (colorPalette, marioPosition, world, entities, pipeDestinations) = LoadEntitiesFromCSV(filePath);

            Game1.Instance.CurrWorld = world;

            int color = int.Parse(colorPalette);
            if(customColor > -1)
            {
                color = customColor;
            }
            Game1.Instance.SetBackgroundColor(color);

            mario.setPosition((int)(marioPosition.X), (int)(marioPosition.Y));

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
                    case "LongPipe":
                        var longPipe = new Pipe(position, sceneryTextures);
                        longPipe.makePipeLong();
                        blocks.Add(longPipe);
                        break;
                    case "LongPipeDestination":
                        var longPipeD = new Pipe(position, sceneryTextures);
                        if (pipeDestinations.Count > 0)
                        {
                            var destination = pipeDestinations[0];
                            longPipeD.setIsEntrance(destination.LevelDest, destination.X, destination.Y);
                            pipeDestinations.RemoveAt(0);
                        }
                        longPipeD.makePipeLong();
                        blocks.Add(longPipeD);
                        break;
                    case "Goomba":
                        enemies.Add(new Goomba(enemyTextures, _spriteBatch, entity.X, entity.Y, color));
                        break;
                    case "Koopa":
                        enemies.Add(new Koopa(enemyTextures, _spriteBatch, entity.X, entity.Y, color));
                        break;
                    case "Piranha":
                        enemies.Add(new Piranha(enemyTextures, _spriteBatch, entity.X, entity.Y));
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
                        scenery.Add(new Castle(sceneryTextures, entity.X, entity.Y));
                        break;
                    case "Flag":
                        blocks.Add(new Flagpole(position, sceneryTextures));
                        break;
                    case "LPipe":
                        blocks.Add(new LPipe(position, sceneryTextures));
                        break;
                    case "UpPlatform":
                        blocks.Add(new Platform(position, itemTextures));
                        break;
                    case "DownPlatform":
                        var p = new Platform(position, itemTextures);
                        blocks.Add(p);
                        p.ReverseDirection();
                        break;
                }
            }
        }

        private static (string colorPalette, Vector2 marioPosition, int world, List<(string ObjectType, int X, int Y)> entities, List<(int LevelDest, int X, int Y)> pipeDestinations) LoadEntitiesFromCSV(string filePath)
        {
            string colorPalette = "";
            Vector2 marioPosition = Vector2.Zero;
            int world = 0;
            var entities = new List<(string, int, int)>();
            var pipeDestinations = new List<(int LevelDest, int X, int Y)>();

            using (var reader = new StreamReader(filePath))
            {
                // Read first line for ColorPalette
                var colorPaletteLine = reader.ReadLine();
                if (colorPaletteLine != null)
                {
                    var colorValues = colorPaletteLine.Split(',');
                    if (colorValues[0] == "ColorPallete")
                    {
                        colorPalette = colorValues[1];
                    }
                }

                // Read second line for MarioPosition
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

                // Read third line for World
                var worldLine = reader.ReadLine();
                if (worldLine != null)
                {
                    var worldValues = worldLine.Split(',');
                    if (worldValues[0] == "World")
                    {
                        world = int.Parse(worldValues[1]);
                    }
                }

                //separator line
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 0 || string.IsNullOrWhiteSpace(values[0]))
                        continue;

                    if (values[0] == "PipeDestination" || values[0] == "LongPipeDestination")
                    {
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
                        string objectType = values[0];
                        int xPosition = int.Parse(values[1]);
                        int yPosition = int.Parse(values[2]);

                        entities.Add((objectType, xPosition, yPosition));
                    }
                }
            }

            return (colorPalette, marioPosition, world, entities, pipeDestinations);
        }
    }
}
