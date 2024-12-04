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
            Crouch,
            Damaged// moving sprite
        }
        public enum Mode
        {
            None = 0,
            Fire = 1,
            Big  = 2,
            Star = 3,
            invincible = 4
        }

        private bool Direction = false;
        public Mode mode = Mode.None;
        private bool isgrounded = true;  
        private bool isfalling = false;
        private bool isjumping = false;
        public SpriteType current = SpriteType.Static;
        private float Velocity = 0f;
        public Vector2 UPlayerPosition;
        private MarioState state;
        private int invincibletime = -1;
        private int Startime = -1;
        public int coin = 0;
        private int score = 0;
        private int lives = 5;
        private bool DownSignal = false;
        

        public PlayerSprite(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            UPlayerPosition = position;
            state = new MarioState(texture, speed, Graphics, game);
            
        }

        public void setPosition(int x, int y)
        {
            UPlayerPosition = new Vector2(x, y);
        }

        public void intialize_player()
        {
            state.intialize_player(this, new Vector2(100, 500));
        }


        public void Update(GameTime gameTime, PlayerSprite mario)
        {
            // update based on current sprite type
            //below for checking current state of mario
            if(mode == PlayerSprite.Mode.Star || mode == PlayerSprite.Mode.invincible)
            {
                if(Startime == -1 && mode == PlayerSprite.Mode.Star)
                {
                    Startime = 1000;
                    invincibletime = -1;
                }
                else if (invincibletime == -1 && mode == PlayerSprite.Mode.invincible)
                {
                    invincibletime = 100;
                }
                else if(Startime == 0)
                {
                    mode = PlayerSprite.Mode.None;
                    Startime = -1;
                }
                else if (invincibletime == 0)
                {
                    mode = PlayerSprite.Mode.None;
                    invincibletime = -1;
                }

                else if(Startime>0)
                {
                    Startime--;
                }
                else if (invincibletime > 0)
                {
                    invincibletime--;
                }
            }
           
            state.Update(gameTime, this);

        }


       


        public void Draw(SpriteBatch _spriteBatch, int width, int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference, Color c)
        {
            if (mode == PlayerSprite.Mode.Star || mode == PlayerSprite.Mode.Fire || mode == PlayerSprite.Mode.Big)
            {
               
                if (mode == PlayerSprite.Mode.Star)
                {
                   
                    pos_difference = 24;
                    c = Color.SteelBlue;

                }
                //check sprint type for draw
                else if (mode == PlayerSprite.Mode.Fire)
                {
                   
                    pos_difference = 22;
                    c = Color.White;
                    // Game.Fire = true;
                }
                else if (mode == PlayerSprite.Mode.Big)
                {
                    pos_difference = 24;
                    c = Color.White;
                }


                if (current == SpriteType.Crouch)
                {
                    width = 17;
                    height = 22;
                    pos_difference = 30;
                }
                else
                {
                    width = 18;
                    height = 32;
                }
                
            }
            
            else if (mode == PlayerSprite.Mode.invincible)
            {
                width = 14;
                height = 16;
                pos_difference = 0;
                c = Color.SteelBlue;

            }
            else
            {
                width = 14;
                height = 16;
                pos_difference = 0;
                c= Color.White;
            }
            state.Draw(_spriteBatch, width, height, Scale, sourceRectangle, pos_difference, c, this);
           
        }

        // Added GetDestinationRectangle method
        public Rectangle GetDestinationRectangle()
        {
            Rectangle rectangle = new Rectangle();
           
            if(mode == PlayerSprite.Mode.Big || mode == PlayerSprite.Mode.Fire || mode == PlayerSprite.Mode.Star)
            {
                if (current == PlayerSprite.SpriteType.Crouch)
                {
                    rectangle = new Rectangle((int)(UPlayerPosition.X - 25), (int)(UPlayerPosition.Y), 51, 68);
                }
                else
                {
                    rectangle = new Rectangle((int)(UPlayerPosition.X - 20), (int)(UPlayerPosition.Y - 71), 42, 96);
                }
            }
            else
            {
                //rectangle = new Rectangle((int)(UPlayerPosition.X), (int)(UPlayerPosition.Y), 14, 16);
                rectangle = new Rectangle((int)(UPlayerPosition.X - 21), (int)(UPlayerPosition.Y - 24), 42,48);
            }
            return rectangle;
        }

       public void Reset()
        {
         direction = false;
          
        isJumping = false;
        isGrounded = true;
        isFalling = false;
        mode = PlayerSprite.Mode.None;
        current = PlayerSprite.SpriteType.Falling;
         UPlayerPosition = new Vector2(100, 500);
            coin = 0;
            score = 0;
            lives = 5;
        }

        public List<int> getCoinScoreLives()
        {
            List<int> source = new List<int>();
            source.Add(coin); 
            source.Add(score);
            source.Add(lives);
            return source;
        }

        public void SetCoin(int value)
        {
            coin += value;
        }

        public void SetScore(int value)
        {
            score += value;
        }

        public void SetLives(int value)
        {
            lives -= value;
        }

        public bool downSignal
        { get 
            { 
                return DownSignal; 
            } 
            set 
            { 
                DownSignal = value; 
            } 
        }

        public float velocity
        {
            get
            {
                return Velocity;
;
            }
            set
            {
                Velocity = value;
            }
        }

        public  bool direction
        {
            get
            {
                return Direction;
                
            }
            set
            {
                Direction = value;
            }
        }


        public bool isGrounded
        {
            get
            {
                return isgrounded;

            }
            set
            {
                isgrounded = value;
            }
        }

        public bool isFalling
        {
            get
            {
                return isfalling;

            }
            set
            {
                isfalling = value;
            }
        }

        public bool isJumping

        {
            get
            {
                return isjumping
;

    }
            set
            {
                isjumping = value;
            }
        }
    }
}
