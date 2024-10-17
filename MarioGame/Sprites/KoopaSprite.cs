using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MarioGame
{
    internal class KoopaSprite : ISprite
    {
        private int SpriteWidth = 60;
        private int SpriteHeight = 85;
        private int SourceX = 210;
        private int SourceY = 0;
        private const int SourceWidth = 16;
        private const int SourceHeight = 24;
        private SpriteBatch sb;
        private Texture2D texture;
        private Rectangle DestinationRectangle;
        private Rectangle SourceRectangle;
        private const int spacingInterval = 30;
        private int currSprite = 0;
        private bool hasChangedToShellAnim = false;
        private double timeElapsed = 0;
        private double timeElapsedSinceLastUpdate = 0;
        private double elapsedTimeForShellAnim = 0.7;

        public bool ChangeDirection { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }

        public Rectangle GetDestinationRectangle() => DestinationRectangle;

        public KoopaSprite(Texture2D Texture, SpriteBatch SpriteBatch, int X, int Y)
        {
            sb = SpriteBatch;
            posX = X;
            posY = Y;
            texture = Texture;
            DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            SourceRectangle = new Rectangle(SourceX, SourceY, SourceWidth, SourceHeight);
            ChangeDirection = false;
        }

        public void Draw()
        {
            DestinationRectangle = new Rectangle(posX, posY, SpriteWidth, SpriteHeight);
            sb.Begin();
            sb.Draw(texture, DestinationRectangle, SourceRectangle, Color.White);
            sb.End();
        }

        public bool ChangeToShell(GameTime gm)
        {
            timeElapsed = gm.TotalGameTime.TotalSeconds;
            if (hasChangedToShellAnim && timeElapsed - timeElapsedSinceLastUpdate > elapsedTimeForShellAnim)
            {
                timeElapsedSinceLastUpdate = timeElapsed;
                SourceRectangle.X = 360;
                return true;
            }
            else
            {
                SpriteWidth = 75;
                SpriteHeight = 75;
                SourceRectangle.X = 330;
                hasChangedToShellAnim = true;
                return false;
            }
        }

        public void Update(GameTime gm)
        {
            if (ChangeDirection)
            {
                SourceRectangle.X = (SourceRectangle.X >= 200) ? 150 : 210;
                currSprite = 0;
                ChangeDirection = false;
            }
                if (currSprite == 0)
                {
                    SourceRectangle.X += spacingInterval;
                    currSprite = 1;
                }
                else
                {
                    SourceRectangle.X -= spacingInterval;
                    currSprite = 0;
                }
        }

        public void SetDeathFrame()
        {
            SourceRectangle = new Rectangle(330, 20, SourceWidth, SourceHeight);
        }
    }
}
