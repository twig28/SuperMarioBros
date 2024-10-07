﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

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
            Damaged// moving sprite
        }
        public Texture2D marioTexture { get; set; }
        public Vector2 PlayerPosition;
        public Vector2 UPlayerPosition;
        float PlayerSpeed;
        public GraphicsDeviceManager _graphics;
        public Game1 Game;
        public MotionPlayer MRplayer;
        public MotionPlayerLeft MLplayer;
        public Static Staplayer;
        public StaticL StaLplayer;
        public Jump Jumpplayer;
        public Damaged Damagedplayer;
        public JumpL JumpLplayer;
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

        public void intialize_player()
        {
            //Player initialize
            //move toward right
            MRplayer = new MotionPlayer(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            //standing toward right
            Staplayer = new Static(marioTexture, PlayerPosition);
            //standing toward left
            StaLplayer = new StaticL(marioTexture, PlayerPosition);
            //moving toward left
            MLplayer = new MotionPlayerLeft(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
            //juming toward right
            Jumpplayer = new Jump(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            //juming toward left
            JumpLplayer = new JumpL(marioTexture, PlayerPosition, PlayerSpeed, _graphics, Game);
            //damaged
            Damagedplayer = new Damaged(marioTexture, PlayerPosition, PlayerSpeed, _graphics);
        }


        public void Update(GameTime gameTime)
        {
            // update based on current sprite type
            //below for checking current state of mario
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
            else if (current == SpriteType.JumpL)
            {
                JumpLplayer.Position = UPlayerPosition;
                JumpLplayer.Update(gameTime);
                UPlayerPosition = JumpLplayer.Position;
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
                else if (current == SpriteType.Static)
                {
                    Staplayer.Position = UPlayerPosition;
                }

            }

        }


       public void Draw(SpriteBatch _spriteBatch)
        {
            //check sprint type for draw
            if (current == PlayerSprite.SpriteType.Static)
            {
                Staplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.StaticL)
            {
                StaLplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.Motion)
            {
                MRplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.MotionL)
            {
                MLplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.Jump)
            {
                Jumpplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.JumpL)
            {
                 JumpLplayer.Draw(_spriteBatch);
            }
            if (current == PlayerSprite.SpriteType.Damaged)
            {
                Damagedplayer.Draw(_spriteBatch);
            }
        }

    }
}
