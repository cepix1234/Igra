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
        public BulletMovement(Game game, DrawScripts.DrawInOrder draw)
            : base(game)
        {
            // TODO: Construct any child components here
            _draw = draw;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            _bullets = new List<DrawScripts.AnimSprite>();
            base.Initialize();
        }
        #region Members
        public List<DrawScripts.AnimSprite> _bullets;
        #endregion

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            int i = 0;
            List<int> iji = new List<int>();
            foreach(DrawScripts.AnimSprite bullet in _bullets)
            {
                bullet.position += bullet.direction *100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                _bullets.RemoveAt(ij);
                _draw._bullets.RemoveAt(ij);
            }
            base.Update(gameTime);
        }
    }
}
