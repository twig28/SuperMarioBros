
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

            IItem itemRemove;
            foreach (IItem item in items)
            {
                Rectangle itemRectangle = item.getDestinationRectangle();
                Rectangle playerRectangle = player.GetDestinationRectangle();

               
            }
           
            
        }
    }
}
