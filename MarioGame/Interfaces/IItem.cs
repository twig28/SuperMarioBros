using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Interfaces;

public interface IItem
{
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
    public Rectangle GetDestinationRectangle();
    public string GetName();
    public void MoveX(int x);
    public void MoveY(int y);
    public void OnCollide();
    public bool HasCollision();
    public double GetLifeTime();
    public bool CanBeCollect();
}

