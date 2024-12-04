using MarioGame.Blocks;
using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    internal class ItemCollisionLogic
    {
        public static void CheckItemBlockCollision(List<IBlock> blocks, List<IItem> items)
        {
            Rectangle itemRectangle;
            Rectangle blockRectangle;
            Rectangle intersection;
            foreach (IItem item in items)
            {
                if (item.HasCollision() == false)
                {
                    continue;
                }
                String itemName = item.GetName();
                // if (itemName.Equals("Mushroom") || itemName.Equals("Star"))
                {
                    itemRectangle = item.GetDestinationRectangle();
                    foreach (IBlock block in blocks)
                    {
                        blockRectangle = block.GetDestinationRectangle();
                        intersection = Rectangle.Intersect(itemRectangle, blockRectangle);
                        if (!intersection.IsEmpty)
                        {
                            if (intersection.Width >= intersection.Height)
                            {
                                if (itemName.Equals("Mushroom") || itemName.Equals("Star"))
                                    item.MoveY(-intersection.Height);
                                item.OnCollide();
                                block.OnCollide();
                            }
                        }
                    }
                }
            }
        }

        public static void CheckMarioItemCollision(PlayerSprite mario, List<IItem> items, GameTime gt)
        {
            IItem itemToRemove = null;

            foreach (IItem item in items)
            {
                if (item.CanBeCollect() && mario.GetDestinationRectangle().Intersects(item.GetDestinationRectangle()))
                {
                    itemToRemove = item;
                    ApplyItemEffect(mario, item);
                }
            }

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        private static void ApplyItemEffect(PlayerSprite mario, IItem item)
        {
            switch (item.GetName())
            {
                case "Star":
                    ActivateStarPower(mario);
                    break;

                case "FireFlower":
                    ActivateFirePower(mario);
                    break;

                case "Mushroom":
                    GrowMario(mario);
                    break;

                case "Coin":
                    IncreaseScore(mario);
                    Game1.Instance.GetSoundLib().PlaySound("coin");
                    break;
            }
        }

        private static void ActivateStarPower(PlayerSprite mario)
        {
            mario.mode = PlayerSprite.Mode.Star;
        }

        private static void ActivateFirePower(PlayerSprite mario)
        {
            if (mario.mode != PlayerSprite.Mode.Star)
            {
                mario.mode = PlayerSprite.Mode.Fire;
            }
        }

        private static void GrowMario(PlayerSprite mario)
        {
            if (mario.mode != PlayerSprite.Mode.Star && mario.mode != PlayerSprite.Mode.Big)
            {
                mario.mode = PlayerSprite.Mode.Big;
            }
        }

        private static void IncreaseScore(PlayerSprite mario)
        {
            mario.score += 200;
            mario.coin += 1;
        }


    }
}




