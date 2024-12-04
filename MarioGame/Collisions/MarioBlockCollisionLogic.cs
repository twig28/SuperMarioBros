using MarioGame.Blocks;
using MarioGame.Interfaces;
using MarioGame.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MarioGame.Collisions
{
    internal class MarioBlockCollisionLogic
    {

        public static void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks, List<IItem> items)
        {
            if (mario.current == PlayerSprite.SpriteType.Damaged) return;

            List<IBlock> blocksToRemove = new List<IBlock>();
            List<IBlock> standingBlocks = new List<IBlock>();

            foreach (IBlock block in blocks )
            {
                Rectangle blockRect = block.GetDestinationRectangle();
                Rectangle marioRect = mario.GetDestinationRectangle();
                if (blockRect.Intersects(marioRect) && block is Flagpole f)
                {
                    HandleFlagpoleCollision(f, mario);
                    continue;
                }
                //Pipe that occurs after castles
                else if (block is LPipe lpipe && CollisionLogic.GetCollisionDirection(blockRect, marioRect) != CollisionLogic.CollisionDirection.None)
                {
                    //TODO move mario animation (optional)
                    Game1.Instance.SetLevel(Game1.Instance.CurrLevel + 1);
                    continue;
                }
                //Pipes that go places
                else if (block is Pipe p && (p.getIsEntrance() && CollisionLogic.GetCollisionDirection(blockRect, marioRect) == CollisionLogic.CollisionDirection.Above && mario.downSignal||
                    p.getIsLong() && CollisionLogic.GetCollisionDirection(blockRect, marioRect) == CollisionLogic.CollisionDirection.Side))
                {
                    (Vector2 destination, int level) = p.GetDestination();
                    Game1.Instance.SetLevel(level);
                    mario.setPosition((int)destination.X, (int)destination.Y);
                    continue;
                }
                else if (marioRect.Intersects(blockRect))
                {
                    if (IsStandingOnBlock(mario, blockRect, marioRect))
                    {
                        HandleStandingOnBlock(mario, block, standingBlocks);
                    }
                    else {
                        if (standingBlocks.Contains(block))
                        {
                            standingBlocks.Remove(block); 
                        }
                        else if (IsBelowBlock(mario, blockRect, marioRect))
                        {
                            HandleBelowBlockCollision(mario, block, items, blocksToRemove);
                        }
                        else
                        {
                            HandleSideCollision(mario, block);
                        }
                    }
                }
              
            }

            CheckIfFalling(mario, standingBlocks);
            RemoveBreakableBlocks(blocks, blocksToRemove, items);
        }

        private static bool IsStandingOnBlock(PlayerSprite mario, Rectangle blockRect, Rectangle marioRect)
        {
            bool isStanding = false;
            //if(mario.UPlayerPosition.Y < blockRect.Top && marioRect.Right  > blockRect.Left 
            //   && marioRect.Left  < blockRect.Right )
            if (marioRect.Bottom >= blockRect.Top && marioRect.Right > blockRect.Left
              && marioRect.Left < blockRect.Right && marioRect.Top < blockRect.Top)
            {
                isStanding = true;
            }
            return isStanding;
                

        }

        private static bool IsBelowBlock(PlayerSprite mario, Rectangle blockRect, Rectangle marioRect)
        {
            return mario.UPlayerPosition.Y > blockRect.Bottom &&
                   !mario.isGrounded &&
                   (marioRect.Right > blockRect.Left && marioRect.Left < blockRect.Right);
        }

        private static void HandleStandingOnBlock(PlayerSprite mario, IBlock block, List<IBlock> standingBlocks)
        {
            if (!standingBlocks.Contains(block)) { standingBlocks.Add(block); }

            if (!mario.isGrounded)
            {
                mario.isGrounded = true;
                mario.isJumping = false;
                mario.velocity = 0f;
                mario.current = mario.direction ? PlayerSprite.SpriteType.StaticL : PlayerSprite.SpriteType.Static;
            }

            AdjustMarioYPosition(mario, block.GetDestinationRectangle().Top);
        }

        private static void AdjustMarioYPosition(PlayerSprite mario, int blockTop)
        {
            if (mario.mode == PlayerSprite.Mode.Big || mario.mode == PlayerSprite.Mode.Fire || mario.mode == PlayerSprite.Mode.Star)
            {
                if (mario.current == PlayerSprite.SpriteType.Crouch)
                {
                    mario.UPlayerPosition.Y = blockTop - mario.GetDestinationRectangle().Height / 2 - 25;

                }
                else
                {
                    mario.UPlayerPosition.Y = blockTop - mario.GetDestinationRectangle().Height / 2 + 26;
                }
            }
            else
            {
                // mario.UPlayerPosition.Y = blockTop - mario.GetDestinationRectangle().Height / 2 + 2;
                 mario.UPlayerPosition.Y = blockTop - mario.GetDestinationRectangle().Height / 2 + 2;

            }
        }

        private static void HandleBelowBlockCollision(PlayerSprite mario, IBlock block, List<IItem> items, List<IBlock> blocksToRemove)
        {
            mario.velocity = 0f;

            if (block is MysteryBlock mystery && !mystery.IsOpened && !mystery.isBumped)
            {
                OpenMysteryBlock(mystery, items, block);
            }

            if (mario.mode == PlayerSprite.Mode.Big || mario.mode == PlayerSprite.Mode.Fire|| mario.mode == PlayerSprite.Mode.Star)
            {
                if (block.IsBreakable)
                {
                    block.OnCollide();
                    
                    blocksToRemove.Add(block);
                }
                mario.UPlayerPosition.Y = block.GetDestinationRectangle().Bottom + mario.GetDestinationRectangle().Height / 2 + 24;
            }
            else
            {
                mario.UPlayerPosition.Y = block.GetDestinationRectangle().Bottom + mario.GetDestinationRectangle().Height / 2 + 2;
                if (block is Block b)
                {
                    b.Bump();
                }
            }
        }

        private static void OpenMysteryBlock(MysteryBlock mystery, List<IItem> items, IBlock block)
        {
                mystery.OnCollide();

                Random random = new Random();
                Texture2D itemTexture = Game1.Instance.Content.Load<Texture2D>("smb_items_sheet");
                float xOffset = block.GetDestinationRectangle().Width / 2 - 16;
                Vector2 itemPosition = block.Position - new Vector2(-xOffset, block.GetDestinationRectangle().Height);

                // Define weights for each item type
                var weights = new[] { 50, 20, 20, 10 }; // Adjust weights as needed
                var totalWeight = weights.Sum();
                var randomValue = random.Next(0, totalWeight);

                // Determine the selected item type based on weights
                int cumulativeWeight = 0;
                ItemType selectedType = ItemType.Coin; // Default fallback

                for (int i = 0; i < weights.Length; i++)
                {
                    cumulativeWeight += weights[i];
                    if (randomValue < cumulativeWeight)
                    {
                        selectedType = (ItemType)i;
                        break;
                    }
                }

                // Create the item instance
                ItemBase newItem = ItemFactory.CreateInstance(selectedType, itemTexture, itemPosition);

                // Set item properties
                newItem.Velocity = new Vector2(0f, -1f) * 30f;
                newItem.GravityScale = 20.0f;
                newItem.bUseGravity = true;

                // Add the new item to the list
                items.Add(newItem);
        }

        private static void HandleSideCollision(PlayerSprite mario, IBlock block)
        {
            Rectangle blockRect = block.GetDestinationRectangle();
            Rectangle marioRect = mario.GetDestinationRectangle();

            if (mario.mode == PlayerSprite.Mode.Star && block.IsBreakable)
            {
                block.OnCollide(); // Break the block if Mario has a star
            }
            else if (marioRect.Right >= blockRect.Left && marioRect.Left < blockRect.Left)
            {
                mario.UPlayerPosition.X = blockRect.Left - marioRect.Width / 2;
            }
            else if (marioRect.Left <= blockRect.Right && marioRect.Right > blockRect.Right)
            {
                mario.UPlayerPosition.X = blockRect.Right + marioRect.Width / 2;
            }
        }

        private static void CheckIfFalling(PlayerSprite mario, List<IBlock> standingBlocks)
        {
            if (standingBlocks.Count == 0 && mario.isGrounded)
            {
                mario.isGrounded = false;
                mario.isFalling = true;
                mario.current = PlayerSprite.SpriteType.Falling;
            }
        }

        private static void RemoveBreakableBlocks(List<IBlock> blocks, List<IBlock> blocksToRemove, List<IItem> items)
        {
            foreach (IBlock blockToRemove in blocksToRemove)
            {
                if(blockToRemove is Block b && b.IsDestroyed)
                {
                    blocks.Remove(blockToRemove);
                }
                else if (blockToRemove is not Block)
                {
                    blocks.Remove(blockToRemove);
                }

                Texture2D brickFragmentTexture = Game1.Instance.Content.Load<Texture2D>("blocks");
                float xOffset = blockToRemove.GetDestinationRectangle().Width / 2 - 16;
                for (int i = 0; i < 4; ++i)
                {
                    float dir = i % 2 == 0 ? 1 : -1;
                    float yoffset = i < 2 ? 0 : -50;
                    var brickFragment = new BrickFragment(brickFragmentTexture, blockToRemove.Position - new Vector2(-xOffset, blockToRemove.GetDestinationRectangle().Height + yoffset));
                    brickFragment.Velocity = new Vector2(dir, -3.0f) * 30f;
                    brickFragment.GravityScale = 50.0f;
                    brickFragment.bUseGravity = true;
                    brickFragment.MaxLifeTime = 2000f;

                    items.Add(brickFragment);
                }
            }
        }

        private static void HandleFlagpoleCollision(Flagpole pole, PlayerSprite mario)
        {
            if (!pole.IsCollided)
            {
                pole.OnCollide();
                Rectangle flagpoleRect = pole.GetDestinationRectangle();
                int flagpoleHeight = flagpoleRect.Bottom - flagpoleRect.Top; 
                int marioPositionOnPole = flagpoleRect.Bottom - mario.GetDestinationRectangle().Center.Y;
                marioPositionOnPole = Math.Clamp(marioPositionOnPole, 0, flagpoleHeight);

                float scoreProportion = (float)marioPositionOnPole / flagpoleHeight;
                mario.SetScore((int)(scoreProportion * 5000));
                mario.UPlayerPosition.X += 20;
                pole.setMarioStartPosition(mario.UPlayerPosition);
                mario.isFalling = false;
                mario.isGrounded = true;
                mario.velocity = 0;
                return;
            }
            if (!pole.isFinished)
            {
                Vector2 marioPosition = pole.getMarioPosition();
                mario.setPosition((int)marioPosition.X, (int)marioPosition.Y);
            }
        }

    }
}
