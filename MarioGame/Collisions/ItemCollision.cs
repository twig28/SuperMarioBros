﻿
using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.Collisions
{
    internal class ItemCollision
    {
        private PlayerSprite player;
        public ItemCollision(PlayerSprite player)
        {
            this.player = player;
        }

        public void ItemCollisionHandler(List<IItem> items)
        {
            Rectangle playerRectangle = player.GetDestinationRectangle();
            Rectangle itemRectangle;
            Rectangle intersectionRectangle;
            List<IItem> itemList = new List<IItem>();
            foreach (IItem item in items)
            {
                itemRectangle = item.getDestinationRectangle();
                intersectionRectangle = Rectangle.Intersect(itemRectangle, playerRectangle);
                if (!intersectionRectangle.IsEmpty)
                {
                    itemList.Add(item);
                }
            }
            foreach (IItem item in itemList) { 
                items.Remove(item);
            }
        }
    }
}