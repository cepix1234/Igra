using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Invasion_of_the_bunny_snatchers.Motors
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EnemyMovement : Microsoft.Xna.Framework.GameComponent
    {
        List <DrawScripts.AnimSprite> _enemys;
        Colliders.ColliderBulletscs _bulletcolider;
        public EnemyMovement(Game game, List <DrawScripts.AnimSprite> enemys,Colliders.ColliderBulletscs bulletcol)
            : base(game)
        {
            // TODO: Construct any child components here
            _enemys = enemys;
            _bulletcolider = bulletcol;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            
            base.Update(gameTime);
        }

        private void EnemyAI()
        {
            foreach (DrawScripts.AnimSprite enemy in _enemys)
            {
                //movement for enemy based on type
                if(enemy.enemyType == 0)
                {

                }else if(enemy.enemyType == 1)
                {

                }



                if (!enemy.powerup)
                {
                    enemy.frameEnemy++;
                    int frameCountD = enemy.frameEnemy / 15;
                    int anim = frameCountD % 2;

                    switch (anim)
                    {
                        case 0:
                            enemy.currentAnim = 1;
                            break;

                        case 1:
                            enemy.currentAnim = 0;
                            break;
                    }
                }
            }
        }
    }
}
