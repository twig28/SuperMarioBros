using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items;

public class Item
{
    private static readonly List<IItem> items = new List<IItem>();
    private static int index = 0;

    public Item(Texture2D texture)
    {
        items.Add(new Coin(texture));
        items.Add(new Flower1(texture));
        items.Add(new Flower2(texture));
        items.Add(new Spring(texture));
        items.Add(new Star(texture));
    }

    public static void nextItem()
    {
        index++;
        if (index > items.Count - 1)
            index = 0;
    }

    public static void lastItem()
    {
        index--;
        if (index < 0)
            index = items.Count - 1;
    }
    public void Update(GameTime gameTime)
    {   
        items[index].Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        items[index].Draw(spriteBatch,location);
    }
}