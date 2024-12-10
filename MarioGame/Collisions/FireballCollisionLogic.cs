using MarioGame.Blocks;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioGame.Collisions
{
    internal class FireballCollisionLogic
    {
        public static void CheckFireballCollisions(List<IEnemy> enemies, List<IBlock> blocks, PlayerSprite mario)
        {
            foreach (var enemy in enemies)
            {
                if (enemy is Bowser bowser)
                {
                    var fireballs = bowser.GetFireballs(); 

                    foreach (var fireball in fireballs)
                    {
                        if (!fireball.Alive) continue;

                        
                        foreach (var block in blocks)
                        {
                            if (fireball.GetDestinationRectangle().Intersects(block.GetDestinationRectangle()))
                            {
                                fireball.Destroy(); 
                                break; 
                            }
                        }

                 
                        if (fireball.Alive && fireball.GetDestinationRectangle().Intersects(mario.GetDestinationRectangle()))
                        {
                            fireball.Destroy();
                            mario.current = PlayerSprite.SpriteType.Damaged; 
                        }
                    }

                  
                    fireballs.RemoveAll(f => !f.Alive);
                }
            }
        }
    }
}
