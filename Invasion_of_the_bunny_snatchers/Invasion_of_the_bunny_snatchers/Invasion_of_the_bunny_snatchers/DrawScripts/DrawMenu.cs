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


namespace Invasion_of_the_bunny_snatchers.DrawScripts
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DrawMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        public DrawMenu(Game game, List<DrawScripts.AnimSprite> UI)
            : base(game)
        {
            // TODO: Construct any child components here
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _UI = UI;
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
        #region Members
        public List<DrawScripts.AnimSprite> _UI;
        #endregion
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Begin();
            
            foreach (DrawScripts.AnimSprite _drawable in _UI)
            {
                if (_drawable.slika)
                {
                    _spriteBatch.Draw(_drawable.texture, _drawable.position, _drawable.animations[_drawable.currentAnim], Color.White, 0f, _drawable.center, _drawable.scale, SpriteEffects.None, 0f);
                }
                else
                {
                    _spriteBatch.DrawString(_drawable.font, _drawable.text.ToString(), _drawable.position, Color.Black, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
            }

            _spriteBatch.End();
        }
    }
}
