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

namespace Invasion_of_the_bunny_snatchers
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //private System.Xml.XmlDocument doc;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //doc = new System.Xml.XmlDocument();
            //doc.Load("");

            //----------------------------------------------------------------------------Dodajanje ozadja
            DrawScripts.AnimSprite _spBackground = new DrawScripts.AnimSprite(this);
            _spBackground.texture = Content.Load<Texture2D>("Assets\\Background\\BackGround");
            _spBackground.position = Vector2.Zero;
            _spBackground.animations = new List<Rectangle>();
            _spBackground.animations.Add(new Rectangle(0,0,_spBackground.texture.Width,_spBackground.texture.Height));
            _spBackground.scale = new Vector2((float)graphics.GraphicsDevice.Viewport.Width / _spBackground.texture.Width, (float)graphics.GraphicsDevice.Viewport.Height / _spBackground.texture.Height);
            _spBackground.center = Vector2.Zero;
            _spBackground.currentAnim = 0;
            _spBackground.slika = true;
            Components.Add(_spBackground);


            //------------------------------------------------------------------------dodajanje trupla glavnem igralcu
            DrawScripts.AnimSprite _aspCharacter = new DrawScripts.AnimSprite(this);
            _aspCharacter.texture = Content.Load<Texture2D>("Assets\\Character\\Character");
            _aspCharacter.animations = new List<Rectangle>();
            _aspCharacter.animations.Add(new Rectangle(0, 0, 31, 43)); //desno
            _aspCharacter.animations.Add(new Rectangle(32, 0, 31, 43)); //gor
            _aspCharacter.animations.Add(new Rectangle(56, 0, 31, 43)); //dol
            _aspCharacter.animations.Add(new Rectangle(88, 0, 31, 43)); //levo
            _aspCharacter.scale = new Vector2(2f, 2f);
            _aspCharacter.center = Vector2.Zero;
            _aspCharacter.position = new Vector2((float)graphics.GraphicsDevice.Viewport.Width / 2, (float)graphics.GraphicsDevice.Viewport.Height / 2);
            _aspCharacter.slika = true;
            Components.Add(_aspCharacter);

            //---------------------------------------------------------------------------dodajanje nog glavnemu igralcu
            DrawScripts.AnimSprite _aspCharacterLegs = new DrawScripts.AnimSprite(this);
            _aspCharacterLegs.texture = _aspCharacter.texture;
            _aspCharacterLegs.animations = new List<Rectangle>();
            _aspCharacterLegs.animations.Add(new Rectangle(0, 57, 31, 28)); //desno
            _aspCharacterLegs.animations.Add(new Rectangle(32, 57, 31, 28)); //gor
            _aspCharacterLegs.animations.Add(new Rectangle(56, 57, 31, 28)); //dol
            _aspCharacterLegs.animations.Add(new Rectangle(88, 57, 31, 28)); //levo
            _aspCharacterLegs.animations.Add(new Rectangle(0, 86, 31, 28)); //stoji desno
            _aspCharacterLegs.animations.Add(new Rectangle(32, 86, 31, 28)); // stoji gor
            _aspCharacterLegs.scale = new Vector2(2f, 2f);
            _aspCharacterLegs.position = _aspCharacter.position + new Vector2(0, 88);
            _aspCharacterLegs.center = Vector2.Zero;
            _aspCharacterLegs.slika = true;
            Components.Add(_aspCharacterLegs);


           
            //-------------------------------------------------------------------dodajanje helth bara
            DrawScripts.AnimSprite _helthBar = new DrawScripts.AnimSprite(this);
            _helthBar.font = Content.Load<SpriteFont>("Assets\\Font\\Font"); ;
            _helthBar.slika = false;
            _helthBar.helthBar = true;
            Components.Add(_helthBar);

            //---------------------------------------------------------------------dodajanje texta za stopnjo
            DrawScripts.AnimSprite _LevelText = new DrawScripts.AnimSprite(this);
            _LevelText.font = _helthBar.font;
            _LevelText.slika = false;
            _LevelText.helthBar = false;
            _LevelText.text = "Trenutna stopnja: 1!";
            _LevelText.position = new Vector2(((int)graphics.GraphicsDevice.Viewport.Width / 2)-100, 0);
            Components.Add(_LevelText);

            DrawScripts.AnimSprite _Angle = new DrawScripts.AnimSprite(this);
            _Angle.font = _helthBar.font;
            _Angle.slika = false;
            _Angle.helthBar = false;
            _Angle.text = "Trenutn kot :!";
            _Angle.position = new Vector2(((int)graphics.GraphicsDevice.Viewport.Width / 2) - 100, 100);
            Components.Add(_Angle);

            //----------------------------------------------------------------------------dodajanje croshaira
            DrawScripts.AnimSprite _spCrossHair = new DrawScripts.AnimSprite(this);
            _spCrossHair.texture = Content.Load<Texture2D>("Assets\\Character\\CrossHair");
            _spCrossHair.position = Vector2.Zero;
            _spCrossHair.animations = new List<Rectangle>();
            _spCrossHair.animations.Add(new Rectangle(0, 0, _spCrossHair.texture.Width, _spCrossHair.texture.Height));
            _spCrossHair.scale = new Vector2(1, 1);
            _spCrossHair.center = new Vector2(_spCrossHair.texture.Width/2, _spCrossHair.texture.Height/2);
            _spCrossHair.currentAnim = 0;
            _spCrossHair.slika = true;
            Components.Add(_spCrossHair);

            //----------------------------------------------------------------------------------------------------------------------------------dodajanje motorja za igralca
            Motors.CharacterMovement _charMove = new Motors.CharacterMovement(this, _aspCharacter, _aspCharacterLegs, _helthBar, _spCrossHair, _Angle);
            Components.Add(_charMove);

            //-------------------------------------------------------------------dodajanje enemijev
            DrawScripts.AnimSprite _aspEnemy = new DrawScripts.AnimSprite(this);
            _aspEnemy.texture = Content.Load<Texture2D>("Assets\\Enemy\\Enemy");
            _aspEnemy.animations = new List<Rectangle>();
            _aspEnemy.animations.Add(new Rectangle(3, 7, 43, 19));
            _aspEnemy.animations.Add(new Rectangle(56, 3, 43, 22));
            _aspEnemy.scale = new Vector2(2f, 2f);
            _aspEnemy.position = new Vector2(50, 50);
            _aspEnemy.currentAnim = 0;
            _aspEnemy.center = Vector2.Zero;
            _aspEnemy.slika = true;
            Components.Add(_aspEnemy);

            DrawScripts.AnimSprite _aspEnemy2 = new DrawScripts.AnimSprite(this);
            _aspEnemy2.texture = _aspEnemy.texture;
            _aspEnemy2.animations = new List<Rectangle>();
            _aspEnemy2.animations.Add(new Rectangle(1, 36, 43, 20));
            _aspEnemy2.animations.Add(new Rectangle(56, 29, 43, 22));
            _aspEnemy2.scale = new Vector2(2f, 2f);
            _aspEnemy2.position = new Vector2(200, 50);
            _aspEnemy2.currentAnim = 0;
            _aspEnemy2.center = Vector2.Zero;
            _aspEnemy2.slika = true;
            Components.Add(_aspEnemy2);


            Motors.EnemyMovement _enemyMove = new Motors.EnemyMovement(this, _aspEnemy, 0);
            Components.Add(_enemyMove);

            Motors.EnemyMovement _enemyMove2 = new Motors.EnemyMovement(this, _aspEnemy2, 1);
            Components.Add(_enemyMove2);
            //--------------------------------------------------------------------------------konec dodajanje enimejv

            //------------------------------------------------------------------------------dodajanje powe upov
            DrawScripts.AnimSprite _powerUps = new DrawScripts.AnimSprite(this);
            _powerUps.texture = Content.Load<Texture2D>("Assets\\Character\\PowerUps");
            _powerUps.animations = new List<Rectangle>();
            _powerUps.animations.Add(new Rectangle(0, 0, 27, 28)); //multy shot
            _powerUps.animations.Add(new Rectangle(33, 0, 21, 28)); //speed shot
            _powerUps.animations.Add(new Rectangle(60, 0, 23, 28)); //speeeeeeed boooooost
            _powerUps.scale = new Vector2(2f, 2f);
            _powerUps.center = Vector2.Zero;
            Random rd = new Random();
            int x = rd.Next((int)graphics.GraphicsDevice.Viewport.Width);
            int y = rd.Next((int)graphics.GraphicsDevice.Viewport.Height);
            _powerUps.position = new Vector2(x, y);
            _powerUps.slika = true;
            int anim = rd.Next(3);
            _powerUps.currentAnim = anim;
            Components.Add(_powerUps);

            //------------------------------------------------------------------------------izrisevanje vseh objektov s skripto DrawInOrder
            List<DrawScripts.AnimSprite> _drawables = new List<DrawScripts.AnimSprite>();
            _drawables.Add(_spBackground);
            _drawables.Add(_aspEnemy);
            _drawables.Add(_aspEnemy2);
            _drawables.Add(_powerUps);
            _drawables.Add(_aspCharacter);
            _drawables.Add(_aspCharacterLegs);
            _drawables.Add(_spCrossHair);
            _drawables.Add(_helthBar);
            _drawables.Add(_LevelText);
            _drawables.Add(_Angle);

            DrawScripts.DrawInOrder _draw = new DrawScripts.DrawInOrder(this,_drawables);
            Components.Add(_draw);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);          


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
