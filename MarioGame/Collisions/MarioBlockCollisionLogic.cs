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

namespace MarioGame.Collisions
{
    internal class MarioBlockCollisionLogic
    {
        public static void CheckMarioBlockCollision(PlayerSprite mario, List<IBlock> blocks, List<IItem> items)
        {
            if (mario.current == PlayerSprite.SpriteType.Damaged) return;

            List<IBlock> blocksToRemove = new List<IBlock>();
            List<IBlock> standingBlocks = new List<IBlock>();

            foreach (IBlock block in blocks)
            {
                Rectangle blockRect = block.GetDestinationRectangle();
                Rectangle marioRect = mario.GetDestinationRectangle();
                if (block is Flagpole)
                {
                    //TODO to be implemented, move mario down to ground and add score
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
                else if (block is Pipe p && (p.getIsEntrance() && CollisionLogic.GetCollisionDirection(blockRect, marioRect) == CollisionLogic.CollisionDirection.Above && mario.crouched ||
                    p.getIsLong() && CollisionLogic.GetCollisionDirection(blockRect, marioRect) == CollisionLogic.CollisionDirection.Side))
                {
                    (Vector2 destination, int level) = p.GetDestination();
                    Game1.Instance.SetLevel(level);
                    mario.setPosition((int)destination.X, (int)destination.Y);
                    continue;
                }

                if (marioRect.Intersects(blockRect))
                {
                    if (IsStandingOnBlock(mario, blockRect))
                    {
                        HandleStandingOnBlock(mario, block, standingBlocks);
                    }
                    else if (IsBelowBlock(mario, blockRect))
                    {
                        HandleBelowBlockCollision(mario, block, items, blocksToRemove);
                    }
                    else
                    {
                        HandleSideCollision(mario, block);
                    }
                }
                else
                {
                    standingBlocks.Remove(block);
                }
            }

            CheckIfFalling(mario, standingBlocks);
            RemoveBreakableBlocks(blocks, blocksToRemove, items);
        }

        private static bool IsStandingOnBlock(PlayerSprite mario, Rectangle blockRect)
        {
            return mario.UPlayerPosition.Y < blockRect.Top;
        }

        private static bool IsBelowBlock(PlayerSprite mario, Rectangle blockRect)
        {
            return mario.UPlayerPosition.Y > blockRect.Bottom &&
                   !mario.isGrounded &&
                   mario.UPlayerPosition.X < blockRect.Right &&
                   mario.UPlayerPosition.X > blockRect.Left;
        }

        private static void HandleStandingOnBlock(PlayerSprite mario, IBlock block, List<IBlock> standingBlocks)
        {
            standingBlocks.Add(block);

            if (!mario.isGrounded)
            {
                mario.isGrounded = true;
                mario.isJumping = false;
                mario.velocity = 0f;
                mario.current = mario.left ? PlayerSprite.SpriteType.StaticL : PlayerSprite.SpriteType.Static;
            }

            AdjustMarioYPosition(mario, block.GetDestinationRectangle().Top);
        }

        private static void AdjustMarioYPosition(PlayerSprite mario, int blockTop)
        {
            if (mario.Big || mario.Fire || mario.Star)
            {
                mario.UPlayerPosition.Y = blockTop - mario.GetDestinationRectangle().Height / 2 + 26;
            }
            else
            {
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

            if (mario.Big || mario.Fire || mario.Star)
            {
                if (block.IsBreakable)
                {
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

            // Example Coin creation (may need refactoring to use a CoinFactory pattern)
            Texture2D coinTexture = Game1.Instance.Content.Load<Texture2D>("smb_items_sheet");
            float xOffset = block.GetDestinationRectangle().Width / 2 - 16;
            var coin = new Coin(coinTexture, block.Position - new Vector2(-xOffset, block.GetDestinationRectangle().Height));
            coin.Velocity = new Vector2(0f, -3f) * 30f;
            coin.GravityScale = 15.0f;
            coin.EnableGravity = true;

            items.Add(coin);
        }

        private static void HandleSideCollision(PlayerSprite mario, IBlock block)
        {
            Rectangle blockRect = block.GetDestinationRectangle();
            Rectangle marioRect = mario.GetDestinationRectangle();

            if (mario.Star && block.IsBreakable)
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
                blocks.Remove(blockToRemove);

                Texture2D brickFragmentTexture = Game1.Instance.Content.Load<Texture2D>("blocks");
                float xOffset = blockToRemove.GetDestinationRectangle().Width / 2 - 16;
                for (int i = 0; i < 4; ++i)
                {
                    float dir = i % 2 == 0 ? 1 : -1;
                    float yoffset = i < 2 ? 0 : -50;
                    var brickFragment = new BrickFragment(brickFragmentTexture, blockToRemove.Position - new Vector2(-xOffset, blockToRemove.GetDestinationRectangle().Height + yoffset));
                    brickFragment.Velocity = new Vector2(dir, -3.0f) * 30f;
                    brickFragment.GravityScale = 50.0f;
                    brickFragment.EnableGravity = true;
                    brickFragment.MaxLifeTime = 2f;

                    items.Add(brickFragment);
                }
            }
        }

    }
}
