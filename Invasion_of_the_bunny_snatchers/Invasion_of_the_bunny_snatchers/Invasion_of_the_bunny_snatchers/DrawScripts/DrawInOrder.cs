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
    public class DrawInOrder : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch _spriteBatch;
        public DrawInOrder(Game game, List<DrawScripts.AnimSprite> drawabels)
            : base(game)
        {
            // TODO: Construct any child components here
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _drawabels = drawabels;
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
        private List<DrawScripts.AnimSprite> _drawabels;
        #endregion

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Begin();
            foreach (DrawScripts.AnimSprite _drawable in _drawabels)
            {
                if(_drawable.slika)
                {
                    _spriteBatch.Draw(_drawable.texture, _drawable.position, _drawable.animations[_drawable.currentAnim], Color.White, 0f, _drawable.center, _drawable.scale, SpriteEffects.None, 0f);
                }
                else if(_drawable.helthBar)
                {
                    Texture2D rec = new Texture2D(GraphicsDevice, _drawable.helth, 25);
                    Color[] data = new Color[_drawable.helth * 25];
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
                    rec.SetData(data);
                    
                    _spriteBatch.Draw(rec, Vector2.Zero, Color.Red);
                    _spriteBatch.DrawString(_drawable.font, _drawable.helth.ToString() + "%", new Vector2(10, 0), Color.Black, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }else
                {
                    _spriteBatch.DrawString(_drawable.font, _drawable.text.ToString(), _drawable.position, Color.Black, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                }
            }
            _spriteBatch.End();
        }

    }
}