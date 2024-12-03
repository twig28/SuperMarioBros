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
    public class MarioState
    {
        private float PlayerSpeed;
        private GraphicsDeviceManager _graphics;
        private Game1 Game;
        private Texture2D marioTexture { get; set; }
        private MotionPlayer MRplayer;
        private Static Staplayer;
        private Jump Jumpplayer;
        private Damaged Damagedplayer;
        private Fall Fallplayer;
        private Crouch Crouchplayer;
        private MarioController Mario_state;
        public MarioState(Texture2D texture, float speed, GraphicsDeviceManager Graphics, Game1 game)
        {
            marioTexture = texture;
            PlayerSpeed = speed;
            _graphics = Graphics;
            Game = game;

        }
        public void intialize_player(PlayerSprite mario,Vector2 PlayerPosition)
        {
            //Player initialize
            //move toward right
            MRplayer = new MotionPlayer(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            Staplayer = new Static(marioTexture, PlayerPosition, Game);
            Jumpplayer = new Jump(marioTexture, PlayerPosition, Game);
            Damagedplayer = new Damaged(marioTexture, PlayerPosition, PlayerSpeed, _graphics); 
            Fallplayer = new Fall(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            Crouchplayer = new Crouch(marioTexture, PlayerPosition, Game);
            Mario_state = new MarioController(Game);

        }

        public void Update(GameTime gameTime, PlayerSprite mario)
        {
            if (mario.current == PlayerSprite.SpriteType.Motion || mario.current == PlayerSprite.SpriteType.MotionL)
            {
                MRplayer.Position = mario.UPlayerPosition; //U means upated
                MRplayer.Update(gameTime, mario);
                mario.UPlayerPosition = MRplayer.Position;
            }

            else if (mario.current == PlayerSprite.SpriteType.Jump || mario.current == PlayerSprite.SpriteType.JumpL)
            {
                Jumpplayer.Position = mario.UPlayerPosition;
                Jumpplayer.Update(gameTime, mario);
                mario.UPlayerPosition = Jumpplayer.Position;
            }

            else if (mario.current == PlayerSprite.SpriteType.Damaged)
            {
                Damagedplayer.Position = mario.UPlayerPosition;
                Damagedplayer.Update(gameTime, mario);
                mario.UPlayerPosition = Damagedplayer.Position;
            }
            else if (mario.current == PlayerSprite.SpriteType.Falling)
            {
                Fallplayer.Position = mario.UPlayerPosition;
                Fallplayer.Update(gameTime, mario);
                mario.UPlayerPosition = Fallplayer.Position;

            }
            else if (mario.current == PlayerSprite.SpriteType.Crouch)
            {
                Crouchplayer.Position = mario.UPlayerPosition;
            }
            else
            {

                Staplayer.Position = mario.UPlayerPosition;



            }
        }

        public IPlayer getPlayer(PlayerSprite mario)
        {
            
           
            if (mario.current == PlayerSprite.SpriteType.Motion || mario.current == PlayerSprite.SpriteType.MotionL)
            {

                return MRplayer;
            }
            else if (mario.current == PlayerSprite.SpriteType.Jump || mario.current == PlayerSprite.SpriteType.JumpL)
            {

                return Jumpplayer;
            }
            else if (mario.current == PlayerSprite.SpriteType.Damaged)
            {

                return Damagedplayer;
            }
            else if (mario.current == PlayerSprite.SpriteType.Falling)
            {
                return Fallplayer;
            }
            else if (mario.current == PlayerSprite.SpriteType.Crouch)
            {
                return Crouchplayer;
            }
            else
            {
                return Staplayer;
            }
        }


        public void Draw(SpriteBatch _spriteBatch, int width, int height, float Scale, List<Rectangle> sourceRectangle, int pos_difference, Color c, PlayerSprite mario)
        {
            sourceRectangle = Mario_state.Switch(mario.current, mario);
            
            getPlayer(mario).Draw(_spriteBatch, width, height, Scale, sourceRectangle, pos_difference, c);
        }

    }
}