
using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.Collisions
{
    internal class ItemCollision
    {
        private IItem item;
        public ItemCollision(IItem item)
        {
            this.item = item;
        }

        public void ItemBlockCollision(List<IBlock> blocks)
        {
            Rectangle itemRectangle = item.getDestinationRectangle();
            Rectangle blockRectangle;
            Rectangle intersection;
            foreach (IBlock block in blocks)
            {
                blockRectangle = block.GetDestinationRectangle();
                intersection = Rectangle.Intersect(itemRectangle, blockRectangle);
                if (!intersection.IsEmpty)
                {
                    if(intersection.Width >= intersection.Height)
                    {
                        item.moveY(-intersection.Height);
                    }
                }
            }
        }
    }
}
