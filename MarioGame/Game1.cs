using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Collisions;

namespace MarioGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        Texture2D marioTexture;
        Texture2D enemyTextures;
        Texture2D itemTextures;
        Texture2D sceneryTextures;
        private Texture2D multipleBlockTextures;

        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public PlayerSprite player_sprite;

        IController keyControl;
        IController mouseControl;
        ItemContainer items;

        private float ballSpeed = 300f;

        public bool Fire = false;

        private List<IEnemy> enemies;
        private List<IBlock> blocks;

        // Block textures
        private Texture2D groundBlockTexture;
        private Texture2D blockTexture;

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
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
            ItemContainer.Initialize();
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);

            // Initialize player's position
            PlayerSpeed = 100f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            marioTexture = Content.Load<Texture2D>("smb_mario_sheet");
            enemyTextures = Content.Load<Texture2D>("smb_enemies_sheet");
            itemTextures = Content.Load<Texture2D>("smb_items_sheet");
            groundBlockTexture = Content.Load<Texture2D>("resizedGroundBlock");
            blockTexture = Content.Load<Texture2D>("InitialBrickBlock");
            multipleBlockTextures = Content.Load<Texture2D>("blocks");
            sceneryTextures = Content.Load<Texture2D>("smb1_scenery_sprites");

            items = new ItemContainer(itemTextures);

            // Load fireball textures through the Ball class
            Ball.LoadContent(Content);

            blocks = new List<IBlock>
            {
               new Block(new Vector2(500, 200), blockTexture),
                new GroundBlock(new Vector2(900, GraphicsDevice.Viewport.Height - 120), groundBlockTexture),
               new MysteryBlock(new Vector2(560, 200), multipleBlockTextures)
            };
            PlayerPosition = new Vector2(100, GraphicsDevice.Viewport.Height - 83);
            // Create a row of blocks on the bottom, except for the leftmost two so Mario can fall
            for (int i = 0; i <= GraphicsDevice.Viewport.Width - 120; i += 60)
            {
                blocks.Add(new GroundBlock(new Vector2(i, GraphicsDevice.Viewport.Height - 60), groundBlockTexture));
            }

            enemies = new List<IEnemy>
            {
                new Goomba(enemyTextures, _spriteBatch, 500, 500),
                new Koopa(enemyTextures, _spriteBatch, 600, 500),
                new Piranha(enemyTextures, _spriteBatch, 1100, 500),
            };

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

            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            player_sprite.Update(gameTime);

            ItemCollision itemCollision = new ItemCollision(player_sprite);
            itemCollision.ItemCollisionHandler(items.getItemList());

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            // Use the Ball class's static method to handle fireball inputs and update
           Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl);

            Ball.UpdateAll(gameTime, GraphicsDevice.Viewport.Width);

            CollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime,false);
            CollisionLogic.CheckFireballBlockCollision(Ball.GetBalls(), blocks);

            items.Update(gameTime);
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

            items.Draw(_spriteBatch);
            player_sprite.Draw(_spriteBatch);

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            // Use Ball class's static method to draw all balls
            Ball.DrawAll(_spriteBatch);
            DrawCollisionRectangles(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
