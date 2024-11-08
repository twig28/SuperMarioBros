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
using MarioGame.Score;
using MarioGame.Collisions;
using System.Runtime.CompilerServices;


namespace MarioGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public SpriteBatch spriteBatchText;

        //make private
        public PlayerSprite player_sprite;
        private Vector2 offset;

        IController keyControl;
        IController mouseControl;
        private float ballSpeed = 300f;

        //make private
        public bool Fire = false;

        public int CurrLevel { get; set; }

        public void SetLevel(int level)
        {
            this.CurrLevel = level;
            ResetGame();
        }

        public int GetLevel()
        {
            return this.CurrLevel;
        }

        private List<IEnemy> enemies;
        private List<IBlock> blocks;
        private List<IItem> items;
        private List<IScenery> scenery;

        private SoundLib soundLib;
        public static Game1 Instance { get; private set; }

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
            offset = new Vector2(0, 0);
        }

        SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resource";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 750;

            _graphics.ApplyChanges();
            soundLib = new SoundLib(); 
            Instance = this;
            SetLevel(1);
        }

        protected override void Initialize()
        {
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchText = new SpriteBatch(GraphicsDevice);
            Texture2D itemTextures = Content.Load<Texture2D>("smb_items_sheet");
            //Load the sound
             soundLib.LoadContent(Content);
            enemies = new List<IEnemy>();
            blocks = new List<IBlock>();
            items = new List<IItem>();
            scenery = new List<IScenery>();

            LoadLevels.LoadLevel(this, blocks, enemies, items, scenery, this.CurrLevel);

            // Load fireball textures through the Ball class
            BallSprite.LoadContent(Content.Load<Texture2D>("smb_enemies_sheet"));

            //Initialize Player
            player_sprite = new PlayerSprite(Content.Load<Texture2D>("smb_mario_sheet"), new Vector2(100, 500), 100f, _graphics, this);
            player_sprite.intialize_player();
            font = Content.Load<SpriteFont>("text");
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            EnemyCollisionLogic.CheckEnemyBlockCollisions(enemies, blocks);
            MarioBlockCollisionLogic.CheckMarioBlockCollision(player_sprite, blocks, items);
            EnemyCollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime);
            MarioEnemyCollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
            CollisionLogic.CheckMarioItemCollision(player_sprite, items, gameTime);
            CollisionLogic.CheckItemBlockCollision(blocks, items);

            if (PositionChecks.checkDeathByFalling(player_sprite.GetDestinationRectangle(), GraphicsDevice.Viewport.Height)) player_sprite.current = PlayerSprite.SpriteType.Damaged;

            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            player_sprite.Update(gameTime);

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IItem item in items)
            {
                item.Update(gameTime);
            }

            // Use the Ball class's static method to handle fireball inputs and update
            Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl);
            Ball.UpdateAll(gameTime, GraphicsDevice.Viewport.Width);
            CollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime, false);
            CollisionLogic.CheckFireballBlockCollision(Ball.GetBalls(), blocks);

            base.Update(gameTime);
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

         public SoundLib GetSoundLib()
        {
            return soundLib;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            offset = PositionChecks.GetCameraOffset(player_sprite.GetDestinationRectangle(), GraphicsDevice.Viewport.Width);
            Matrix transform = Matrix.CreateTranslation(new Vector3(offset, 0));
            _spriteBatch.Begin(transformMatrix: transform);

            foreach (IEnemy enemy in enemies)
            {
                if (PositionChecks.renderEnemy(enemy, offset, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height))
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
                enemy.Draw();
            }

            player_sprite.Draw(_spriteBatch, 14, 16,3f, new List<Rectangle>(),0,Color.White);

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            foreach (IItem item in items)
            {
                item.Draw(_spriteBatch);
            }
            
            Ball.DrawAll(_spriteBatch);

            if (player_sprite.current == PlayerSprite.SpriteType.Damaged)
            {
                GameOver.Draw(this, _spriteBatch);
            }

            _spriteBatch.End();

            spriteBatchText.Begin();

            //draw string for record score of mario
            spriteBatchText.DrawString(font, "Coins: " + player_sprite.score, new Vector2(40, 0), Color.White);
            //Score.Draw(this, _spriteBatch,100);

            spriteBatchText.End();

            base.Draw(gameTime);
        }
    }
}
