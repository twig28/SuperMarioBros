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
        public int coin = 0;
        public int score = 0;
        public int lives = 5;

        public SpriteType current = SpriteType.Static;
        private MarioState state;
        public float velocity = 0f;
        public Vector2 UPlayerPosition;
        public Vector2 PlayerPosition;
        private int invincibletime = -1;
        private int Startime = -1;





        public PlayerSprite(Texture2D texture, Vector2 position, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            UPlayerPosition = position;
            PlayerPosition = position;
            state = new MarioState(texture, speed, Graphics, game);
        }

        public void setPosition(int x, int y)
        {
            UPlayerPosition = new Vector2(x, y);
        }

        public void intialize_player()
        {
            state.intialize_player(this);
        }


        public void Update(GameTime gameTime, PlayerSprite mario)
        {
            // update based on current sprite type
            //below for checking current state of mario
            if(Star || invincible)
            {
                if(Startime == -1 && Star)
                {
                    Startime = 1000;
                    invincible = false;
                    invincibletime = -1;
                }
                else if (invincibletime == -1 && invincible)
                {
                    invincibletime = 100;
                }
                else if(Startime == 0)
                {
                    Star = false;
                    Startime = -1;
                }
                else if (invincibletime == 0)
                {
                    invincible = false;
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
            current = PlayerSprite.SpriteType.Falling;
        crouched = false;
         UPlayerPosition = new Vector2(100, 500);
            coin = 0;
            score = 0;
            lives = 5;
        }

    }
}
