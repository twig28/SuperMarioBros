using MarioGame.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

internal class BowserSprite : SpriteBase
{
    public bool ChangeDirection { get; set; }

    public BowserSprite(Texture2D _texture, SpriteBatch _spriteBatch, Vector2 _position) : base(_texture, _spriteBatch, _position)
    {
        ChangeDirection = false;

        Init();
    }
}
