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


namespace Invasion_of_the_bunny_snatchers.Scenes
{
    /// <summary>
    /// this.Game is a game component that implements IUpdateable.
    /// </summary>
    public class Level : Microsoft.Xna.Framework.GameComponent
    {
        public Level(Game game)
            : base(game)
        {
            // TODO: Construct any child this.Game.Components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  this.Game is where it can query for any required services and load this.Game.Content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //----------------------------------------------------------------------------Dodajanje ozadja
            DrawScripts.AnimSprite _spBackground = new DrawScripts.AnimSprite(this.Game);
            _spBackground.texture = this.Game.Content.Load<Texture2D>("Assets\\Background\\BackGround");
            _spBackground.position = Vector2.Zero;
            _spBackground.animations = new List<Rectangle>();
            _spBackground.animations.Add(new Rectangle(0, 0, _spBackground.texture.Width, _spBackground.texture.Height));
            _spBackground.scale = new Vector2((float)this.Game.GraphicsDevice.Viewport.Width / _spBackground.texture.Width, (float)this.Game.GraphicsDevice.Viewport.Height / _spBackground.texture.Height);
            _spBackground.center = Vector2.Zero;
            _spBackground.currentAnim = 0;
            _spBackground.slika = true;
            this.Game.Components.Add(_spBackground);


            //------------------------------------------------------------------------dodajanje trupla glavnem igralcu
            DrawScripts.AnimSprite _aspCharacter = new DrawScripts.AnimSprite(this.Game);
            _aspCharacter.texture = this.Game.Content.Load<Texture2D>("Assets\\Character\\Character");
            _aspCharacter.animations = new List<Rectangle>();
            _aspCharacter.animations.Add(new Rectangle(0, 0, 31, 43)); //desno
            _aspCharacter.animations.Add(new Rectangle(32, 0, 31, 43)); //gor
            _aspCharacter.animations.Add(new Rectangle(56, 0, 31, 43)); //dol
            _aspCharacter.animations.Add(new Rectangle(88, 0, 31, 43)); //levo
            _aspCharacter.scale = new Vector2(2f, 2f);
            _aspCharacter.center = Vector2.Zero;
            this.Game.Components.Add(_aspCharacter);

            //---------------------------------------------------------------------------dodajanje nog glavnemu igralcu
            DrawScripts.AnimSprite _aspCharacterLegs = new DrawScripts.AnimSprite(this.Game);
            _aspCharacterLegs.texture = _aspCharacter.texture;
            _aspCharacterLegs.animations = new List<Rectangle>();
            _aspCharacterLegs.animations.Add(new Rectangle(0, 57, 31, 28)); //desno
            _aspCharacterLegs.animations.Add(new Rectangle(32, 57, 31, 28)); //gor
            _aspCharacterLegs.animations.Add(new Rectangle(56, 57, 31, 28)); //dol
            _aspCharacterLegs.animations.Add(new Rectangle(88, 57, 31, 28)); //levo
            _aspCharacterLegs.animations.Add(new Rectangle(0, 86, 31, 28)); //stoji desno
            _aspCharacterLegs.animations.Add(new Rectangle(32, 86, 31, 28)); // stoji gor
            _aspCharacterLegs.scale = new Vector2(2f, 2f);
            _aspCharacterLegs.center = Vector2.Zero;
            this.Game.Components.Add(_aspCharacterLegs);

            Player.Player _player = new Player.Player(this.Game);
            _player.body = _aspCharacter;
            _player.legs = _aspCharacterLegs;
            _player.position = new Vector2((float)this.Game.GraphicsDevice.Viewport.Width / 2, (float)this.Game.GraphicsDevice.Viewport.Height / 2);
            _player.speed = 100;
            _player.shootSpeed = 1f;
            _player.multipleBoolets = false;
            _player.helth = 100;
            _player.shoot1 = this.Game.Content.Load<SoundEffect>("Assets\\Sounds\\GunSound1");
            _player.shoot2 = this.Game.Content.Load<SoundEffect>("Assets\\Sounds\\GunSound2");
            this.Game.Components.Add(_player);



            //-------------------------------------------------------------------dodajanje helth bara
            DrawScripts.AnimSprite _helthBar = new DrawScripts.AnimSprite(this.Game);
            _helthBar.font = this.Game.Content.Load<SpriteFont>("Assets\\Font\\Font"); ;
            _helthBar.slika = false;
            _helthBar.helthBar = true;
            this.Game.Components.Add(_helthBar);

            //---------------------------------------------------------------------dodajanje texta za stopnjo
            DrawScripts.AnimSprite _LevelText = new DrawScripts.AnimSprite(this.Game);
            _LevelText.font = _helthBar.font;
            _LevelText.slika = false;
            _LevelText.helthBar = false;
            _LevelText.text = "Trenutna stopnja: 1";
            _LevelText.position = new Vector2(((int)this.Game.GraphicsDevice.Viewport.Width / 2) - 100, 0);
            this.Game.Components.Add(_LevelText);


            //----------------------------------------------------------------------------dodajanje croshaira
            DrawScripts.AnimSprite _spCrossHair = new DrawScripts.AnimSprite(this.Game);
            _spCrossHair.texture = this.Game.Content.Load<Texture2D>("Assets\\Character\\CrossHair");
            _spCrossHair.position = Vector2.Zero;
            _spCrossHair.animations = new List<Rectangle>();
            _spCrossHair.animations.Add(new Rectangle(0, 0, _spCrossHair.texture.Width, _spCrossHair.texture.Height));
            _spCrossHair.scale = new Vector2(1, 1);
            _spCrossHair.center = new Vector2(_spCrossHair.texture.Width / 2, _spCrossHair.texture.Height / 2);
            _spCrossHair.currentAnim = 0;
            _spCrossHair.slika = true;
            this.Game.Components.Add(_spCrossHair);

            //----------------------------------------------------------------------------dodajanje metka
            DrawScripts.AnimSprite _spBullet = new DrawScripts.AnimSprite(this.Game);
            _spBullet.texture = this.Game.Content.Load<Texture2D>("Assets\\Character\\Bullet");
            _spBullet.animations = new List<Rectangle>();
            _spBullet.animations.Add(new Rectangle(0, 0, _spBullet.texture.Width, _spBullet.texture.Height));
            _spBullet.scale = new Vector2(1, 1);
            _spBullet.center = new Vector2(_spBullet.texture.Width / 2, _spBullet.texture.Height / 2);
            _spBullet.currentAnim = 0;
            this.Game.Components.Add(_spBullet);

            Bullet.Bullet _bullet = new Bullet.Bullet(this.Game);
            _bullet.body = _spBullet;
            this.Game.Components.Add(_bullet);



            //-------------------------------------------------------------------dodajanje enemijev
            DrawScripts.AnimSprite _aspEnemy = new DrawScripts.AnimSprite(this.Game);
            _aspEnemy.texture = this.Game.Content.Load<Texture2D>("Assets\\Enemy\\Enemy");
            _aspEnemy.animations = new List<Rectangle>();
            _aspEnemy.animations.Add(new Rectangle(3, 7, 43, 19));
            _aspEnemy.animations.Add(new Rectangle(56, 3, 43, 22));
            _aspEnemy.scale = new Vector2(2f, 2f);
            _aspEnemy.currentAnim = 0;
            _aspEnemy.center = new Vector2(21, 11);
            this.Game.Components.Add(_aspEnemy);

            DrawScripts.AnimSprite _aspEnemy2 = new DrawScripts.AnimSprite(this.Game);
            _aspEnemy2.texture = _aspEnemy.texture;
            _aspEnemy2.animations = new List<Rectangle>();
            _aspEnemy2.animations.Add(new Rectangle(1, 36, 43, 20));
            _aspEnemy2.animations.Add(new Rectangle(56, 29, 43, 22));
            _aspEnemy2.scale = new Vector2(2f, 2f);
            _aspEnemy2.currentAnim = 1;
            _aspEnemy2.center = new Vector2(21, 11);
            this.Game.Components.Add(_aspEnemy2);

            //------------------------------------------------------------------------------dodajanje powe upov
            DrawScripts.AnimSprite _powerUps = new DrawScripts.AnimSprite(this.Game);
            _powerUps.texture = this.Game.Content.Load<Texture2D>("Assets\\Character\\PowerUps");
            this.Game.Components.Add(_powerUps);


            //------------------------------------------------------------------------------izrisevanje vseh objektov s skripto DrawInOrder

            List<DrawScripts.AnimSprite> _background = new List<DrawScripts.AnimSprite>();
            _background.Add(_spBackground);

            List<Enemy.enemy> _enemys = new List<Enemy.enemy>();


            List<DrawScripts.AnimSprite> _UI = new List<DrawScripts.AnimSprite>();
            _UI.Add(_spCrossHair);
            _UI.Add(_helthBar);
            _UI.Add(_LevelText);

            DrawScripts.DrawInOrder _draw = new DrawScripts.DrawInOrder(this.Game, _background, _enemys, _player, _UI, new List<DrawScripts.AnimSprite>());
            this.Game.Components.Add(_draw);
            //--------------------------------------------------------------------------------------------------- konec dodajanja elementov v izrisevanje

            //------------------------------------------------------------------------------------motor za metke
            Colliders.ColliderBulletscs collider = new Colliders.ColliderBulletscs(this.Game);
            this.Game.Components.Add(collider);

            //-------------------------------------------------------------------- motro za enemije
            Motors.EnemyMovement _enemyMove = new Motors.EnemyMovement(this.Game);
            this.Game.Components.Add(_enemyMove);

            //----------------------------------------------------------------------------------------------------------------------------------dodajanje motorja za igralca
            Motors.CharacterMovement _charMove = new Motors.CharacterMovement(this.Game);
            this.Game.Components.Add(_charMove);

            //----------------------------------------------------------------------------dodajanje collider za igralca
            Colliders.CharacterCollider _charCollider = new Colliders.CharacterCollider(this.Game);
            this.Game.Components.Add(_charCollider);

            //--------------------------------------------------------------add wave manager
            Manger.WaveManger _waveManager = new Manger.WaveManger(this.Game);
            this.Game.Components.Add(_waveManager);

            this.Game.IsMouseVisible = false;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
