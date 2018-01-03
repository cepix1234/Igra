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
using System.Xml;


namespace Invasion_of_the_bunny_snatchers.Manger
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class WaveManger : Microsoft.Xna.Framework.GameComponent
    {
        List<Enemy.enemy> _enemys;
        String _file = "Content/Assets/Manager/Waves.xml";
        DrawScripts.AnimSprite _aspEnemy1;
        DrawScripts.AnimSprite _aspEnemy2;
        List<int[]> _waves;
        int CurrentWave;
        List<Vector2> spawnPoints;
        SoundEffect _shoot1;
        SoundEffect _shoot2;
        SoundEffect _die;
        SoundEffect _voice;
        DrawScripts.AnimSprite waveText;
        float SpawnTime = 0;
        Boolean spawn = true;
        public WaveManger(Game game)
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

            //load lound efects
            _shoot1 = Game.Content.Load<SoundEffect>("Assets\\Sounds\\GunSound1");
            _shoot2 = Game.Content.Load<SoundEffect>("Assets\\Sounds\\GunSound2");
            _die = Game.Content.Load<SoundEffect>("Assets\\Sounds\\BunnyDying");
            _voice = Game.Content.Load<SoundEffect>("Assets\\Sounds\\BunnySound");

            //initialize
            waveText = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._UI[2];
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            _aspEnemy1 = Game.Components.OfType<DrawScripts.AnimSprite>().ToList()[7];
            _aspEnemy2 = Game.Components.OfType<DrawScripts.AnimSprite>().ToList()[8];
            _waves = new List<int[]>();
            spawnPoints = new List<Vector2>() { };
            readFile();
            DefineSpawnPoints();
            CurrentWave = 0;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if(spawn)
            {
                SpawnEnemysOfWave(CurrentWave,gameTime);
            }
            _enemys = Game.Components.OfType<DrawScripts.DrawInOrder>().ToList()[0]._enemys;
            checkIfZeroEnemys();
            base.Update(gameTime);
        }

        void DefineSpawnPoints()
        {
            //this.Game.GraphicsDevice.Viewport.Width

            int korakX = (int)(this.Game.GraphicsDevice.Viewport.Width / 4);
            int korakY = (int)(this.Game.GraphicsDevice.Viewport.Height / 4);

            int stranica = 0;
            for(int i = 0; i<4;i++)
            {
                stranica += korakX;
                spawnPoints.Add(new Vector2(stranica,-50));
            }

            stranica = 0;
            for (int i = 0; i < 4; i++)
            {
                stranica += korakX;
                spawnPoints.Add(new Vector2(stranica, (int) this.Game.GraphicsDevice.Viewport.Height+50));
            }

            stranica = 0;
            for (int i = 0; i < 4; i++)
            {
                stranica += korakY;
                spawnPoints.Add(new Vector2(-50, stranica));
            }

            stranica = 0;
            for (int i = 0; i < 4; i++)
            {
                stranica += korakY;
                spawnPoints.Add(new Vector2((int)this.Game.GraphicsDevice.Viewport.Width + 50, stranica));
            }
        }

        void readFile ()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_file);
            
            XmlNodeList mele = xmlDoc.GetElementsByTagName("mele");
            XmlNodeList range = xmlDoc.GetElementsByTagName("range");
            for(int i = 0; i< mele.Count;i++)
            {
                int[] wave = new int[] { Int32.Parse(mele[i].InnerText), Int32.Parse(range[i].InnerText) };
                _waves.Add(wave);
            }
        }

        void checkIfZeroEnemys ()
        {
            if (_enemys.Count <= 0 && !spawn)
            {
                //add neew enmys plus edit wave text
                CurrentWave++;
                spawn = true;
                int diss = CurrentWave + 1;
                waveText.text = "Trenutna stopnja: "+diss;
            }
        }

        void checkIfEndOFWaves()
        {
            if (_enemys.Count <= 0 && !spawn && CurrentWave == 12)
            {
                // show end screen 
            }
        }

        void SpawnEnemysOfWave (int wave, GameTime gameTime)
        {
            Random rnd = new Random();
            SpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //spawn mele
            if (_waves[wave][0] > 0 && SpawnTime >= 1)
            {
                int randomSP = rnd.Next(0, 16);
                Enemy.enemy enemy = new Enemy.enemy(this.Game);
                enemy.body = _aspEnemy1;
                enemy.position = spawnPoints[randomSP];
                enemy.attack = false;
                enemy.shootSpeed = 6f;
                enemy.enemyType = 0;
                enemy.lastFire = 0;
                enemy.frameEnemy = 0;
                enemy.shoot1 = _shoot1;
                enemy.shoot2 = _shoot2;
                enemy.die = _die;
                enemy.voice = _voice;
                this.Game.Components.Add(enemy);
                _enemys.Add(enemy);
                _waves[wave][0]--;
                SpawnTime = 0;
            }

            if (_waves[wave][1] > 0 && SpawnTime >= 1)
            {
                //spawn range
                int randomSP = rnd.Next(0, 16);
                Enemy.enemy enemy = new Enemy.enemy(this.Game);
                enemy.body = _aspEnemy2;
                enemy.position = spawnPoints[randomSP];
                enemy.attack = false;
                enemy.shootSpeed = 6f;
                enemy.enemyType = 1;
                enemy.lastFire = 0;
                enemy.frameEnemy = 0;
                enemy.shoot1 = _shoot1;
                enemy.shoot2 = _shoot2;
                enemy.die = _die;
                enemy.voice = _voice;
                this.Game.Components.Add(enemy);
                _enemys.Add(enemy);
                _waves[wave][1]--;
                SpawnTime = 0;
            }

            if(_waves[wave][0] == 0 && _waves[wave][1] == 0)
            {
                spawn = false;
            }
        }
    }
}
