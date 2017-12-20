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
        public List<Bullet.Bullet> _bullets;
        public List<DrawScripts.AnimSprite> _powerUps;
        public List<Enemy.enemy> _enemys;
        public DrawScripts.DrawInOrder _draw;
        private DrawScripts.AnimSprite _powerup;
        public ColliderBulletscs(Game game)
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
            _bullets = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._bullets;
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            _powerUps = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._powerUps;
            _draw = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0];
            _powerup = Game.Components.OfType<DrawScripts.AnimSprite>().ToList()[9];
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            _bullets = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._bullets;
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            _powerUps = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._powerUps;
            bullethitrabit();
            base.Update(gameTime);
        }       


        private void bullethitrabit()
        {
            int itiBullet = 0;
            int itiEnemy = 0;
            int anim = 0;
            List<DrawScripts.AnimSprite> dodaniPowerupi = new List<DrawScripts.AnimSprite>();
            List<int> zadetiEnemyi = new List<int>();
            Boolean zadetek = false;
            foreach (Bullet.Bullet bullet in _bullets)
            {
                if (bullet.player)
                {
                    itiEnemy = 0;
                    foreach (Enemy.enemy enemy in _enemys)
                    {
                        if (enemy.position.X + 15 * enemy.body.scale.X >= bullet.position.X && enemy.position.Y + 15 * enemy.body.scale.Y >= bullet.position.Y && enemy.position.X - 15 * enemy.body.scale.X <= bullet.position.X && enemy.position.Y - 15 * enemy.body.scale.Y <= bullet.position.Y)
                        {
                            // kill enemy and bullet check if spawn power up
                            zadetiEnemyi.Add(itiEnemy);
                            Random rnd = new Random();
                            int randZaDrop = rnd.Next(0, 101);
                            if(randZaDrop <50)
                            {
                                int randZaKateriDrop = rnd.Next(0, 101);
                                if(randZaKateriDrop >= 0 && randZaKateriDrop < 34)
                                {
                                    anim = 0;
                                }
                                else if(randZaKateriDrop > 33 && randZaKateriDrop < 67)
                                {
                                    anim = 1;
                                }else if(randZaKateriDrop > 66 && randZaKateriDrop <= 100)
                                {
                                    anim = 2;
                                }

                                DrawScripts.AnimSprite _powerUps = new DrawScripts.AnimSprite(Game);
                                _powerUps.texture = _powerup.texture;
                                _powerUps.animations = new List<Rectangle>();
                                _powerUps.animations.Add(new Rectangle(0, 0, 27, 28)); //multy shot
                                _powerUps.animations.Add(new Rectangle(33, 0, 21, 28)); //speed shot
                                _powerUps.animations.Add(new Rectangle(60, 0, 23, 28)); //speeeeeeed boooooost
                                _powerUps.scale = new Vector2(2f, 2f);
                                _powerUps.center = Vector2.Zero;
                                _powerUps.position = enemy.position;
                                _powerUps.slika = true;
                                _powerUps.currentAnim = anim;
                                dodaniPowerupi.Add(_powerUps);
                                Game.Components.Add(_powerUps);
                            }
                            zadetek = true;
                            float volume = 0.4f;
                            float pitch = 0.0f;
                            float pan = 0.0f;
                            enemy.die.Play(volume, pitch, pan);
                            break;
                        }
                        itiEnemy++;
                    }
                    if (zadetek)
                    {
                        break;
                    }
                }else
                {
                    //Check if player is hit
                }
                itiBullet++;
            }

            //izbrisi vse zadeto iz tabel
            if(zadetek)
            {
                _bullets.RemoveAt(itiBullet);
            }
            foreach(int i in zadetiEnemyi)
            {
                _enemys.RemoveAt(i);
            }
            foreach(DrawScripts.AnimSprite i in dodaniPowerupi)
            {
                _powerUps.Add(i);
            }
        }
    }
}
