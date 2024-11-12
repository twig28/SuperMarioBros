using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Sprites;
namespace MarioGame
{
   

    //would have texture, position, state, and more properties
    public class PlayerSprite : IPlayer
    {
        public enum SpriteType
        {
            Static,
            StaticL,// Static sprite
            Motion,    // moving sprite
            MotionL,
            Jump,
            JumpL,
            Falling,
            Damaged// moving sprite
        }
        public bool left = false;
        public bool Fire = false;
        public bool Big = false;
        public bool isJumping = false;
        public bool isGrounded = true;
        public bool isFalling= false;
        public bool invincible = false;
        public bool Star = false;
        public bool crouched = false;
        private int time = -1;
        public int score = 0;
    

        public Texture2D marioTexture { get; set; }
        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public float velocity = 0f;
        public GraphicsDeviceManager _graphics;
        public Game1 Game;

        private MotionPlayer MRplayer;
        private Static Staplayer;
        private Jump Jumpplayer;
        private Damaged Damagedplayer;
        private Fall Fallplayer;
        private MarioController Mario_state;
        public SpriteType current = SpriteType.Static;

      

        public PlayerSprite(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            marioTexture = texture;
            PlayerPosition = position;
            UPlayerPosition = position;
            PlayerSpeed = speed;
            _graphics = Graphics;
            Game = game;
        }

        public void setPosition(int x, int y)
        {
            this.UPlayerPosition = new Vector2(x, y);
        }

        public void intialize_player()
        {
            //Player initialize
            //move toward right
            MRplayer = new MotionPlayer(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            Staplayer = new Static(marioTexture, PlayerPosition,Game);
            Jumpplayer = new Jump(marioTexture, PlayerPosition, Game);
            Damagedplayer = new Damaged(marioTexture, PlayerPosition, PlayerSpeed, _graphics);            Fallplayer = new Fall(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            Mario_state = new MarioController(Game);

        }


        public void Update(GameTime gameTime, PlayerSprite mario)
        {
            // update based on current sprite type
            //below for checking current state of mario
            if(Star || invincible)
            {
                if(time == -1 && Star)
                {
                    time = 1000;
                }
                else if (time == -1 && invincible)
                {
                    time = 100;
                }
                else if(time == 0)
                {
                    Star = false;
                    invincible = false;
                    time = -1;
                }
                else
                {
                    time--;
                }
            }
            if (current == SpriteType.Motion || current == SpriteType.MotionL)
            {
                MRplayer.Position = UPlayerPosition; //U means upated
                MRplayer.Update(gameTime,this);
                UPlayerPosition = MRplayer.Position;
            }
            
            else if (current == SpriteType.Jump || current == SpriteType.JumpL)
            {
                Jumpplayer.Position = UPlayerPosition;
                Jumpplayer.Update(gameTime,this);
                UPlayerPosition = Jumpplayer.Position;
            }
           
            else if (current == SpriteType.Damaged)
            {
                Damagedplayer.Position = UPlayerPosition;
                Damagedplayer.Update(gameTime,this);
                UPlayerPosition = Damagedplayer.Position;
            }
            else if (current == SpriteType.Falling)
            {
                Fallplayer.Position = UPlayerPosition;
                Fallplayer.Update(gameTime,this);
                UPlayerPosition = Fallplayer.Position;

            }

            else
            {
               
                    Staplayer.Position = UPlayerPosition;
              
               

            }

        }


       


        public void Draw(SpriteBatch _spriteBatch, int width, int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference, Color c)
        {
            if(Star)
            {
                width = 18;
                height = 32;
                pos_difference = 24;
                c = Color.SteelBlue;

            }
            //check sprint type for draw
            else if(Fire)
            {
                width = 18;
                height = 32;
                pos_difference = 22;
                c= Color.White;
               // Game.Fire = true;
            }
            else if(Big)
            {
                width = 18;
                height = 32;
                pos_difference = 24;
                c=Color.White;
            }
            else if (invincible)
            {
                width = 16;
                height = 16;
                pos_difference = 0;
                c = Color.SteelBlue;

            }
            else
            {
                width = 16;
                height = 16;
                pos_difference = 0;
                c= Color.White;
            }
            sourceRectangle = Mario_state.Switch(current,this);
            if (current == PlayerSprite.SpriteType.Static || current == PlayerSprite.SpriteType.StaticL)
            {
                Staplayer.Draw(_spriteBatch,width,height,Scale,sourceRectangle,pos_difference,c);
            }
            if (current == PlayerSprite.SpriteType.Motion || current == PlayerSprite.SpriteType.MotionL)
            {
               
                MRplayer.Draw(_spriteBatch,width,height,Scale,sourceRectangle, pos_difference,c);
            }
            if (current == PlayerSprite.SpriteType.Jump || current == PlayerSprite.SpriteType.JumpL)
            {
              
                Jumpplayer.Draw(_spriteBatch,width,height,Scale,sourceRectangle, pos_difference,c);
            }
            if (current == PlayerSprite.SpriteType.Damaged)
            {
              
                Damagedplayer.Draw(_spriteBatch,width,height,Scale,sourceRectangle, pos_difference,c);
            }
            if (current == PlayerSprite.SpriteType.Falling)
            {
                Fallplayer.Draw(_spriteBatch,width,height,Scale,sourceRectangle, pos_difference,c);
            }
        }

        // Added GetDestinationRectangle method
        public Rectangle GetDestinationRectangle()
        {
            Rectangle rectangle = new Rectangle();
           
            if(Fire || Big || Star)
            {
                rectangle = new Rectangle((int)(UPlayerPosition.X - 27), (int)(UPlayerPosition.Y - 71), 54, 96);
            }
            else
            {
               
                rectangle = new Rectangle((int)(UPlayerPosition.X - 21), (int)(UPlayerPosition.Y - 24), 42,48);

            }
            return rectangle;
        }

       public void Reset()
        {
         left = false;
            Fire = false;
         Big = false;
        isJumping = false;
         isGrounded = true;
        isFalling = false;
         invincible = false;
        current = PlayerSprite.SpriteType.Static;
        crouched = false;
         UPlayerPosition = new Vector2(100, 500);
        }

    }
}
