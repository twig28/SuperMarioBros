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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        Texture2D marioTexture;
        Texture2D enemyTextures;
        Texture2D itemTextures;
        Texture2D sceneryTextures;
        Texture2D groundBlockTexture;
        Texture2D blockTextures;
        IController keyControl;
        IController mouseControl;
        Item items;

        //Temporary for sprint 2
        IEnemy[] enemies = new IEnemy[3];
        IEnemy currEnemy;
        public void changeEnemy(bool forward)
        {
            for (int i = 0; i < 4; i++)
            {
                if (currEnemy == enemies[i])
                {
                    //loop forward
                    if (forward && i + 1 > enemies.Length)
                    {
                        currEnemy = enemies[0];
                    }
                    //loop backward
                    else if (!forward && i - 1 < 0)
                    {
                        currEnemy = enemies[enemies.Length];
                    }
                    else if (forward)
                    {
                        currEnemy = enemies[i + 1];
                    }
                    else
                    {
                        currEnemy = enemies[i - 1];
                    }
                }
            }
        }

        private double elapsedTime = 0.0;

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
            base.Initialize();
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);
            items = new Item(itemTextures);
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
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);

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
