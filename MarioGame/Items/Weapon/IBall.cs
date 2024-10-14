using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IBall
{
    void Update(GameTime gameTime, int screenWidth);
    void Draw(SpriteBatch spriteBatch);
    

    bool IsVisible { get; set; }

    Rectangle GetDestinationRectangle();
}
