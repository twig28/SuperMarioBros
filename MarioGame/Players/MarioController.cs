using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using MarioGame.Items;
using MarioGame.Blocks;
using System.Collections.Generic;
using MarioGame.Collisions;

namespace MarioGame
{
    public class MarioController
    {
        private Game1 Game;
        public MarioController(Game1 game)
        {
            Game = game;
        }

        public List<Rectangle> Switch(PlayerSprite.SpriteType current)
        {
            List<Rectangle> sourceRectangle = new List<Rectangle>();
            if (current == PlayerSprite.SpriteType.Static)
            {
                sourceRectangle.Clear();
                if (Game.player_sprite.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(209, 122, 18, 32));
                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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

            if (current == PlayerSprite.SpriteType.StaticL)
            {
                sourceRectangle.Clear();
                
                if (Game.player_sprite.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(180, 122, 18, 32));
                }
                
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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

            if (current == PlayerSprite.SpriteType.Motion)
            {
                sourceRectangle.Clear();
                if (Game.player_sprite.Fire)
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(237, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(263, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(287, 122, 18, 32));


                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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


            if (current == PlayerSprite.SpriteType.MotionL)
            {
                sourceRectangle.Clear();
                if (Game.player_sprite.Fire)
                {

                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(151, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(128, 122, 18, 32));
                    sourceRectangle.Add(new Rectangle(102, 122, 18, 32));



                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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



            if (current == PlayerSprite.SpriteType.Jump)
            {
                sourceRectangle.Clear();


                if (Game.player_sprite.Fire)
                {
                    sourceRectangle.Clear();
                    sourceRectangle.Add(new Rectangle(361, 122, 18, 32));

                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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



            if (current == PlayerSprite.SpriteType.JumpL)
            {
                sourceRectangle.Clear();
                if (Game.player_sprite.Fire)
                {
                    sourceRectangle.Clear();

                    sourceRectangle.Add(new Rectangle(25, 122, 18, 32));
                }
                else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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


            if (current == PlayerSprite.SpriteType.Damaged)
            {
                sourceRectangle.Clear();
                sourceRectangle.Add(new Rectangle(0, 16, 14, 16));

            }


            if (current == PlayerSprite.SpriteType.Falling)
            {
                sourceRectangle.Clear();


                if (Game.player_sprite.left)
                {
                    if (Game.player_sprite.Fire)
                    {
                        sourceRectangle.Clear();
                        sourceRectangle.Add(new Rectangle(25, 122, 18, 32));
                    }
                    else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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

                    if (Game.player_sprite.Fire)
                    {
                        sourceRectangle.Clear();

                        sourceRectangle.Add(new Rectangle(361, 122, 18, 32));

                    }
                    else if (!Game.player_sprite.Fire && Game.player_sprite.Big)
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

            }
            return sourceRectangle;
        }

    }
}
