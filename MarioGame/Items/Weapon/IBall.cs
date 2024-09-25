using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IBall
{
    void Update(GameTime gameTime, int screenWidth);
    void Draw(SpriteBatch spriteBatch);
    void Update(object gameTime, int width);

    bool IsVisible { get; set; }
}
