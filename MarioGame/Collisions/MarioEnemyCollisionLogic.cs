using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static MarioGame.CollisionLogic;

namespace MarioGame.Collisions
{
    internal class MarioEnemyCollisionLogic
    {
        public static void CheckMarioEnemyCollision(PlayerSprite mario, ref List<IEnemy> enemies, GameTime gt)
        {
            bool toDie = false;
            IEnemy enemyToRemove = null;
            IEnemy enemyToAdd = null;

            foreach (IEnemy enemy in enemies)
            {
                if (!enemy.Alive)
                {
                    enemyToRemove = enemy;
                    continue;
                }

                CollisionDirection collisionDirection = GetCollisionDirection(mario.GetDestinationRectangle(), enemy.GetDestinationRectangle());

                if (collisionDirection == CollisionDirection.Below)
                {
                    HandleBelowCollision(mario, enemy, gt, ref enemyToAdd);
                }
                else if (collisionDirection != CollisionDirection.None && enemy.getdeathStartTime <= 0)
                {
                    HandleSideOrAboveCollision(mario, enemy, gt, ref toDie);
                }

                if (toDie)
                {
                    if (mario.Big || mario.Fire)
                    {
                        mario.Big = false;
                        mario.Fire = false;
                        mario.invincible = true;
                    }
                    else if (!mario.invincible)
                    {
                        mario.current = PlayerSprite.SpriteType.Damaged;
                    }
                    toDie = false;
                }
            }

            if (enemyToRemove != null) { enemies.Remove(enemyToRemove); }
            if (enemyToAdd != null) { enemies.Add(enemyToAdd); }
        }

        private static void HandleBelowCollision(PlayerSprite mario, IEnemy enemy, GameTime gt, ref IEnemy enemyToAdd)
        {
            if (enemy is Piranha)
            {
                mario.velocity = 0f;
                mario.current = PlayerSprite.SpriteType.Damaged;
            }
            else if (enemy is Koopa koopa && koopa.getdeathStartTime <= 0)
            {
                koopa.TriggerDeath(gt, true);
                enemyToAdd = koopa.SpawnKoopa(gt);
                mario.velocity = -10f;
            }
            else if (enemy is KoopaShell shell && !shell.getIsMoving())
            {
                StartKoopaShell(shell, mario);
            }
            else if (enemy.getdeathStartTime <= 0)
            {
                mario.velocity = -10f;
                if (enemy is not KoopaShell) enemy.TriggerDeath(gt, true);
            }
        }

        private static void HandleSideOrAboveCollision(PlayerSprite mario, IEnemy enemy, GameTime gt, ref bool toDie)
        {
            if (mario.Star)
            {
                enemy.TriggerDeath(gt, false);
            }
            else if (enemy is KoopaShell shell && !shell.getIsMoving())
            {
                StartKoopaShell(shell, mario);
            }
            else
            {
                toDie = true;
            }
        }

        private static void HandleMarioDamage(PlayerSprite mario)
        {
            if (mario.Big || mario.Fire)
            {
                mario.Big = false;
                mario.Fire = false;
                mario.invincible = true;
            }
            else if (!mario.invincible)
            {
                mario.current = PlayerSprite.SpriteType.Damaged;
            }
        }

        private static void StartKoopaShell(KoopaShell shell, PlayerSprite mario)
        {
            if (mario.current == PlayerSprite.SpriteType.Motion || mario.current == PlayerSprite.SpriteType.Jump)
            {
                shell.Start(true);
            }
            else if (mario.current == PlayerSprite.SpriteType.MotionL || mario.current == PlayerSprite.SpriteType.JumpL)
            {
                shell.Start(false);
            }
        }

    }
}
