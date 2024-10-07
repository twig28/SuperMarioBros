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
        public enum SpriteType
        {
            Static,
            StaticL,// Static sprite
            Motion,    // moving sprite
            MotionL,
            Jump,
            JumpL,
            Damaged// moving sprite
        }

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
        public SpriteType current = SpriteType.Static;
        //FOR CONTROLLER
        IController keyControl;
        IController mouseControl;
        //FOR ITEM
        Item items;
        //FOR WEAPON
        private Texture2D ballTextureRight;  // fireball to the right
        private Texture2D ballTextureLeft;  // fireball to the left
        private Texture2D fireBoltTextureRight;  // firebolt to the right
        private Texture2D fireBoltTextureLeft;  // firebolt to the left
        private Texture2D multipleBlockTextures;  // firebolt to the left
        private List<IBall> balls = new List<IBall>();  // list of ball
        private float ballSpeed = 300f;  // ball speed
        public bool zPressed = false;  //status of z
        public bool nPressed = false;  // status of n
        public bool keyboardPermitZ = false;
        public bool keyboardPermitN = false;
        public bool Fire = false;
        //Temporary for sprint 2
        IEnemy[] enemies = new IEnemy[4];
        IEnemy currEnemy;

        // List to store and manage blocks
        private List<IBlock> blocks;
        private int currentBlockIndex = 0;  // Track the current block index

        // Block textures
        private Texture2D groundBlockTexture;
        private Texture2D blockTexture;

        public void changeEnemy(bool forward)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (currEnemy.Equals(enemies[i]))
                {
                    //loop forward
                    if (forward && i == 3)
                    {
                        currEnemy = enemies[0];
                    }
                    else if (forward)
                    {
                        currEnemy = enemies[i + 1];
                    }
                    else if (i == 0)
                    {
                        currEnemy = enemies[3];
                    }
                    else
                    {
                        currEnemy = enemies[i - 1];
                    }
                    break;
                }
            }
        }

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
            UPlayerPosition = PlayerPosition;
            PlayerSpeed = 100f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            //To Be Implemented in its own class (maybe)
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
                new Block(new Vector2(500, 200), blockTexture, new Rectangle(0, 0, 50, 50)),
                new MysteryBlock(new Vector2(500, 200), multipleBlockTextures, new Rectangle(80, 112, 15, 15))
            };

            //enemy intialize
            enemies[0] = new Goomba(enemyTextures, _spriteBatch, 500, 500);
            enemies[1] = new Koopa(enemyTextures, _spriteBatch, 500, 500);
            //This Koopa is different in that it gets killed in update after 3 secs
            enemies[3] = new Koopa(enemyTextures, _spriteBatch, 500, 500);
            enemies[2] = new Piranha(enemyTextures, _spriteBatch, 500, 500);
            currEnemy = enemies[3];
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

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

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

            // update based on current sprite type
            //below for checking current state of mario
            if (current == SpriteType.Motion)
            {
                player_sprite.MRplayer.Position = UPlayerPosition; //U means upated
                player_sprite.MRplayer.Update(gameTime);
                UPlayerPosition = player_sprite.MRplayer.Position;
            }
            else if (current == SpriteType.MotionL)
            {
                player_sprite.MLplayer.Position = UPlayerPosition;
                player_sprite.MLplayer.Update(gameTime);
                UPlayerPosition = player_sprite.MLplayer.Position;
            }
            else if (current == SpriteType.Jump)
            {
                player_sprite.Jumpplayer.Position = UPlayerPosition;
                player_sprite.Jumpplayer.Update(gameTime);
                UPlayerPosition = player_sprite.Jumpplayer.Position;
            }
            else if (current == SpriteType.JumpL)
            {
                player_sprite.JumpLplayer.Position = UPlayerPosition;
                player_sprite.JumpLplayer.Update(gameTime);
                UPlayerPosition = player_sprite.JumpLplayer.Position;
            }
            else if (current == SpriteType.Damaged)
            {
                player_sprite.Damagedplayer.Position = UPlayerPosition;
                player_sprite.Damagedplayer.Update(gameTime);
                UPlayerPosition = player_sprite.Damagedplayer.Position;

            }

            else
            {
                if (current == SpriteType.StaticL)
                {
                    player_sprite.StaLplayer.Position = UPlayerPosition;
                }
                else if (current == SpriteType.Static)
                {
                    player_sprite.Staplayer.Position = UPlayerPosition;
                }

            }

            //check whether mario attack
            if (keyboardPermitZ)
            {
                balls.Add(new Ball(ballTextureLeft, UPlayerPosition, ballSpeed, true));
                keyboardPermitZ = false;
            }


            if (keyboardPermitN)
            {
                balls.Add(new BallLeft(ballTextureRight, UPlayerPosition, ballSpeed, false));
                keyboardPermitN = false;
            }

            foreach (var ball in balls)
            {
                ball.Update(gameTime, GraphicsDevice.Viewport.Width);
            }
            
            balls.RemoveAll(b => !b.IsVisible);
            //update items
            items.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();

            //foreach (var block in blocks)
            //{
            //    block.Draw(_spriteBatch);
            //}

            // Draw only the current block
            blocks[currentBlockIndex].Draw(_spriteBatch);

            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);
            //check sprint type for draw
            if (current == SpriteType.Static)
            {
                player_sprite.Staplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.StaticL)
            {
                player_sprite.StaLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Motion)
            {
                player_sprite.MRplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.MotionL)
            {
                player_sprite.MLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Jump)
            {
                player_sprite.Jumpplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.JumpL)
            {
                player_sprite.JumpLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Damaged)
            {
                player_sprite.Damagedplayer.Draw(_spriteBatch);
            }
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            foreach (IEnemy enemy in enemies)
            {
                if (currEnemy == enemy)
                {
                    enemy.Update(gameTime);
                    enemy.Draw();
                }
            }

            base.Draw(gameTime);
        }
    }
}


