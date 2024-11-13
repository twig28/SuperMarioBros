using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IBall
{
    void Update(GameTime gameTime, int screenWidth, List<IBlock> blocks);
    void Draw(SpriteBatch spriteBatch);
    bool IsVisible { get; set; }
    void Bounce();
    Rectangle GetDestinationRectangle();
}
