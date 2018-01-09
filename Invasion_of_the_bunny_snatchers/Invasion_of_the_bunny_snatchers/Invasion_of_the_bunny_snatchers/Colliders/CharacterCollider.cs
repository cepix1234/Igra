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
        Player.Player _aspCharacter;
        Player.Player _aspLegs;
        public List<Enemy.enemy> _enemys;
        public List<DrawScripts.AnimSprite> _powerUps;
        public CharacterCollider(Game game)
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
            _aspCharacter = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._player;
            _aspLegs = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._player;
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            _powerUps = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._powerUps;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Game.Components.OfType<DrawScripts.DrawInOrder>().ToList().Count != 0)
            {
                _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
                _powerUps = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._powerUps;
            }
            BorderCollision();
            colideWithPowerup();
            colideWithEnemy(gameTime);
            base.Update(gameTime);
        }

        private void colideWithEnemy(GameTime gameTime)
        {
            foreach (Enemy.enemy enemy in _enemys)
            {
                if (enemy.attack)
                {
                    //collide with enemy move back and take X damage
                    enemy.attack = false;
                    _aspCharacter.helth -= 10;
                    Vector2 direction = enemy.position - _aspCharacter.position;
                    _aspCharacter.position -= direction *10*(float)gameTime.ElapsedGameTime.TotalSeconds;
                } 
            }
        }

        private void colideWithPowerup()
        {
            int i = 0;
            bool pickedUp = false;
            foreach (DrawScripts.AnimSprite powerUp in _powerUps)
            {
                if (_aspCharacter.position.X + (13 * _aspCharacter.body.scale.X) >= powerUp.position.X)
                {
                    if (_aspCharacter.position.X - (11 * _aspCharacter.body.scale.X) <= powerUp.position.X + 27 * powerUp.scale.X)
                    {
                        if (_aspCharacter.position.Y - (20 * _aspCharacter.body.scale.Y) <= powerUp.position.Y)
                        {
                            if (_aspLegs.position.Y + (20 * _aspLegs.legs.scale.Y) >= powerUp.position.Y + 28 * powerUp.scale.Y)
                            {
                                pickedUp = true;
                                switch (powerUp.currentAnim)
                                {
                                    case 0:
                                        // pick up multy shot
                                        _aspCharacter.multipleBoolets = true;
                                        _aspCharacter.speed = 100;
                                        _aspCharacter.shootSpeed = 1f;
                                        break;

                                    case 1:
                                        //pick up shoot speed
                                        _aspCharacter.multipleBoolets = false;
                                        _aspCharacter.speed = 100;
                                        _aspCharacter.shootSpeed = 0.5f;
                                        break;

                                    case 2:
                                        //pick up move seped
                                        _aspCharacter.multipleBoolets = false;
                                        _aspCharacter.speed = 200;
                                        _aspCharacter.shootSpeed = 1f;
                                        break;
                                }
                                break;
                            }
                        }
                    }
                }
                i++;
            }
            if(pickedUp)
            {
                _powerUps.RemoveAt(i);
            }
        }


        private void BorderCollision ()
        {
            if(_aspCharacter.position.X+(13*_aspCharacter.body.scale.X) >= Game.GraphicsDevice.Viewport.Width)
            {
                _aspCharacter.position =new Vector2((int)Game.GraphicsDevice.Viewport.Width - (13 * _aspCharacter.body.scale.X), _aspCharacter.position.Y);
            }
            if(_aspCharacter.position.X- (11 * _aspCharacter.body.scale.X) <= 0)
            {
                _aspCharacter.position = new Vector2((11 * _aspCharacter.body.scale.X), _aspCharacter.position.Y);
            }
            if (_aspCharacter.position.Y - (20 * _aspCharacter.body.scale.Y) <= 0)
            {
                _aspCharacter.position = new Vector2(_aspCharacter.position.X, (20 * _aspCharacter.body.scale.Y));
            }
            if (_aspLegs.position.Y + (20 * _aspLegs.legs.scale.Y) >= Game.GraphicsDevice.Viewport.Height)
            {
                _aspCharacter.position = new Vector2(_aspCharacter.position.X,(int)Game.GraphicsDevice.Viewport.Height - (_aspLegs.legs.texture.Height-22*_aspLegs.legs.scale.Y));
            }
        }
    }
}
