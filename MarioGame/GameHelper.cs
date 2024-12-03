using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Items;
using MarioGame.Blocks;
using MarioGame.Sprites;
using MarioGame.Collisions;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Controllers;

namespace MarioGame
{
    internal class GameHelper
    {
        public static void checkAllCollisions(List<IEnemy> enemies, List<IBlock> blocks, List<IItem> items, PlayerSprite player_sprite, GameTime gameTime, int height)
        {
            EnemyCollisionLogic.CheckEnemyBlockCollisions(enemies, blocks, gameTime, player_sprite);
            MarioBlockCollisionLogic.CheckMarioBlockCollision(player_sprite, blocks, items);
            EnemyCollisionLogic.CheckEnemyEnemyCollision(enemies, gameTime, player_sprite);
            MarioEnemyCollisionLogic.CheckMarioEnemyCollision(player_sprite, ref enemies, gameTime);
            CollisionLogic.CheckMarioItemCollision(player_sprite, items, gameTime);
            CollisionLogic.CheckItemBlockCollision(blocks, items);
            PositionChecks.checkDeathByFalling(player_sprite, height);
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

            items.RemoveAll(item => item.GetLifeTime() < 0.0f);

            Ball.CreateFireballs(player_sprite.UPlayerPosition, ballSpeed, (KeyboardController)keyControl, soundLib);
            Ball.UpdateAll(gameTime, g.Viewport.Width, blocks);
            BallCollisionLogic.CheckFireballEnemyCollision(Ball.GetBalls(), ref enemies, gameTime, false);
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

            player_sprite.Draw(_spriteBatch, 14, 16, 3f, new List<Rectangle>(), 0, Color.White);

            _spriteBatch.End();
        }
    }
}
