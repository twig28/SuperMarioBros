using MarioGame.Collisions;
using MarioGame.Controllers;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioGame
{
    internal class GameHelper
    {
        public static void checkAllCollisions(List<IEnemy> enemies, List<IBlock> blocks, List<IItem> items, PlayerSprite player_sprite, GameTime gameTime, int height, bool dead)
        {
            EnemyCollisionLogic.CheckEnemyBlockCollisions(enemies, blocks, gameTime, player_sprite);
            if (!dead)
            {
                MarioBlockCollisionLogic.CheckMarioBlockCollision(player_sprite, blocks, items);
                MarioEnemyCollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
                PositionChecks.checkDeathByFalling(player_sprite, height);
            }
            EnemyCollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime, player_sprite);
            ItemCollisionLogic.CheckMarioItemCollision(player_sprite, items, gameTime);
            ItemCollisionLogic.CheckItemBlockCollision(blocks, items);
            blocks.RemoveAll(block => block is Block b && b.IsDestroyed);
        }

        public static void updateAll(List<IEnemy> enemies, List<IBlock> blocks, List<IItem> items, PlayerSprite player_sprite, GameTime gameTime, GraphicsDevice g, SoundLib soundLib, IController keyControl, float ballSpeed)
        {
            player_sprite.Update(gameTime, player_sprite);

            foreach (var block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IItem item in items)
            {
                item.Update(gameTime);
            }

            items.RemoveAll(item => item.GetLifeTime() <= 0.0f);

            Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl, soundLib);
            Ball.UpdateAll(gameTime, g.Viewport.Width, blocks);
            BallCollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime, false);
            DropFireball.UpdateAll(gameTime, blocks);


        }

        public static void drawAll(List<IEnemy> enemies, List<IBlock> blocks, List<IItem> items, List<IScenery> scenery, PlayerSprite player_sprite, SpriteBatch _spriteBatch, Vector2 offset, GraphicsDevice g, GameTime gameTime)
        {

            foreach (IEnemy enemy in enemies)
            {
                if (PositionChecks.renderEnemy(enemy, offset, g.Viewport.Width, g.Viewport.Height) || enemy is KoopaShell)
                {
                    enemy.Update(gameTime);
                    if (enemy is Bowser b) { b.detectMarioChange(player_sprite.GetDestinationRectangle()); }
                }
            }

            foreach (IScenery scene in scenery)
            {
                scene.Draw(_spriteBatch);
            }

            foreach (IEnemy enemy in enemies)
            {
                if (enemy is Piranha)
                    enemy.Draw();
            }

            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            foreach (IItem item in items)
            {
                item.Draw(_spriteBatch);
            }

            foreach (IEnemy enemy in enemies)
            {
                if (enemy is not Piranha)
                    enemy.Draw();
            }

            Ball.DrawAll(_spriteBatch);
            DropFireball.DrawAll(_spriteBatch);

            player_sprite.Draw(_spriteBatch, 14, 16, 3f, new List<Rectangle>(), 0, Color.White);

            _spriteBatch.End();
        }
    }
}
