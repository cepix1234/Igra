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
    public class CharacterMovement : Microsoft.Xna.Framework.GameComponent
    {
        DrawScripts.AnimSprite _body;
        DrawScripts.AnimSprite _legs;
        DrawScripts.AnimSprite _helthBar;
        DrawScripts.AnimSprite _crossHair;
        DrawScripts.AnimSprite _text;
        KeyboardState keyState;
        int frameCount;
        int oldState = 0;
        int helth;

        public CharacterMovement(Game game, DrawScripts.AnimSprite CharBody, DrawScripts.AnimSprite CharLegs, DrawScripts.AnimSprite helthBar, DrawScripts.AnimSprite crosshair, DrawScripts.AnimSprite text)
            : base(game)
        {
            // TODO: Construct any child components here
            _body = CharBody;
            _legs = CharLegs;
            _helthBar = helthBar;
            _crossHair = crosshair;
            _text = text;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            _body.currentAnim = 0;
            _legs.currentAnim = 0;

            //menjava strni izracunaj pozicijo nog
            int x = _body.animations[_body.currentAnim].Width / 2;
            x = x - _legs.animations[_body.currentAnim].Width / 2;
            _legs.position = _body.position + new Vector2(x * _legs.scale.X, (_body.animations[_body.currentAnim].Bottom - 1) * _legs.scale.Y);
            keyState = Keyboard.GetState();
            helth = 100;
            _helthBar.helth = helth;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Movement(gameTime);
            Crosshair();
            base.Update(gameTime);
        }

        public void Crosshair ()
        {
            MouseState _mouseState = Mouse.GetState();
            _crossHair.position = new Vector2(_mouseState.X, _mouseState.Y);

            double kot = Math.Atan2(_mouseState.Y - _body.position.Y, _mouseState.X - _body.position.X);
            if(kot <=-0.5 && kot >=-2.5)
            {
                _body.currentAnim = 1;
            }else if(kot <=-2.5 || kot >=2.5)
            {
                _body.currentAnim = 3;
            }else if(kot <=2.5 && kot >=0.5)
            {
                _body.currentAnim = 2;
            }else
            {
                _body.currentAnim = 0;
            }
            _body.center = new Vector2(_body.animations[_body.currentAnim].Width/2, _body.animations[_body.currentAnim].Height / 2);
        }

        public void Movement (GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            keyState = Keyboard.GetState();
            Boolean pressed = false;
            if (keyState.IsKeyDown(Keys.W))
            {
                oldState = 5;
                pressed = true;
                direction += new Vector2(0, -1);
                
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 4;

                switch (anim)
                {
                    case 0:
                        _legs.currentAnim = 5;
                        break;
                    case 1:
                        _legs.currentAnim = 1;
                        break;
                    case 2:
                        _legs.currentAnim = 5;
                        break;
                    case 3:
                        _legs.currentAnim = 2;
                        break;
                }

            }
            if (keyState.IsKeyDown(Keys.A))
            {
                oldState = 6;
                pressed = true;
                direction += new Vector2(-1, 0);
                _body.currentAnim = 3;
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 2;

                switch (anim)
                {
                    case 0:
                        _legs.currentAnim = 3;
                        break;
                    case 1:
                        _legs.currentAnim = 5;
                        break;
                }
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                oldState = 5;
                pressed = true;
                direction += new Vector2(0, 1);
                _body.currentAnim = 2;
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 4;

                switch (anim)
                {
                    case 0:
                        _legs.currentAnim = 5;
                        break;
                    case 1:
                        _legs.currentAnim = 1;
                        break;
                    case 2:
                        _legs.currentAnim = 5;
                        break;
                    case 3:
                        _legs.currentAnim = 2;
                        break;
                }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                oldState = 4;
                pressed = true;
                direction += new Vector2(1, 0);
                _body.currentAnim = 0;
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 2;

                switch (anim)
                {
                    case 0:
                        _legs.currentAnim = 0;
                        break;
                    case 1:
                        _legs.currentAnim = 4;
                        break;
                }
            }
            if (oldState == 5 && !pressed)
            {
                _legs.currentAnim = 5;
            }
            else if (oldState == 4 && !pressed)
            {
                _legs.currentAnim = 4;
            }

            _body.position += direction * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _legs.center = new Vector2(_legs.animations[_legs.currentAnim].Width / 2, 0);
            _legs.position = new Vector2(_body.position.X, _body.position.Y+_body.animations[_body.currentAnim].Height);
        }
    }
}