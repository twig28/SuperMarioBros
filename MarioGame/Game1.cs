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
        private double damagedTimer = 0;
        private const double damagedDelay = 3; 

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
            damagedTimer = 0;
        }
        public void ResetLevel()
        {
            this.LoadContent();
            offset = new Vector2(0, 0);
            player_sprite.current = PlayerSprite.SpriteType.Falling;
            damagedTimer = 0;
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
            // Handle input controls
            keyControl.HandleInputs(player_sprite);
            mouseControl.HandleInputs(player_sprite);

            if (player_sprite.current == PlayerSprite.SpriteType.Damaged)
            {
                damagedTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (damagedTimer >= damagedDelay)
                {
                    damagedTimer = 0; 
                    Game1.Instance.ResetLevel(); 
                }
            }
            GameHelper.checkAllCollisions(enemies, blocks, items, player_sprite, gameTime, GraphicsDevice.Viewport.Height, player_sprite.current == PlayerSprite.SpriteType.Damaged);
            GameHelper.updateAll(enemies, blocks, items, player_sprite, gameTime, GraphicsDevice, soundLib, keyControl, ballSpeed);

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
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            offset = PositionChecks.GetCameraOffset(player_sprite.GetDestinationRectangle(), GraphicsDevice.Viewport.Width);
            Matrix transform = Matrix.CreateTranslation(new Vector3(offset, 0));
            _spriteBatch.Begin(transformMatrix: transform);
            GameHelper.drawAll(enemies, blocks, items, scenery, player_sprite, _spriteBatch, offset, GraphicsDevice, gameTime);

            spriteBatchText.Begin();

            TextDraw.DrawText(font, spriteBatchText, player_sprite, this.CurrWorld);
            if (player_sprite.getCoinScoreLives()[2] < 1)
            {
                TextDraw.Draw(font, spriteBatchText, player_sprite);
            }

            spriteBatchText.End();
            
            base.Draw(gameTime);
        }
    }
}
