using System.Drawing;
using System.Numerics;

namespace MarioGame.Blocks;

public class Block
{
    public Vector2 Position { get; set; }
    private Texture2D texture;
    private bool isBreakable;

    public Block(Vector2 Position, Texture2D texture, bool isBreakable)
    {
        this.Position = Position;
        this.texture = texture;
        this.isBreakable = isBreakable;
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.White);
    }

    public bool OnCollide(IPlayer player)
    {
        if (isBreakable)
        {
            Break();
        }
        else
        {

        }
    }

    public bool IsBreakable() { return isBreakable; }

    public void Trigger()
    {

    }

    public void Break()
    {

    }

}