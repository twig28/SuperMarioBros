using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MarioGame


{
    
    public class Game1 : Game
    {
        public enum SpriteType
        {
            Static,   // Static sprite
            Motion,    // moving sprite
            MotionL,    // moving sprite
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
        Texture2D MRTexture;
        Vector2 PlayerPosition;
        private Texture2D ballTextureRight;  // fireball to the right
        private Texture2D ballTextureLeft;  // fireball to the left
        private List<IBall> balls = new List<IBall>();  // list of ball
        private float ballSpeed = 300f;  // ball speed
        public bool qPressed = false;  //status of q
        public bool ePressed = false;  // status of e
        public bool keyboardPermitQ=false;
        public bool keyboardPermitE=false;


        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public MotionPlayer MRplayer;
        Texture2D MLTexture;
        public MotionPlayerLeft MLplayer;
        Texture2D StaTexture;
        public Static Staplayer;
        public SpriteType current = SpriteType.Static;
        IController keyControl;
        IController mouseControl;
        Item items;

        //Temporary for sprint 2
        IEnemy[] enemies = new IEnemy[3];
        IEnemy currEnemy;
        public void changeEnemy(bool forward)
        {
            for (int i = 0; i <= 2; i++)
            {
                if (currEnemy.Equals(enemies[i]))
                {
                    //loop forward
                    if (forward && i==2)
                    {
                        currEnemy = enemies[0];
                    }
                    else if (forward)
                    {
                        currEnemy = enemies[i + 1];
                    }
                    else if (i == 0)
                    {
                        currEnemy = enemies[2];
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
            itemTextures = Content.Load<Texture2D>("smb_items_sheet");
            items = new Item(itemTextures);
            groundBlockTexture = Content.Load<Texture2D>("GroundBlock");
            blockTextures = Content.Load<Texture2D>("blocks");

            enemies[0] = new Goomba(enemyTextures, _spriteBatch, 500, 500);
            enemies[1] = new Koopa(enemyTextures, _spriteBatch, 500, 500);
            enemies[2] = new Piranha(enemyTextures, _spriteBatch, 500, 500);
            currEnemy = enemies[2];
            MRTexture = Content.Load<Texture2D>("MA");
            StaTexture = Content.Load<Texture2D>("standing");
            MLTexture = Content.Load<Texture2D>("MAl");
            MRplayer = new MotionPlayer(MRTexture, PlayerPosition, PlayerSpeed, _graphics, 2, 3);
            Staplayer = new Static(StaTexture, PlayerPosition);
            MLplayer = new MotionPlayerLeft(MLTexture, PlayerPosition, PlayerSpeed, _graphics, 2, 3);
            ballTextureRight = Content.Load<Texture2D>("fireballRight");  //load the ball texture to the left
            ballTextureLeft = Content.Load<Texture2D>("fireballLeft");//load the ball texture to the left
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();
            // update based on current sprite type
           
            if (current == SpriteType.Motion)
            {
                MRplayer.Position = UPlayerPosition;
                MRplayer.Update(gameTime);
                UPlayerPosition = MRplayer.Position;
            }
            else if (current == SpriteType.MotionL)
            {
                MLplayer.Position = UPlayerPosition;
                MLplayer.Update(gameTime);
                UPlayerPosition = MLplayer.Position;
            }
            else
            {
                Staplayer.Position = UPlayerPosition;
                current = SpriteType.Static;
            }

           if (keyboardPermitQ)  // only released when q is pressed
                {
                    balls.Add(new Ball(ballTextureLeft, UPlayerPosition, ballSpeed, true));  // add new ball to left
                    keyboardPermitQ= false;  
                }
          
            // ball to right
            
                if (keyboardPermitE)  // released when pressed
                {
                    balls.Add(new BallLeft(ballTextureRight, UPlayerPosition, ballSpeed, false));  // add ball to right
                    keyboardPermitE = false;  
                }
           

            foreach (var ball in balls)
            {
                ball.Update(gameTime, GraphicsDevice.Viewport.Width); 
            }

            
            balls.RemoveAll(b => !b.IsVisible);
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
            if (current == SpriteType.Motion)
            {
                MRplayer.Draw(_spriteBatch);
            }
            if (current == SpriteType.MotionL)
            {
                MLplayer.Draw(_spriteBatch);
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
