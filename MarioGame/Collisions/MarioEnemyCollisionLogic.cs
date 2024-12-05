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
                    if (toDie)
                    {
                        HandleMarioDamage(mario);
                        toDie = false;
                    }
                }
                
            }

            if (enemyToRemove != null) { enemies.Remove(enemyToRemove); }
            if (enemyToAdd != null) { enemies.Add(enemyToAdd); }
        }

        private static void HandleBelowCollision(PlayerSprite mario, IEnemy enemy, GameTime gt, ref IEnemy enemyToAdd)
        {
            if (enemy is Piranha || enemy is Bowser)
            {
                HandleMarioDamage(mario);
            }
            else if (enemy is Koopa koopa && koopa.getdeathStartTime <= 0)
            {
                koopa.TriggerDeath(gt, true);
                enemyToAdd = koopa.SpawnKoopa(gt);
                mario.velocity = -10f;
                mario.SetScore(100);
            }
            else if (enemy is KoopaShell shell && !shell.getIsMoving())
            {
                StartKoopaShell(shell, mario);
            }
            else if (enemy.getdeathStartTime <= 0)
            {
                mario.velocity = -10f;

                if (enemy is not KoopaShell)
                {
                    enemy.TriggerDeath(gt, true);
                    mario.SetScore(100);
                }
            }
        }

        private static void HandleSideOrAboveCollision(PlayerSprite mario, IEnemy enemy, GameTime gt, ref bool toDie)
        {
            if (mario.mode == PlayerSprite.Mode.Star)
            {
                enemy.TriggerDeath(gt, false);
            }
            else if (enemy is KoopaShell shell && !shell.getIsMoving())
            {
                StartKoopaShell(shell, mario);
            }
            else if (enemy is KoopaShell s && s.getIsMoving() && (s.DefaultMoveMentDirection && IsLeft(mario.GetDestinationRectangle(), s.GetDestinationRectangle()) || !s.DefaultMoveMentDirection && !IsLeft(mario.GetDestinationRectangle(), s.GetDestinationRectangle()))){
                //nothing for now if moving right and mario is left or moving left and mario is right bug fix
            }
            else
            {
                toDie = true;
            }
        }

        public static bool IsLeft(Rectangle a, Rectangle b)
        {
            if (a.Right <= b.Right)
            {
                return true; // `a` is to the left of `b`
            }
            return false;
        }

        private static void HandleMarioDamage(PlayerSprite mario)
        {
            if (mario.mode == PlayerSprite.Mode.Big || mario.mode == PlayerSprite.Mode.Fire)
            {
                mario.mode = PlayerSprite.Mode.invincible;
            }
            else if(mario.mode != PlayerSprite.Mode.invincible && mario.getCoinScoreLives()[2] >= 1)
            {
                mario.SetLives(1);
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
