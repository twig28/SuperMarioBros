using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Interfaces;
using System.Runtime.CompilerServices;

namespace MarioGame
{
    public class Pipe : IBlock
    {

        private const int width = 120;
        private const int height = 250;
        private int xPosDest;
        private int yPosDest;
        private int levelDestination;

        public Vector2 Position { get; set; }
        public bool IsSolid { get; }
        public bool IsBreakable { get; }
        private bool isEntrance;
        private bool isLong;

        public bool getIsEntrance()
        {
            return isEntrance;
        }

        public (Vector2 position, int destination) GetDestination()
        {
            Vector2 position = new Vector2(xPosDest, yPosDest);

            return (position, levelDestination);
        }

        public void makePipeLong()
        {
            isLong = true;
        }

        public bool getIsLong()
        {
            return isLong;
        }

        public void setIsEntrance(int LevelDestination, int XPosDest, int YPosDest)
        {
            levelDestination = LevelDestination;
            xPosDest = XPosDest;
            yPosDest = YPosDest;
            isEntrance = true;
        }

        protected Texture2D Texture { get; set; }
        protected Rectangle SourceRectangle = new Rectangle(230, 385, 35, 66);
        protected Rectangle DestinationRectangle;

        public Pipe(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            isEntrance = false;
        }

        public void OnCollide()
        {
            //no collision impact
        }

        public Rectangle GetDestinationRectangle() { return DestinationRectangle; }

        public virtual void Update(GameTime gameTime)
        {
            // no update logic
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
    }
}
