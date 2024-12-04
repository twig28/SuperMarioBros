using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Items
{
    enum ItemType
    {
        Coin,
        FireFlower,
        Mushroom,
        Spring,
        Star,

        CNT // count
    }

    internal class ItemFactory
    {
        public static ItemBase CreateInstance(ItemType itemType, Texture2D texture, Vector2 position)
        {
            return new Coin(texture, position);
            switch (itemType)
            {
                case ItemType.Coin:
                    return new Coin(texture, position);
                case ItemType.FireFlower:
                    return new FireFlower(texture, position);
                case ItemType.Spring:
                    return new Spring(texture, position);
                case ItemType.Mushroom:
                    return new Mushroom(texture, position);
                case ItemType.Star:
                    return new Star(texture, position);
            }
            return null;
        }
    }
}
