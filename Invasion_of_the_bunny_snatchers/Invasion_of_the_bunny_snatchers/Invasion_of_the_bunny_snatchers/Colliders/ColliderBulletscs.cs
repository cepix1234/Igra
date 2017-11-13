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


namespace Invasion_of_the_bunny_snatchers.Colliders
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ColliderBulletscs : Microsoft.Xna.Framework.GameComponent
    {
        public List<DrawScripts.AnimSprite> _bullets;
        public List<DrawScripts.AnimSprite> _enemys;
        public DrawScripts.DrawInOrder _draw;
        Motors.BulletMovement motor;
        public ColliderBulletscs(Game game, List<DrawScripts.AnimSprite> bullets, List<DrawScripts.AnimSprite> enemys, DrawScripts.DrawInOrder draw)
            : base(game)
        {
            // TODO: Construct any child components here
            _bullets = bullets;
            _enemys = enemys;
            _draw = draw;
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

            bullethitrabit();
            base.Update(gameTime);
        }       


        private void bullethitrabit()
        {
            motor = Game.Components.OfType<Motors.BulletMovement>().ToList()[0];//za pomoc pri ostalih skritptah lahko dobim component
            foreach (DrawScripts.AnimSprite bullet in _bullets)
            {
                foreach (DrawScripts.AnimSprite enemy in _enemys)
                {
                    if (!enemy.powerup)
                    {
                        if (enemy.position.X + 15 * enemy.scale.X >= bullet.position.X && enemy.position.Y + 15 * enemy.scale.Y >= bullet.position.Y && enemy.position.X - 15 * enemy.scale.X <= bullet.position.X && enemy.position.Y - 15 * enemy.scale.Y <= bullet.position.Y)
                        {
                            // kill enemy and bullet check if spawn power up
                        }
                    }
                }
            }
        }
    }
}
