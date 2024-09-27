using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using System;

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
        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public MotionPlayer MRplayer;
        public MotionPlayerLeft MLplayer;
        public Static Staplayer;
        public StaticL StaLplayer;
        public Jump Jumpplayer;
        public Damaged Damagedplayer;
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
            MRplayer = new MotionPlayer(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            Staplayer = new Static(marioTexture, PlayerPosition);
            StaLplayer = new StaticL(marioTexture, PlayerPosition);
            MLplayer = new MotionPlayerLeft(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            Jumpplayer = new Jump(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            Damagedplayer = new Damaged(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();
            // update based on current sprite type
           
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
                else if(current == SpriteType.Static) 
                {
                    Staplayer.Position = UPlayerPosition;
                }
               
            }

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
            if (current == SpriteType.Damaged)
            {
                Damagedplayer.Draw(_spriteBatch);
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
