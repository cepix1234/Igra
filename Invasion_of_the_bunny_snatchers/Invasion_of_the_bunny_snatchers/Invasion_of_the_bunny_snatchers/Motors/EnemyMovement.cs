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
        DrawScripts.AnimSprite _enemy;
        int _enemyType;
        int frameCount;

        public EnemyMovement(Game game, DrawScripts.AnimSprite enemy, int enemyType)
            : base(game)
        {
            // TODO: Construct any child components here
            _enemy = enemy;
            _enemyType = enemyType;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            _enemy.currentAnim = _enemyType;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            frameCount++;
            int frameCountD = frameCount / 15;
            int anim = frameCountD % 2;

            switch (anim)
            {
                case 0:
                    _enemy.currentAnim = 1;
                    break;
                case 1:
                    _enemy.currentAnim = 0;
                    break;
            }

            base.Update(gameTime);
        }
    }
}
