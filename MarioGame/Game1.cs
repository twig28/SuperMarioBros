using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
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
        Texture2D groundBlockTexture;
        Texture2D blockTextures;
        //FOR PLAYER VAR
        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public MotionPlayer MRplayer;
        public MotionPlayerLeft MLplayer;
        public Static Staplayer;
        public StaticL StaLplayer;
        public Jump Jumpplayer;
        public Damaged Damagedplayer;
        public JumpL JumpLplayer;
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
            PlayerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                     _graphics.PreferredBackBufferHeight / 2);
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
            //block intialize
            groundBlockTexture = Content.Load<Texture2D>("GroundBlock");
            blockTextures = Content.Load<Texture2D>("blocks");

            //enemy intialize
            enemies[0] = new Goomba(enemyTextures, _spriteBatch, 500, 500);
            enemies[1] = new Koopa(enemyTextures, _spriteBatch, 500, 500);
            //This Koopa is different in that it gets killed in update after 3 secs
            enemies[3] = new Koopa(enemyTextures, _spriteBatch, 500, 500);
            enemies[2] = new Piranha(enemyTextures, _spriteBatch, 500, 500);
            currEnemy = enemies[3];

            //Player initialize

            MRplayer = new MotionPlayer(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            Staplayer = new Static(marioTexture, PlayerPosition);
            StaLplayer = new StaticL(marioTexture, PlayerPosition);
            MLplayer = new MotionPlayerLeft(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            Jumpplayer = new Jump(marioTexture, PlayerPosition, PlayerSpeed, _graphics,this);
            JumpLplayer = new JumpL(marioTexture, PlayerPosition, PlayerSpeed, _graphics,this);
            Damagedplayer = new Damaged(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            //weapon intialize
            ballTextureRight = Content.Load<Texture2D>("fireballRight");  //load the ball texture to the left
            ballTextureLeft = Content.Load<Texture2D>("fireballLeft");//load the ball texture to the left
            //fireBoltTextureRight = Content.Load<Texture2D>("fireBoltRight");  //load the firebolt texture to the left
            //fireBoltTextureLeft = Content.Load<Texture2D>("fireBoltLeft");//load the firebolt texture to the left
        }


        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();
            // update based on current sprite type
            //below for checking current state of mario
            if (current == SpriteType.Motion)
            {
                MRplayer.Position = UPlayerPosition; //U means upated
                MRplayer.Update(gameTime);
                UPlayerPosition = MRplayer.Position;
            }
            else if (current == SpriteType.MotionL)
            {
                MLplayer.Position = UPlayerPosition;
                MLplayer.Update(gameTime);
                UPlayerPosition = MLplayer.Position;
            }
            else if (current == SpriteType.Jump)
            {
                Jumpplayer.Position = UPlayerPosition;
                Jumpplayer.Update(gameTime);
                UPlayerPosition = Jumpplayer.Position;
            }
            else if (current == SpriteType.JumpL)
            {
                JumpLplayer.Position = UPlayerPosition;
                JumpLplayer.Update(gameTime);
                UPlayerPosition = JumpLplayer.Position;
            }
            else if (current == SpriteType.Damaged)
            {
                Damagedplayer.Position = UPlayerPosition;
                Damagedplayer.Update(gameTime);
                UPlayerPosition = Damagedplayer.Position;

            }

            else
            {
                if (current == SpriteType.StaticL)
                {
                    StaLplayer.Position = UPlayerPosition;
                }
                else if (current == SpriteType.Static)
                {
                    Staplayer.Position = UPlayerPosition;
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

            
            if (enemies[3].Alive && gameTime.TotalGameTime.TotalSeconds > 3)
            {
                enemies[3].TriggerDeath(gameTime, true);
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
            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);
            if (current == SpriteType.Static)
            {
                Staplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.StaticL)
            {
                StaLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Motion)
            {
                MRplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.MotionL)
            {
                MLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Jump)
            {
                Jumpplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.JumpL)
            {
                JumpLplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.Damaged)
            {
                Damagedplayer.Draw(_spriteBatch);
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


