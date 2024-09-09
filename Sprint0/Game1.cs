using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        IController keyControl;
        IController mouseControl;
        ISprite staticS;
        ISprite staticM;
        ISprite animS;
        ISprite animM;
        TextSprite textS;
        private SpriteFont font;
        Texture2D marioTexture;
        private int currentSprite = 1;
        public int CurrentSprite   // Allows controllers to change current sprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }

        private double intervalDuration = 0.5; // Time interval in seconds
        private double elapsedTime = 0.0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void SetSprite(int num)
        {
            currentSprite = num;
        }

        protected override void Initialize()
        {
            base.Initialize();
            keyControl = new KeyboardController(this);
            mouseControl = new MouseController(this);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            marioTexture = Content.Load<Texture2D>("smb_mario_sheet");

            staticS = new StaticStationSprite();
            staticM = new StaticMovingSprite();
            animS = new AnimatedStationSprite();
            animM = new AnimatedMovingSprite();
            textS = new TextSprite();
        }

        protected override void Update(GameTime gameTime)
        {

            keyControl.HandleInputs();
            mouseControl.HandleInputs();

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (currentSprite == 1)
            {
                staticS.Draw(_spriteBatch, marioTexture, false);
            }
            else if (currentSprite == 2)
            {
                if (elapsedTime >= intervalDuration)
                {
                    animS.Draw(_spriteBatch, marioTexture, true);
                    elapsedTime -= elapsedTime;
                }
                else
                {
                    animS.Draw(_spriteBatch, marioTexture, false);
                }
            }
            else if (currentSprite == 3)
            {
               staticM.Draw(_spriteBatch, marioTexture, false);
            }
            else if(currentSprite == 4)
            {
                if (elapsedTime >= intervalDuration)
                {
                    animM.Draw(_spriteBatch, marioTexture, true);
                    elapsedTime -= elapsedTime;
                }
                else
                {
                    animM.Draw(_spriteBatch, marioTexture, false);
                }
            }

            //Draw text no matter what
            textS.Draw(_spriteBatch, font);

            base.Draw(gameTime);
        }

    }
}
