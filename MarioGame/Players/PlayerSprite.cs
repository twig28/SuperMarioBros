using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;

namespace MarioGame
{
    //would have texture, position, state, and more properties
    public class PlayerSprite
    {
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



        public PlayerSprite(Texture2D texture, Vector2 position,float speed, GraphicsDeviceManager Graphics, Game1 game)
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
       
    }
}
