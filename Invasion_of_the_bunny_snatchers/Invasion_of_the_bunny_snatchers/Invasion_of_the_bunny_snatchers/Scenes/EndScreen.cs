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


namespace Invasion_of_the_bunny_snatchers.Scenes
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EndScreen : Microsoft.Xna.Framework.GameComponent
    {
        List<DrawScripts.AnimSprite> _UI;
        DrawScripts.AnimSprite _inputText;
        String _file = "Content/Assets/Leaderboard/LeaderBoard.xml";
        Dictionary<Keys,String> crke;
        Dictionary<Keys, Boolean> keyStatesOld;
        Dictionary<Keys, Boolean> keyStatesNew;
        public int Waves = 2;
        public EndScreen(Game game)
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
            DrawScripts.AnimSprite _spMainMenuB = new DrawScripts.AnimSprite(this.Game);
            _spMainMenuB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Mainmenu");
            _spMainMenuB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2.7f, this.Game.GraphicsDevice.Viewport.Height - 30);
            _spMainMenuB.animations = new List<Rectangle>();
            _spMainMenuB.animations.Add(new Rectangle(0, 0, _spMainMenuB.texture.Width, _spMainMenuB.texture.Height));
            _spMainMenuB.center = new Vector2(_spMainMenuB.texture.Width / 2, _spMainMenuB.texture.Height / 2);
            _spMainMenuB.scale = new Vector2(0.5f, 0.5f);
            _spMainMenuB.currentAnim = 0;
            _spMainMenuB.slika = true;
            this.Game.Components.Add(_spMainMenuB);

            DrawScripts.AnimSprite _spSaveB = new DrawScripts.AnimSprite(this.Game);
            _spSaveB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Save");
            _spSaveB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 1.5f, this.Game.GraphicsDevice.Viewport.Height - 30);
            _spSaveB.animations = new List<Rectangle>();
            _spSaveB.animations.Add(new Rectangle(0, 0, _spSaveB.texture.Width, _spSaveB.texture.Height));
            _spSaveB.center = new Vector2(_spSaveB.texture.Width / 2, _spSaveB.texture.Height / 2);
            _spSaveB.scale = new Vector2(0.5f, 0.5f);
            _spSaveB.currentAnim = 0;
            _spSaveB.slika = true;
            this.Game.Components.Add(_spSaveB);

            DrawScripts.AnimSprite _spInputBack = new DrawScripts.AnimSprite(this.Game);
            _spInputBack.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\InputBackGround");
            _spInputBack.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height/2);
            _spInputBack.animations = new List<Rectangle>();
            _spInputBack.animations.Add(new Rectangle(0, 0, _spInputBack.texture.Width, _spInputBack.texture.Height));
            _spInputBack.center = new Vector2(_spInputBack.texture.Width / 2, _spInputBack.texture.Height / 2);
            _spInputBack.scale = new Vector2(0.5f, 0.5f);
            _spInputBack.currentAnim = 0;
            _spInputBack.slika = true;
            this.Game.Components.Add(_spInputBack);

            DrawScripts.AnimSprite _EndScreenTextHeade = new DrawScripts.AnimSprite(this.Game);
            _EndScreenTextHeade.font = this.Game.Content.Load<SpriteFont>("Assets\\Font\\Font");
            _EndScreenTextHeade.slika = false;
            _EndScreenTextHeade.text = "            You died, \n            at "+Waves+" waves. \n \n enter your name for the Leaderboard!";
            _EndScreenTextHeade.position = new Vector2(((int)this.Game.GraphicsDevice.Viewport.Width / 2) - 200, 20);
            this.Game.Components.Add(_EndScreenTextHeade);

            _inputText = new DrawScripts.AnimSprite(this.Game);
            _inputText.font = this.Game.Content.Load<SpriteFont>("Assets\\Font\\Font");
            _inputText.slika = false;
            _inputText.text = "";//17 max leght za ime
            _inputText.position = new Vector2(((int)this.Game.GraphicsDevice.Viewport.Width / 2) - 86, (this.Game.GraphicsDevice.Viewport.Height / 2)-10);
            this.Game.Components.Add(_inputText);


            _UI = new List<DrawScripts.AnimSprite>();
            _UI.Add(_spMainMenuB);
            _UI.Add(_spSaveB);
            _UI.Add(_spInputBack);
            _UI.Add(_EndScreenTextHeade);
            _UI.Add(_inputText);

            DrawScripts.DrawMenu _draw = new DrawScripts.DrawMenu(this.Game, _UI);
            this.Game.Components.Add(_draw);

            crke = new Dictionary<Keys, string>();
            crke.Add(Keys.A,"a");
            crke.Add(Keys.B,"b");
            crke.Add(Keys.C,"c");
            crke.Add(Keys.D,"d");
            crke.Add(Keys.E, "e");
            crke.Add(Keys.F, "f");
            crke.Add(Keys.G, "g");
            crke.Add(Keys.H, "h");
            crke.Add(Keys.I, "i");
            crke.Add(Keys.J, "j");
            crke.Add(Keys.K, "k");
            crke.Add(Keys.L, "l");
            crke.Add(Keys.M, "m");
            crke.Add(Keys.N, "n");
            crke.Add(Keys.O, "o");
            crke.Add(Keys.P, "p");
            crke.Add(Keys.R, "r");
            crke.Add(Keys.S, "s");
            crke.Add(Keys.T, "t");
            crke.Add(Keys.U, "u");
            crke.Add(Keys.V, "v");
            crke.Add(Keys.Z, "z");
            crke.Add(Keys.Space, " ");

            keyStatesOld = new Dictionary<Keys, Boolean>();
            keyStatesOld.Add(Keys.A, false);
            keyStatesOld.Add(Keys.B, false);
            keyStatesOld.Add(Keys.C, false);
            keyStatesOld.Add(Keys.D, false);
            keyStatesOld.Add(Keys.E, false);
            keyStatesOld.Add(Keys.F, false);
            keyStatesOld.Add(Keys.G, false);
            keyStatesOld.Add(Keys.H, false);
            keyStatesOld.Add(Keys.I, false);
            keyStatesOld.Add(Keys.J, false);
            keyStatesOld.Add(Keys.K, false);
            keyStatesOld.Add(Keys.L, false);
            keyStatesOld.Add(Keys.M, false);
            keyStatesOld.Add(Keys.N, false);
            keyStatesOld.Add(Keys.O, false);
            keyStatesOld.Add(Keys.P, false);
            keyStatesOld.Add(Keys.R, false);
            keyStatesOld.Add(Keys.S, false);
            keyStatesOld.Add(Keys.T, false);
            keyStatesOld.Add(Keys.U, false);
            keyStatesOld.Add(Keys.V, false);
            keyStatesOld.Add(Keys.Z, false);
            keyStatesOld.Add(Keys.Back, false);
            keyStatesOld.Add(Keys.Space, false);

            keyStatesNew = new Dictionary<Keys, Boolean>();
            keyStatesNew.Add(Keys.A, false);
            keyStatesNew.Add(Keys.B, false);
            keyStatesNew.Add(Keys.C, false);
            keyStatesNew.Add(Keys.D, false);
            keyStatesNew.Add(Keys.E, false);
            keyStatesNew.Add(Keys.F, false);
            keyStatesNew.Add(Keys.G, false);
            keyStatesNew.Add(Keys.H, false);
            keyStatesNew.Add(Keys.I, false);
            keyStatesNew.Add(Keys.J, false);
            keyStatesNew.Add(Keys.K, false);
            keyStatesNew.Add(Keys.L, false);
            keyStatesNew.Add(Keys.M, false);
            keyStatesNew.Add(Keys.N, false);
            keyStatesNew.Add(Keys.O, false);
            keyStatesNew.Add(Keys.P, false);
            keyStatesNew.Add(Keys.R, false);
            keyStatesNew.Add(Keys.S, false);
            keyStatesNew.Add(Keys.T, false);
            keyStatesNew.Add(Keys.U, false);
            keyStatesNew.Add(Keys.V, false);
            keyStatesNew.Add(Keys.Z, false);
            keyStatesNew.Add(Keys.Back, false);
            keyStatesNew.Add(Keys.Space, false);

            this.Game.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            CheckMousePosOnCLick();
            buildTextOnKeyPress();
            base.Update(gameTime);
        }


        void CheckMousePosOnCLick()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //main menu gumb
                if (mouseState.X <= _UI[0].position.X + (_UI[0].texture.Width / 2) * 0.5f && mouseState.X >= _UI[0].position.X - (_UI[0].texture.Width / 2) * 0.5f && mouseState.Y <= _UI[0].position.Y + (_UI[0].texture.Height / 2) * 0.5f && mouseState.Y >= _UI[0].position.Y - (_UI[0].texture.Height / 2) * 0.5f)
                {
                    this.Game.Components.Clear();
                    Scenes.MainMenu menu = new Scenes.MainMenu(this.Game);
                    this.Game.Components.Add(menu);
                }

                //save gub
                if (mouseState.X <= _UI[1].position.X + (_UI[1].texture.Width / 2) * 0.5f && mouseState.X >= _UI[1].position.X - (_UI[1].texture.Width / 2) * 0.5f && mouseState.Y <= _UI[1].position.Y + (_UI[1].texture.Height / 2) * 0.5f && mouseState.Y >= _UI[1].position.Y - (_UI[1].texture.Height / 2) * 0.5f)
                {
                    save();
                    this.Game.Components.Clear();
                    Scenes.LeaderBoard leaderBoard = new Scenes.LeaderBoard(this.Game);
                    this.Game.Components.Add(leaderBoard);
                }
            }
        }

        void save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_file);

            XmlElement el = xmlDoc.CreateElement("Player");
            XmlElement name = xmlDoc.CreateElement("name");
            name.InnerText = _inputText.text;
            el.AppendChild(name);
            XmlElement score = xmlDoc.CreateElement("score");
            score.InnerText = Waves.ToString();
            el.AppendChild(score);
            xmlDoc.GetElementsByTagName("LeaderBoard")[0].AppendChild(el);
            xmlDoc.Save(_file);
        }

        void buildTextOnKeyPress()
        {
            KeyboardState state = Keyboard.GetState();

            for(int i = 0; i<keyStatesNew.Count;i++)
            {
                if(state.IsKeyDown(keyStatesNew.Keys.ToArray()[i]))
                {
                    keyStatesNew[keyStatesNew.Keys.ToArray()[i]] = true;
                }

                if (state.IsKeyUp(keyStatesNew.Keys.ToArray()[i]))
                {
                    keyStatesNew[keyStatesNew.Keys.ToArray()[i]] = false;
                }
            }

            for (int i = 0; i < keyStatesNew.Count; i++)
            {
                if (keyStatesNew[keyStatesNew.Keys.ToArray()[i]].Equals(true) && keyStatesOld[keyStatesNew.Keys.ToArray()[i]].Equals(false))
                {
                    //naredi teks razen ce je backspace in ceje predolgo
                    if(keyStatesNew.Keys.ToArray()[i].Equals(Keys.Back))
                    {
                        _inputText.text = _inputText.text.Substring(0,_inputText.text.Length - 1);
                    }else if(_inputText.text.Length <16)
                    {
                        _inputText.text += crke[keyStatesNew.Keys.ToArray()[i]];
                    }

                }
            }

            for (int i = 0; i < keyStatesNew.Count; i++)
            {
                keyStatesOld[keyStatesNew.Keys.ToArray()[i]] = keyStatesNew[keyStatesNew.Keys.ToArray()[i]];
            }
        }
    }
}
