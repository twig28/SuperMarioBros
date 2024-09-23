using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class Ball
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public float Speed;
        public bool IsVisible;
        private bool direction;  //to the left or not true-> left false-> right

        public Ball(Texture2D texture, Vector2 position, float speed, bool direction)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            this.direction = direction;
            IsVisible = true;
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            float updatedSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

           
            if (direction==true)// to the left
            {
                Position.X -= updatedSpeed;
            }
            else
            {
                Position.X += updatedSpeed;
            }

            //if it is out of the screen
            if (Position.X < 0 || Position.X > screenWidth)
            {
                IsVisible = false;  // make it invisible
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}
