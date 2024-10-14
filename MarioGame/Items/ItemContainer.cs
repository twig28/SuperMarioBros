using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items;

public class ItemContainer
{
    private static readonly List<IItem> items = new List<IItem>();
    private static int index = 0;

    public ItemContainer(Texture2D texture)
    {
        items.Add(new Coin(texture));
        items.Add(new FireFlower(texture));
        items.Add(new Mushroom(texture));
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
    public void Update(GameTime gameTime)
    {
        foreach (IItem item in items)
        {
            item.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int xValue = 100;
        int yValue = 600;
        Vector2 location = new Vector2(xValue, yValue);
        foreach (IItem item in items)
        {
            if (items.Count > 0)
            {
                item.Draw(spriteBatch, location);
                xValue += 100;
                location = new Vector2(xValue, yValue);
            }
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