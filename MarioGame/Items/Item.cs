using System.Collections.Generic;
using System.ComponentModel;
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
        items.Add(new FireFlower(texture));
        items.Add(new Flower2(texture));
        items.Add(new Spring(texture));
        items.Add(new Star(texture));
    }

    public List<IItem> getItemList() 
    { 
        return items;
    }

    public static void Initialize()
    {
        index = 0;
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
        if (items.Count > 0)
        {
            items[index].Update(gameTime);
        }
    
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        if (items.Count > 0)
        {
            items[index].Draw(spriteBatch, location);
        }
    }
    public void DrawCollisionRectangles(SpriteBatch spriteBatch)
    {
        Texture2D rectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);    
        rectangle.SetData(new[] { Color.White });

        // Draw blocks' collision rectangles
        foreach (IItem item in items)
        {
            spriteBatch.Draw(rectangle, item.getDestinationRectangle(), Color.Green*0.3f);
        }
    }

}