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
  
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            items = new Item(itemTextures);
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
