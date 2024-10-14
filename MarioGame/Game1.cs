using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Net;
using MarioGame.Collisions;

namespace MarioGame
{

    public class Game1 : Game
    { 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private Texture2D marioTexture;
        private Texture2D enemyTextures;
        private Texture2D itemTextures;
        private Texture2D sceneryTextures;
        private Texture2D ballTextureRight; 
        private Texture2D ballTextureLeft;
        private Texture2D multipleBlockTextures;

        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public PlayerSprite player_sprite;
        IController keyControl;
        IController mouseControl;
        Item items; 
        private List<IBall> balls = new List<IBall>();  
        private float ballSpeed = 300f;  

        //to be moved
        public bool zPressed = false;  
        public bool nPressed = false;  
        public bool keyboardPermitZ = false;
        public bool keyboardPermitN = false;
        //

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
            Item.Initialize();
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);
           
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
            items = new Item(itemTextures);

            blocks = new List<IBlock>
            {
                new Block(new Vector2(500, 200), blockTexture, new Rectangle(0, 0, 50, 50)),
                new GroundBlock(new Vector2(900, GraphicsDevice.Viewport.Height - 120), groundBlockTexture, new Rectangle(0, 0, 50, 50)),
                new MysteryBlock(new Vector2(560, 200), multipleBlockTextures, new Rectangle(80, 112, 15, 15))
            };

            PlayerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                   GraphicsDevice.Viewport.Height - 95);

            //create a row of blocks on the bottom, besides leftmost two so mario can fall
            for (int i = 0; i <= GraphicsDevice.Viewport.Width - 120; i += 60)
            {
                blocks.Add(new GroundBlock(new Vector2(i, GraphicsDevice.Viewport.Height - 60), groundBlockTexture, new Rectangle(0, 0, 50, 50)));
            }

            //enemy intialize
            enemies = new List<IEnemy>
            {
                new Goomba(enemyTextures, _spriteBatch, 500, 500),
                new Koopa(enemyTextures, _spriteBatch, 600, 500),
                new Piranha(enemyTextures, _spriteBatch, 1100, 500),
            };
            //For intialize all player
            player_sprite = new PlayerSprite(marioTexture, PlayerPosition, PlayerSpeed, _graphics, this);
            player_sprite.intialize_player();
            
            //weapon intialize
            ballTextureRight = Content.Load<Texture2D>("fireballRight");  //load the ball texture to the left
            ballTextureLeft = Content.Load<Texture2D>("fireballLeft");//load the ball texture to the left
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            CollisionLogic.CheckEnemyBlockCollisions(enemies, blocks);
            CollisionLogic.CheckMarioBlockCollision(player_sprite, blocks);

            // Remove destroyed blocks from the list
            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            // Get the current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            player_sprite.Update(gameTime);

            CollisionLogic.CheckFireballEnemyCollision(balls, enemies, gameTime);
            CollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime);

            //item collision
            ItemCollision itemCollision = new ItemCollision(player_sprite);
            itemCollision.ItemCollisionHandler(items.getItemList());

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            //check whether mario attack (this needs to be in it's own class)
            if (keyboardPermitZ)
            {
                balls.Add(new Ball(ballTextureLeft, player_sprite.UPlayerPosition, ballSpeed, true));
                keyboardPermitZ = false;
            }


            if (keyboardPermitN)
            {
                balls.Add(new BallLeft(ballTextureRight, player_sprite.UPlayerPosition, ballSpeed, false));
                keyboardPermitN = false;
            }

            foreach (var ball in balls)
            {
                ball.Update(gameTime, GraphicsDevice.Viewport.Width);
            }
            
            balls.RemoveAll(b => !b.IsVisible);
            CollisionLogic.CheckFireballBlockCollision(balls, blocks);

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

            // Draw blocks' collision rectangles
            foreach (IBlock block in blocks)
            {
                Rectangle blockRect = block.GetDestinationRectangle();
                spriteBatch.Draw(rectTexture, blockRect, Color.Blue * 0.5f);
            }
            items.DrawCollisionRectangles(spriteBatch);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);

            player_sprite.Draw(_spriteBatch);

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            foreach (IBall ball in balls)
            {
                ball.Draw(_spriteBatch);
            }

            // Draw collision rectangles for debugging
            DrawCollisionRectangles(_spriteBatch);

            _spriteBatch.End();

            foreach (IEnemy enemy in enemies)
            {
                    enemy.Update(gameTime);
                    enemy.Draw();
            }

            base.Draw(gameTime);
        }
    }
}


