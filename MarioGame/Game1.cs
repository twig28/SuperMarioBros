using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Levels;


namespace MarioGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public PlayerSprite player_sprite;

        IController keyControl;
        IController mouseControl;
        private float ballSpeed = 300f;

        public bool Fire = false;
        public bool Star = false;

        int currLevel = 1;

        private List<IEnemy> enemies;
        private List<IBlock> blocks;
        private List<IItem> items;

        // Block textures
        private Texture2D groundBlockTexture;
        private Texture2D blockTexture;

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
        }

        public void ChangeCurrLevel(int level)
        {
            currLevel = level;
            ResetGame();
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resource";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);

            // Initialize player's position
            PlayerSpeed = 100f;
            PlayerPosition = new Vector2(100, 500);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D itemTextures = Content.Load<Texture2D>("smb_items_sheet");

            enemies = new List<IEnemy>();
            blocks = new List<IBlock>();
            items = new List<IItem>();

            LoadLevels.LoadLevel(this, blocks, enemies, items, currLevel);

            // Load fireball textures through the Ball class
            BallSprite.LoadContent(Content.Load<Texture2D>("smb_enemies_sheet"));

            //Initialize Player
            Texture2D marioTexture = Content.Load<Texture2D>("smb_mario_sheet");
            player_sprite = new PlayerSprite(marioTexture, PlayerPosition, PlayerSpeed, _graphics, this);
            player_sprite.intialize_player();
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            CollisionLogic.CheckEnemyBlockCollisions(enemies, blocks);
            CollisionLogic.CheckMarioBlockCollision(player_sprite, blocks);
            CollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime);
            CollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
            CollisionLogic.CheckMarioItemCollision(player_sprite, items,gameTime);
            CollisionLogic.CheckItemBlockCollision(blocks,items);
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
            CollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime,false);
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


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (IEnemy enemy in enemies)
            {
                enemy.Update(gameTime);
                enemy.Draw();
            }

            _spriteBatch.Begin();

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
            //DrawCollisionRectangles(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
