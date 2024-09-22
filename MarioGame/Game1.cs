using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;

namespace MarioGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        TextSprite textS;
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

        IEnemy[] enemies;

        private double elapsedTime = 0.0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resource";
            IsMouseVisible = true;
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
            marioTexture = Content.Load<Texture2D>("smb_mario_sheet");
            enemyTextures = Content.Load<Texture2D>("smb_enemies_sheet");
            itemTextures = Content.Load<Texture2D>("smb_items_sheet");
            groundBlockTexture = Content.Load<Texture2D>("GroundBlock");
            blockTextures = Content.Load<Texture2D>("blocks");

            textS = new TextSprite();
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            items.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            textS.Draw(_spriteBatch, font);
             
            Vector2 itemLocation = new Vector2(200, 200);
            items.Draw(_spriteBatch, itemLocation);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
