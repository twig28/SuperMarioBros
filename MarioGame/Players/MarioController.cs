using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;

namespace MarioGame
{
    public class MarioController
    {
        private Game1 Game;
        public MarioController(Game1 game)
        {
            Game = game;
        }

        public List<Rectangle> Switch(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            if (current == PlayerSprite.SpriteType.Static || current == PlayerSprite.SpriteType.StaticL)
            {
                sourceRectangle =  TextureforStatic(current, mario);
            }
            else if (current == PlayerSprite.SpriteType.Motion || current == PlayerSprite.SpriteType.MotionL)
            {
                sourceRectangle = TextureforMotion(current, mario);
            }
            else if (current == PlayerSprite.SpriteType.Jump || current == PlayerSprite.SpriteType.JumpL)
            {

                sourceRectangle = TextureforJumping(current, mario);
            }
            else if (current == PlayerSprite.SpriteType.Damaged)
            {
                sourceRectangle = TextureforDamge(current, mario);

            }


            else if (current == PlayerSprite.SpriteType.Falling)
            {
                sourceRectangle = TextureforFallinging(current, mario);
            }
            else if(current == PlayerSprite.SpriteType.Crouch)
            {
                sourceRectangle = TextureforCrounch(current, mario);
            }
            return sourceRectangle; 
        }






        public List<Rectangle> TextureforStatic(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            sourceRectangle.Clear();
            if (mario.direction)
            {
                sourceRectangle.Clear();
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(179, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(180, 122, 18, 32));
                }

                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(179, 52, 18, 32));
                }

                else
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(181, 0, 14, 16));
                }
            }
            else
            {
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(208, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(209, 122, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(208, 52, 18, 32));
                }
                else
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(210, 0, 14, 16));

                }
            }
            return sourceRectangle;
        }





        public List<Rectangle> TextureforMotion(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            if (current == PlayerSprite.SpriteType.Motion)
            {
                sourceRectangle.Clear();
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(238, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(270, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(299, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(237, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(263, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(287, 122, 18, 32));


                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(238, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(270, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(299, 52, 18, 32));


                }
                else
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(240, 0, 14, 16));
                    sourceRectangle.Add(new Rectangle(270, 0, 14, 16));
                    sourceRectangle.Add(new Rectangle(300, 0, 14, 16));
                }
            }


            else if (current == PlayerSprite.SpriteType.MotionL)
            {
                sourceRectangle.Clear();
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(150, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(120, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(89, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(151, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(128, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(102, 122, 18, 32));



                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(150, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(120, 52, 18, 32));
                    sourceRectangle.Add(new Rectangle(89, 52, 18, 32));

                }
                else
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(150, 0, 14, 16));
                    sourceRectangle.Add(new Rectangle(120, 0, 14, 16));
                    sourceRectangle.Add(new Rectangle(88, 0, 14, 16));

                }
            }
            return sourceRectangle;
        }








        public List<Rectangle> TextureforJumping(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            if (current == PlayerSprite.SpriteType.Jump)
            {

                sourceRectangle.Clear();

                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(358, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(361, 122, 18, 32));

                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(358, 52, 18, 32));

                }
                else
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(358, 0, 17, 17));

                }
                return sourceRectangle;

            }



            else if (current == PlayerSprite.SpriteType.JumpL)
            {
                sourceRectangle.Clear();
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(29, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(25, 122, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(29, 52, 18, 32));

                }
                else
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(29, 0, 17, 17));
                }
            }
            return sourceRectangle;

        }





        public List<Rectangle> TextureforFallinging(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();

            sourceRectangle.Clear();


            if (mario.direction)
            {
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(29, 52, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(25, 122, 18, 32));
                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(29, 52, 18, 32));

                }
                else
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(29, 0, 17, 17));

                }
            }
            else
            {
                //check status
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(358, 52, 18, 32));
                }

                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(361, 122, 18, 32));

                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(358, 52, 18, 32));

                }
                else
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(358, 0, 17, 17));
                }
            }
            return sourceRectangle;

        }

        public List<Rectangle> TextureforCrounch(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            sourceRectangle.Clear();
            //check status
            if (mario.direction)
            {
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(0, 57, 17, 22));
                }

                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(0, 127, 17, 22));

                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(0, 57, 17, 22));

                }
                
            }
            else
            {
                if (mario.mode == PlayerSprite.Mode.Star)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(388, 57, 17, 22));
                }

                else if (mario.mode == PlayerSprite.Mode.Fire)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(388, 127, 17, 22));

                }
                else if (mario.mode == PlayerSprite.Mode.Big)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(388, 57, 17, 22));

                }
            }
            return sourceRectangle;
        }


        public List<Rectangle> TextureforDamge(PlayerSprite.SpriteType current, PlayerSprite mario)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            sourceRectangle.Clear();
            sourceRectangle.Add(new Rectangle(0, 16, 14, 16));
            return sourceRectangle;
        }
    }
    
}
