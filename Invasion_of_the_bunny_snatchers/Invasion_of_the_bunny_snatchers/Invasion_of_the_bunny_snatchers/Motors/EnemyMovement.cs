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
        List <DrawScripts.AnimSprite> _enemys;
        DrawScripts.AnimSprite _player;
        DrawScripts.AnimSprite _bullet;
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
            _player = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._player[0];
            _bullet = Game.Components.OfType<DrawScripts.AnimSprite>().ToList()[6];
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
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            EnemyAI(gameTime);
            base.Update(gameTime);
        }

        private void EnemyAI(GameTime gameTime)
        {
            foreach (DrawScripts.AnimSprite enemy in _enemys)
            {
                if (!enemy.powerup )
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
                                enemy.currentAnim = 1;
                                break;

                            case 1:
                                enemy.currentAnim = 0;
                                break;
                        }
                    }else
                    {
                        enemy.currentAnim = 0;
                    }
                }
            }
        }

        private void Shoot(DrawScripts.AnimSprite enemy)
        {
            Vector2 direction = _player.position - enemy.position;
            float distance = direction.Length();
            direction /= distance;
            direction.Normalize();
            double kot = Math.Atan2(_player.position.Y - enemy.position.Y, _player.position.X - enemy.position.X);

            DrawScripts.AnimSprite bulet = new DrawScripts.AnimSprite(Game);
            bulet.texture = _bullet.texture;
            bulet.position = enemy.position; // dodeli tocno lokacijo spawna glede na rotacijo
            bulet.animations = new List<Rectangle>();
            bulet.animations.Add(new Rectangle(0, 0, bulet.texture.Width, bulet.texture.Height));
            bulet.scale = new Vector2(1, 1);
            bulet.center = new Vector2(bulet.texture.Width / 2, bulet.texture.Height / 2);
            bulet.currentAnim = 0;
            bulet.slika = true;
            bulet.direction = direction;
            bulet.rotation = (float)kot;
            bulet.player = false;
            Game.Components.Add(bulet);
            _draw._bullets.Add(bulet);
        }
    }
}
