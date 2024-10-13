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

        //FOR PLAYER VAR
        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public PlayerSprite player_sprite;
        //FOR CONTROLLER
        IController keyControl;
        IController mouseControl;
        //FOR ITEM
        Item items;
        //FOR WEAPON
        private Texture2D ballTextureRight;  // fireball to the right
        private Texture2D ballTextureLeft;  // fireball to the left
        private Texture2D multipleBlockTextures;  // firebolt to the left
        private List<IBall> balls = new List<IBall>();  // list of ball
        private float ballSpeed = 300f;  // ball speed
        public bool zPressed = false;  //status of z
        public bool nPressed = false;  // status of n
        public bool keyboardPermitZ = false;
        public bool keyboardPermitN = false;
        public bool Fire = false;
        //Temporary for sprint 2
        private List<IEnemy> enemies;

        // List to store and manage blocks
        private List<IBlock> blocks;
        private int currentBlockIndex = 0;  // Track the current block index

        // Block textures
        private Texture2D groundBlockTexture;
        private Texture2D blockTexture;

        public void ResetGame()
        {
            this.Initialize();
            this.LoadContent();
            currentBlockIndex = 0;
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
            PlayerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                     _graphics.PreferredBackBufferHeight / 2);
            //everytime update player's position
            PlayerSpeed = 100f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            marioTexture = Content.Load<Texture2D>("smb_mario_sheet");
            enemyTextures = Content.Load<Texture2D>("smb_enemies_sheet");
            //ITEM intialize
            itemTextures = Content.Load<Texture2D>("smb_items_sheet");
            items = new Item(itemTextures);

            // Load block textures
            groundBlockTexture = Content.Load<Texture2D>("resizedGroundBlock");
            blockTexture = Content.Load<Texture2D>("InitialBrickBlock");
            multipleBlockTextures = Content.Load<Texture2D>("blocks");

            // Initialize blocks
            blocks = new List<IBlock>
            {
                new GroundBlock(new Vector2(500, 350), groundBlockTexture, new Rectangle(0, 0, 50, 50)),
                new Block(new Vector2(500, 600), blockTexture, new Rectangle(0, 0, 50, 50)),
                new MysteryBlock(new Vector2(560, 600), multipleBlockTextures, new Rectangle(80, 112, 15, 15))
            };

            //enemy intialize
            enemies = new List<IEnemy>
            {
                new Goomba(enemyTextures, _spriteBatch, 500, 200),
                new Koopa(enemyTextures, _spriteBatch, 600, 500),
                new Piranha(enemyTextures, _spriteBatch, 500, 500),
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

            //foreach (var block in blocks)
            //{
            //    block.Update(gameTime);
            //}

            CollisionLogic.CheckEnemyBlockCollisions(enemies, blocks);

            // Remove destroyed blocks from the list
            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);

            // Get the current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Handle block switching using 't' and 'y'
            if (keyControl.IsKeyPressed(Keys.T, currentKeyboardState))
            {
                // Switch to the previous block
                currentBlockIndex = (currentBlockIndex - 1 + blocks.Count) % blocks.Count;
            }
            else if (keyControl.IsKeyPressed(Keys.Y, currentKeyboardState))
            {
                // Switch to the next block
                currentBlockIndex = (currentBlockIndex + 1) % blocks.Count;
            }

            //PLAYER UPDATE
            player_sprite.Update(gameTime);

            // Check for collisions between Mario and blocks
            CollisionLogic.CheckMarioBlockCollision(player_sprite, blocks);

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
            //update items
            items.Update(gameTime);
            base.Update(gameTime);
        }

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
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //foreach (var block in blocks)
            //{
            //    block.Draw(_spriteBatch);
            //}

            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);

            player_sprite.Draw(_spriteBatch);

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            foreach (Ball ball in balls)
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


