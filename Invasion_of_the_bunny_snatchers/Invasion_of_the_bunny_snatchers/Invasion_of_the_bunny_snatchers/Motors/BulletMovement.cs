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
    public class BulletMovement : Microsoft.Xna.Framework.GameComponent
    {
        DrawScripts.DrawInOrder _draw;
        Colliders.ColliderBulletscs _bulletcollider;
        List<Bullet.Bullet> _bullets;
        public BulletMovement(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            _draw = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0];
            _bulletcollider = Game.Components.OfType<Colliders.ColliderBulletscs>().ToList()[0];
            _bullets = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._bullets;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if(Game.Components.OfType<DrawScripts.DrawInOrder>().ToList().Count != 0)
            {
                _bullets = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._bullets;
            }
            int i = 0;
            List<int> iji = new List<int>();
            foreach(Bullet.Bullet bullet in _bullets)
            {
                bullet.position += bullet.direction *200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(bullet.position.X < 0)
                {
                    iji.Add(i);
                }
                if (bullet.position.Y < 0)
                {
                    iji.Add(i);
                }
                if (bullet.position.X > Game.GraphicsDevice.Viewport.Width)
                {
                    iji.Add(i);
                }
                if (bullet.position.Y > Game.GraphicsDevice.Viewport.Height)
                {
                    iji.Add(i);
                }
                i++;
            }
            foreach (int ij in iji)
            {
                if(ij < _bullets.Count)
                _bullets.RemoveAt(ij);
            }
            base.Update(gameTime);
        }
    }
}
