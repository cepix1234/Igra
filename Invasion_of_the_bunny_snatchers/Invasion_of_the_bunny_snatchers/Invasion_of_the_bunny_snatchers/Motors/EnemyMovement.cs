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
    public class EnemyMovement : Microsoft.Xna.Framework.GameComponent
    {
        List <Enemy.enemy> _enemys;
        Player.Player _player;
        Bullet.Bullet _bullet;
        DrawScripts.DrawInOrder _draw;
        public EnemyMovement(Game game)
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
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            _player = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._player;
            _bullet = Game.Components.OfType<Bullet.Bullet>().ToList()[0];
            _draw = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0];
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if(Game.Components.OfType<DrawScripts.DrawInOrder>().ToList().Count != 0)
            {

                _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            }
            EnemyAI(gameTime);
            base.Update(gameTime);
        }

        private void EnemyAI(GameTime gameTime)
        {
            foreach (Enemy.enemy enemy in _enemys)
            {
                //rotaciaj za igralcem
                Vector2 direction = _player.position - enemy.position;
                float distance = direction.Length();
                direction /= distance;
                direction.Normalize();
                double kot = Math.Atan2(_player.position.Y - enemy.position.Y, _player.position.X - enemy.position.X);
                if (kot <= 1.66 && kot >= -1.56)
                {
                    enemy.orientacija = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    enemy.orientacija = SpriteEffects.None;
                    kot = kot - 3.2f;
                }
                enemy.rotation = (float)kot;


                //movement for enemy based on type
                Boolean move = false;
                if (enemy.enemyType == 0 && distance > 40)
                {
                    enemy.position+= direction * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (direction != Vector2.Zero)
                    {
                        move = true;
                    }
                }
                else if (enemy.enemyType == 1 && distance > 300)
                {
                    enemy.position += direction * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (direction != Vector2.Zero)
                    {
                        move = true;
                    }
                }

                //Enemy atack based on type and distance
                if (enemy.enemyType == 0 && distance <= 40)
                {
                    enemy.lastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (enemy.lastFire > enemy.shootSpeed)
                    {
                        //bite
                        enemy.attack = true;
                        enemy.lastFire = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        enemy.lastFire = 0;
                    }else
                    {
                        enemy.attack = false;
                    }
                }
                else if (enemy.enemyType == 1 && distance <= 300)
                {
                    enemy.lastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (enemy.lastFire > enemy.shootSpeed)
                    {
                        //shoot at player
                        Shoot(enemy);
                        float volume = 0.4f;
                        float pitch = 0.0f;
                        float pan = 0.0f;
                        Random rnd = new Random();
                        int rand = rnd.Next(0, 2);
                        if (rand == 0)
                        {
                            enemy.shoot1.Play(volume, pitch, pan);
                        }
                        else
                        {
                            enemy.shoot2.Play(volume, pitch, pan);
                        }
                        enemy.lastFire = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        enemy.lastFire = 0;
                    }
                }


                //if enemy moves animate every 15 frames
                if (move)
                {
                    enemy.frameEnemy++;
                    int frameCountD = enemy.frameEnemy / 15;
                    int anim = frameCountD % 2;

                    switch (anim)
                    {
                        case 0:
                            enemy.body.currentAnim = 1;
                            break;

                        case 1:
                            enemy.body.currentAnim = 0;
                            break;
                    }
                    enemy.lastvoice += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(enemy.lastvoice > 2f)
                    {
                        float volume = 0.4f;
                        float pitch = 0.0f;
                        float pan = 0.0f;
                        enemy.voice.Play(volume, pitch, pan);
                        enemy.lastvoice = 0;
                    }
                }else
                {
                    enemy.body.currentAnim = 0;
                }
            }
        }

        private void Shoot(Enemy.enemy enemy)
        {
            Vector2 direction = _player.position - enemy.position;
            float distance = direction.Length();
            direction /= distance;
            direction.Normalize();
            double kot = Math.Atan2(_player.position.Y - enemy.position.Y, _player.position.X - enemy.position.X);

            Bullet.Bullet bulet = new Bullet.Bullet(Game);
            bulet.body = _bullet.body;
            bulet.position = enemy.position;
            bulet.direction = direction;
            bulet.rotation = (float)kot;
            bulet.player = false;
            Game.Components.Add(bulet);
            _draw._bullets.Add(bulet);
        }
    }
}
