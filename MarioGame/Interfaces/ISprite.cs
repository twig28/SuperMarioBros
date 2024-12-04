using Microsoft.Xna.Framework;


namespace MarioGame
{
    public interface ISprite
    {
        public void Update(GameTime gameTime);
        public void Draw();
    }
}
