using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Interfaces;

public interface IItem
{
    void Draw(SpriteBatch spriteBatch, Vector2 location);
    void Update(GameTime gameTime);
}

