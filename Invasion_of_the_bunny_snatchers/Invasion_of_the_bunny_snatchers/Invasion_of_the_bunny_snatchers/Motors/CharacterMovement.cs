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
        Player.Player _player;
        DrawScripts.AnimSprite _helthBar;
        DrawScripts.AnimSprite _crossHair;
        Bullet.Bullet _bullet;
        DrawScripts.DrawInOrder _draw;
        Motors.BulletMovement _bulletsMotor;
        Colliders.ColliderBulletscs _collider;
        KeyboardState keyState;
        int frameCount;
        int oldState = 0;
        float lastFire = 0;

        public CharacterMovement(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            _player = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._player;
            _helthBar = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._UI[1];
            _crossHair = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._UI[0];
            _bullet = Game.Components.OfType<Bullet.Bullet>().ToList()[0];
            _draw = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0];
            _collider = Game.Components.OfType<Colliders.ColliderBulletscs>().ToList()[0];
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            _player.body.currentAnim = 0;
            _player.legs.currentAnim = 0;

            //menjava strni izracunaj pozicijo nog
            int x = _player.body.animations[_player.body.currentAnim].Width / 2;
            x = x - _player.legs.animations[_player.body.currentAnim].Width / 2;
            keyState = Keyboard.GetState();
            _collider._bullets = new List<Bullet.Bullet>();
            _bulletsMotor = new Motors.BulletMovement(Game);
            Game.Components.Add(_bulletsMotor);
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
            //shoot 60rpm
            lastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lastFire> _player.shootSpeed)
            {
                MouseState _mouseState = Mouse.GetState();
                if (_player.multipleBoolets)
                {
                    multyShot(_mouseState);       
                }
                else
                {
                    shoot(_mouseState);
                }
               lastFire = (float)gameTime.ElapsedGameTime.TotalSeconds;
               lastFire = 0;
            }
            CheckHelth();
            base.Update(gameTime);
        }

        void CheckHelth ()
        {
            if(_player.helth <= 0)
            {
                Manger.WaveManger waveManager = this.Game.Components.OfType<Manger.WaveManger>().ToArray()[0];
                int waves = waveManager.getCurrentWave();
                this.Game.Components.Clear();
                Scenes.EndScreen end = new Scenes.EndScreen(this.Game);
                end.Waves = waves+1;
                this.Game.Components.Add(end);
            }
        }


        public void multyShot(MouseState _mouseState)
        {
            for(int i = -1;i<2;i++)
            {
                Vector2 position = _player.position;
                Vector2 cilj = _crossHair.position;
                switch (_player.body.currentAnim)
                {
                    case 0:
                        position = new Vector2(position.X + 16 * 2, position.Y);
                        cilj = new Vector2(_crossHair.position.X, _crossHair.position.Y + (50 * i));
                        break;

                    case 1:
                        position = new Vector2(position.X, position.Y - 5 * 2);
                        cilj = new Vector2(_crossHair.position.X + (50 * i), _crossHair.position.Y);
                        break;

                    case 2:
                        position = new Vector2(position.X, position.Y + 10 * 2);
                        cilj = new Vector2(_crossHair.position.X + (50 * i), _crossHair.position.Y);
                        break;

                    case 3:
                        position = new Vector2(position.X - 13 * 2, position.Y);
                        cilj = new Vector2(_crossHair.position.X, _crossHair.position.Y + (50 * i));
                        break;
                }

                Vector2 direction = cilj - _player.position;
                float distance = direction.Length();
                direction /= distance;
                direction.Normalize();
                double kot = Math.Atan2(cilj.Y - _player.position.Y, cilj.X - _player.position.X);
                
                shootOneBullet(position, direction, (float)kot);
            }
            float volume = 0.4f;
            float pitch = 0.0f;
            float pan = 0.0f;
            Random rnd = new Random();
            int rand = rnd.Next(0, 2);
            if (rand == 0)
            {
                _player.shoot1.Play(volume, pitch, pan);
            }
            else
            {
                _player.shoot2.Play(volume, pitch, pan);
            }

        }

        public void shoot(MouseState _mouseState)
        {
            Vector2 direction = _crossHair.position- _player.position;
            float distance = direction.Length();
            direction /= distance;
            direction.Normalize();
            double kot = Math.Atan2(_mouseState.Y - _player.position.Y, _mouseState.X - _player.position.X);
            Vector2 position = _player.position;
            switch(_player.body.currentAnim)
            {
                case 0:
                    position = new Vector2(position.X+16*2, position.Y);
                    break;

                case 1:
                    position = new Vector2(position.X, position.Y-5*2);
                    break;

                case 2:
                    position = new Vector2(position.X,position.Y+10*2);
                    break;

                case 3:
                    position = new Vector2(position.X - 13 * 2, position.Y);
                    break;
            }
            shootOneBullet(position,direction,(float)kot);
            float volume = 0.4f;
            float pitch = 0.0f;
            float pan = 0.0f;
            Random rnd = new Random();
            int rand = rnd.Next(0, 2);
            if(rand == 0)
            {
                _player.shoot1.Play(volume, pitch, pan);
            }
            else
            {
                _player.shoot2.Play(volume, pitch, pan);
            }
        }

        public void shootOneBullet(Vector2 position, Vector2 direction, float kot)
        {
            Bullet.Bullet bulet = new Bullet.Bullet(Game);
            bulet.body = _bullet.body;
            bulet.position = position;
            bulet.direction = direction;
            bulet.rotation = kot;
            bulet.player = true;
            Game.Components.Add(bulet);
            _draw._bullets.Add(bulet);
        }

        public void Crosshair ()
        {
            MouseState _mouseState = Mouse.GetState();
            _crossHair.position = new Vector2(_mouseState.X, _mouseState.Y);

            double kot = Math.Atan2(_mouseState.Y - _player.position.Y, _mouseState.X - _player.position.X);
            if (kot <= -0.5 && kot >= -2.5)
            {
                _player.body.currentAnim = 1;
            }
            else if (kot <= -2.5 || kot >= 2.5)
            {
                _player.body.currentAnim = 3;
            }
            else if (kot <= 2.5 && kot >= 0.5)
            {
                _player.body.currentAnim = 2;
            }
            else
            {
                _player.body.currentAnim = 0;
            }
            _player.body.center = new Vector2(_player.body.animations[_player.body.currentAnim].Width / 2, _player.body.animations[_player.body.currentAnim].Height / 2);
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
                        _player.legs.currentAnim = 5;
                        break;
                    case 1:
                        _player.legs.currentAnim = 1;
                        break;
                    case 2:
                        _player.legs.currentAnim = 5;
                        break;
                    case 3:
                        _player.legs.currentAnim = 2;
                        break;
                }

            }
            if (keyState.IsKeyDown(Keys.A))
            {
                oldState = 6;
                pressed = true;
                direction += new Vector2(-1, 0);
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 2;

                switch (anim)
                {
                    case 0:
                        _player.legs.currentAnim = 3;
                        break;
                    case 1:
                        _player.legs.currentAnim = 4;
                        break;
                }
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                oldState = 5;
                pressed = true;
                direction += new Vector2(0, 1);
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 4;

                switch (anim)
                {
                    case 0:
                        _player.legs.currentAnim = 5;
                        break;
                    case 1:
                        _player.legs.currentAnim = 1;
                        break;
                    case 2:
                        _player.legs.currentAnim = 5;
                        break;
                    case 3:
                        _player.legs.currentAnim = 2;
                        break;
                }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                oldState = 4;
                pressed = true;
                direction += new Vector2(1, 0);
                frameCount++;
                int frameCountD = frameCount / 15;
                int anim = frameCountD % 2;

                switch (anim)
                {
                    case 0:
                        _player.legs.currentAnim = 0;
                        break;
                    case 1:
                        _player.legs.currentAnim = 4;
                        break;
                }
            }
            if (oldState == 5 && !pressed)
            {
                _player.legs.currentAnim = 5;
            }
            else if (oldState == 4 && !pressed)
            {
                _player.legs.currentAnim = 4;
            }

            _player.position += direction * _player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _player.legs.center = new Vector2(_player.legs.animations[_player.legs.currentAnim].Width / 2, 0);
            _player.legsOffset = new Vector2(_player.body.position.X, _player.body.position.Y + _player.body.animations[_player.body.currentAnim].Height);
        }
    }
}
