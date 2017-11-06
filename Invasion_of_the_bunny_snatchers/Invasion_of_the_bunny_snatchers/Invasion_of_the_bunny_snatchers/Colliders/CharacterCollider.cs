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
    public class CharacterCollider : Microsoft.Xna.Framework.GameComponent
    {
        DrawScripts.AnimSprite _aspCharacter;
        DrawScripts.AnimSprite _aspLegs;
        public CharacterCollider(Game game, DrawScripts.AnimSprite cahraceter, DrawScripts.AnimSprite legs)
            : base(game)
        {
            // TODO: Construct any child components here
            _aspCharacter = cahraceter;
            _aspLegs = legs;
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
            BorderCollision();
            base.Update(gameTime);
        }

        private void BorderCollision ()
        {
            if(_aspCharacter.position.X+(13*_aspCharacter.scale.X) >= Game.GraphicsDevice.Viewport.Width)
            {
                _aspCharacter.position =new Vector2((int)Game.GraphicsDevice.Viewport.Width - (13 * _aspCharacter.scale.X), _aspCharacter.position.Y);
            }
            if(_aspCharacter.position.X- (11 * _aspCharacter.scale.X) <= 0)
            {
                _aspCharacter.position = new Vector2((11 * _aspCharacter.scale.X), _aspCharacter.position.Y);
            }
            if (_aspCharacter.position.Y - (20 * _aspCharacter.scale.Y) <= 0)
            {
                _aspCharacter.position = new Vector2(_aspCharacter.position.X, (20 * _aspCharacter.scale.Y));
            }
            if (_aspLegs.position.Y + (20 * _aspLegs.scale.Y) >= Game.GraphicsDevice.Viewport.Height)
            {
                _aspCharacter.position = new Vector2(_aspCharacter.position.X,(int)Game.GraphicsDevice.Viewport.Height - (_aspLegs.texture.Height-22*_aspLegs.scale.Y));
            }
        }
    }
}
