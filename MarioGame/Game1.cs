﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Levels;
using System.Net.Http.Headers;
using MarioGame.Sprites;
using MarioGame.Collisions;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;

namespace MarioGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch spriteBatchText;

        private PlayerSprite player_sprite;
        private bool isThemePlaying = false;
        private Vector2 offset;

        private IController keyControl;
        private IController mouseControl;
        private float ballSpeed = 300f;

        public int CurrLevel { get; set; }
        public int CurrWorld { get; set; }
        int levelColor = -1;
        Color backgroundColor;
        private SoundLib soundLib;
        public void SetLevel(int level)
        {
            this.CurrLevel = level;
            ResetLevel();
        }
        public void SetBackgroundColor(int palette)
        {
            if (palette == 1)
            {
                backgroundColor = Color.CornflowerBlue;
            }
            else
            {
                backgroundColor = Color.Black;
            }
        }

        private List<IEnemy> enemies;
        private List<IBlock> blocks;
        private List<IItem> items;
        private List<IScenery> scenery;
        SpriteFont font;
        public static Game1 Instance { get; private set; }

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
            levelColor = -1;
            offset = new Vector2(0, 0);
        }
        public void ResetLevel()
        {
            this.LoadContent();
            offset = new Vector2(0, 0);
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resource";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 750;

            _graphics.ApplyChanges();
            Instance = this;
        }
        
        protected override void Initialize()
        {
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);

            player_sprite = new PlayerSprite(Content.Load<Texture2D>("smb_mario_sheet"), new Vector2(100, 500), 100f, _graphics, this);
            player_sprite.intialize_player();
            font = Content.Load<SpriteFont>("text");

            SetLevel(1);
            if (!isThemePlaying)
            {
                soundLib.themeInstance.Play();
                isThemePlaying = true;
            }

            base.Initialize();

            CurrWorld = 1;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchText = new SpriteBatch(GraphicsDevice);

            soundLib = new SoundLib();
            soundLib.LoadContent(Content);
            enemies = new List<IEnemy>();
            blocks = new List<IBlock>();
            items = new List<IItem>();
            scenery = new List<IScenery>();

            LoadLevels.LoadLevel(this, blocks, enemies, items, scenery, player_sprite, this.CurrLevel, _spriteBatch, levelColor);
            BallSprite.LoadContent(Content.Load<Texture2D>("smb_enemies_sheet"));
        }
        protected override void Update(GameTime gameTime)
        {
            keyControl.HandleInputs(player_sprite);
            mouseControl.HandleInputs(player_sprite);

            EnemyCollisionLogic.CheckEnemyBlockCollisions(enemies, blocks, gameTime, player_sprite);
            MarioBlockCollisionLogic.CheckMarioBlockCollision(player_sprite, blocks, items);
            EnemyCollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime, player_sprite);
            MarioEnemyCollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
            CollisionLogic.CheckMarioItemCollision(player_sprite, items, gameTime);
            CollisionLogic.CheckItemBlockCollision(blocks, items);
            PositionChecks.checkDeathByFalling(player_sprite, GraphicsDevice.Viewport.Height);
            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            player_sprite.Update(gameTime, player_sprite);

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IItem item in items)
            {
                item.Update(gameTime);
            }

            items.RemoveAll(item => item.GetLifeTime() < 0.0f);

            Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl, soundLib);
            Ball.UpdateAll(gameTime, GraphicsDevice.Viewport.Width, blocks);
            BallCollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime, false);

            base.Update(gameTime);
        }

        public void CustomColorLoad(int color)
        {
            levelColor = color;
            LoadContent();
        }

        public SoundLib GetSoundLib()
        {
            return soundLib;
        }
        //For Sprint 3 Debug Only
        private void DrawCollisionRectangles(SpriteBatch spriteBatch)
        {
            Texture2D rectTexture = new Texture2D(GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { Color.White });

            // Draw Mario's collision rectangle
            Rectangle marioRect = player_sprite.GetDestinationRectangle();
            spriteBatch.Draw(rectTexture, marioRect, Color.Red * 0.5f);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            offset = PositionChecks.GetCameraOffset(player_sprite.GetDestinationRectangle(), GraphicsDevice.Viewport.Width);
            Matrix transform = Matrix.CreateTranslation(new Vector3(offset, 0));
            _spriteBatch.Begin(transformMatrix: transform);
            foreach (IEnemy enemy in enemies)
            {
                if (PositionChecks.renderEnemy(enemy, offset, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) || enemy is KoopaShell)
                {
                    enemy.Update(gameTime);
                }
            }

            foreach (IScenery scene in scenery)
            {
                scene.Draw(_spriteBatch);
            }

            foreach (IEnemy enemy in enemies)
            {
                if (enemy is Piranha)
                    enemy.Draw();
            }

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            foreach (IItem item in items)
            {
                item.Draw(_spriteBatch);
            }

            foreach (IEnemy enemy in enemies)
            {
                if(enemy is not Piranha)
                enemy.Draw();
            }

            Ball.DrawAll(_spriteBatch);
            DrawCollisionRectangles(_spriteBatch);
            player_sprite.Draw(_spriteBatch, 14, 16, 3f, new List<Rectangle>(), 0, Color.White);

            _spriteBatch.End();

            spriteBatchText.Begin();

            TextDraw.DrawText(font, spriteBatchText, player_sprite, this.CurrWorld);
            if (player_sprite.current == PlayerSprite.SpriteType.Damaged)
            {
                TextDraw.Draw(font, spriteBatchText, player_sprite);
                // text.DrawGameOver(font, spriteBatchText, player_sprite);
            }
            spriteBatchText.DrawString(font, "Debug Mario Pos: " + player_sprite.UPlayerPosition, new Vector2(20, 600), Color.Yellow);

            spriteBatchText.End();
            
            base.Draw(gameTime);
        }
    }
}
