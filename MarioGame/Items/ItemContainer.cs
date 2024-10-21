using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Items;

public class ItemContainer
{
    private static readonly List<IItem> items = new List<IItem>();

    public ItemContainer(Texture2D texture)
    {
        items.Add(new Coin(texture));
        items.Add(new FireFlower(texture));
        items.Add(new Mushroom(texture));
        items.Add(new Star(texture));
    }

    public void AddItem(IItem item)
    {
        items.Add(item);
    }

    public List<IItem> getItemList()
    {
        return items;
    }

    public static void Initialize()
    {
        items.Clear();
    }
    public void Update(GameTime gameTime, List<IBlock> blocks)
    {
        foreach (IItem item in items)
        {
            item.Update(gameTime);
            CollisionLogic collision = new CollisionLogic();
            collision.ItemBlockCollision(blocks,item);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IItem item in items)
        {
            switch (item.getName())
            {
                case "Coin":
                    item.Draw(spriteBatch,new(100, 550));
                    break;
                case "FireFlower":
                    item.Draw(spriteBatch, new(200, 550));
                    break;
                case "Mushroom":
                    item.Draw(spriteBatch, new(300, 550));
                    break;
                case "Star":
                    item.Draw(spriteBatch, new(400, 550));
                    break;
                default:
                    break;

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
            spriteBatch.Draw(rectangle, item.getDestinationRectangle(), Color.Green * 0.3f);
        }
    }

}