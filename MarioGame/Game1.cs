using Microsoft.Xna.Framework;
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
        private PlayerSprite player_sprite;
        private Vector2 offset;

        IController keyControl;
        IController mouseControl;
        private float ballSpeed = 300f;

        //make private
        public bool Fire = false;

        public int CurrLevel { get; set; }
        private SoundLib soundLib; 
        public void SetLevel(int level)
        {
            this.CurrLevel = level;
            player_sprite.Reset();
            ResetLevel();
        }

        public int GetLevel()
        {
            return this.CurrLevel;
        }

        private List<IEnemy> enemies;
        private List<IBlock> blocks;
        private List<IItem> items;
        private List<IScenery> scenery;

        public static Game1 Instance { get; private set; }

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
            offset = new Vector2(0, 0);
        }

        public void ResetLevel()
        {
            this.LoadContent();
            offset = new Vector2(0, 0);
        }

        Color backgroundColor;
        public void SetBackgroundColor(int palette)
        {
            if (palette == 1) {
                backgroundColor = Color.CornflowerBlue;
            }
            else
            {
                backgroundColor = Color.Black;
            }
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
        }

        protected override void Initialize()
        {
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);

            player_sprite = new PlayerSprite(Content.Load<Texture2D>("smb_mario_sheet"), new Vector2(100, 500), 100f, _graphics, this);
            player_sprite.intialize_player();
            font = Content.Load<SpriteFont>("text");

            SetLevel(1);

            base.Initialize();

            //TEMP
            player_sprite.setPosition(3750, 500);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchText = new SpriteBatch(GraphicsDevice);

            //Load the sound
            soundLib=new SoundLib();
             soundLib.LoadContent(Content);
            enemies = new List<IEnemy>();
            blocks = new List<IBlock>();
            items = new List<IItem>();
            scenery = new List<IScenery>();

            LoadLevels.LoadLevel(this, blocks, enemies, items, scenery, player_sprite,this.CurrLevel);

            // Load fireball textures through the Ball class
            BallSprite.LoadContent(Content.Load<Texture2D>("smb_enemies_sheet"));
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.HandleInputs(player_sprite);
            mouseControl.HandleInputs(player_sprite);

            EnemyCollisionLogic.CheckEnemyBlockCollisions(enemies, blocks, gameTime);
            MarioBlockCollisionLogic.CheckMarioBlockCollision(player_sprite, blocks, items);
            EnemyCollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime);
            MarioEnemyCollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
            CollisionLogic.CheckMarioItemCollision(player_sprite, items, gameTime);
            CollisionLogic.CheckItemBlockCollision(blocks, items);

            if (PositionChecks.checkDeathByFalling(player_sprite.GetDestinationRectangle(), GraphicsDevice.Viewport.Height)) player_sprite.current = PlayerSprite.SpriteType.Damaged;

            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            player_sprite.Update(gameTime,player_sprite);

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IItem item in items)
            {
                item.Update(gameTime);
            }

            // Use the Ball class's static method to handle fireball inputs and update
            Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl,soundLib);
            Ball.UpdateAll(gameTime, GraphicsDevice.Viewport.Width);
            CollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime, false);
            CollisionLogic.CheckFireballBlockCollision(Ball.GetBalls(), blocks);

            base.Update(gameTime);
        }

         public SoundLib GetSoundLib()
        {
            return soundLib;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

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
                enemy.Draw();
            }

            Ball.DrawAll(_spriteBatch);

            if (player_sprite.current == PlayerSprite.SpriteType.Damaged)
            {
                GameOver.Draw(this, _spriteBatch,player_sprite);
            }

            player_sprite.Draw(_spriteBatch, 14, 16, 3f, new List<Rectangle>(), 0, Color.White);

            _spriteBatch.End();

            spriteBatchText.Begin();

            //draw string for record score of mario
            spriteBatchText.DrawString(font, "Coins: " + player_sprite.score, new Vector2(40, 0), Color.White);
            spriteBatchText.DrawString(font, "Debug Mario Pos: " + player_sprite.UPlayerPosition, new Vector2(20, 600), Color.Yellow);
            //Score.Draw(this, _spriteBatch,100);

            spriteBatchText.End();

            base.Draw(gameTime);
        }
    }
}
