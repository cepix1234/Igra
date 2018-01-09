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
    public class LeaderBoard : Microsoft.Xna.Framework.GameComponent
    {
        List<DrawScripts.AnimSprite> _UI;
        DrawScripts.AnimSprite _LeaderBoardText;
        String _file = "Content/Assets/Leaderboard/LeaderBoard.xml";
        List<String[]> _LeaderBoard;
        int top;
        int bot;
        int PrevisuMouseVale;
        float dissTime = 0;
        public LeaderBoard(Game game)
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
            _spMainMenuB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 3.7f, this.Game.GraphicsDevice.Viewport.Height - 30);
            _spMainMenuB.animations = new List<Rectangle>();
            _spMainMenuB.animations.Add(new Rectangle(0, 0, _spMainMenuB.texture.Width, _spMainMenuB.texture.Height));
            _spMainMenuB.center = new Vector2(_spMainMenuB.texture.Width / 2, _spMainMenuB.texture.Height / 2);
            _spMainMenuB.scale = new Vector2(0.5f, 0.5f);
            _spMainMenuB.currentAnim = 0;
            _spMainMenuB.slika = true;
            this.Game.Components.Add(_spMainMenuB);

            DrawScripts.AnimSprite _spRetryB = new DrawScripts.AnimSprite(this.Game);
            _spRetryB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Retry");
            _spRetryB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height -30);
            _spRetryB.animations = new List<Rectangle>();
            _spRetryB.animations.Add(new Rectangle(0, 0, _spRetryB.texture.Width, _spRetryB.texture.Height));
            _spRetryB.center = new Vector2(_spRetryB.texture.Width / 2, _spRetryB.texture.Height / 2);
            _spRetryB.scale = new Vector2(0.5f, 0.5f);
            _spRetryB.currentAnim = 0;
            _spRetryB.slika = true;
            this.Game.Components.Add(_spRetryB);

            DrawScripts.AnimSprite _spExitB = new DrawScripts.AnimSprite(this.Game);
            _spExitB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Exit");
            _spExitB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 1.37f, this.Game.GraphicsDevice.Viewport.Height -30);
            _spExitB.animations = new List<Rectangle>();
            _spExitB.animations.Add(new Rectangle(0, 0, _spExitB.texture.Width, _spExitB.texture.Height));
            _spExitB.center = new Vector2(_spExitB.texture.Width / 2, _spExitB.texture.Height / 2);
            _spExitB.scale = new Vector2(0.5f, 0.5f);
            _spExitB.currentAnim = 0;
            _spExitB.slika = true;
            this.Game.Components.Add(_spExitB);

            DrawScripts.AnimSprite _LeaderBoardTextHead = new DrawScripts.AnimSprite(this.Game);
            _LeaderBoardTextHead.font = this.Game.Content.Load<SpriteFont>("Assets\\Font\\Font");
            _LeaderBoardTextHead.slika = false;
            _LeaderBoardTextHead.text = "Name -- Number of waves";
            _LeaderBoardTextHead.position = new Vector2(((int)this.Game.GraphicsDevice.Viewport.Width / 2) - 100, 20);
            this.Game.Components.Add(_LeaderBoardTextHead);

            
            // 14 izpisev je lepa stevilka
            _LeaderBoardText = new DrawScripts.AnimSprite(this.Game);
            _LeaderBoardText.font = this.Game.Content.Load<SpriteFont>("Assets\\Font\\Font");
            _LeaderBoardText.slika = false;
            _LeaderBoardText.text = "neki -- 13";
            _LeaderBoardText.position = new Vector2(((int)this.Game.GraphicsDevice.Viewport.Width / 2) - 100, 50);
            this.Game.Components.Add(_LeaderBoardText);


            _UI = new List<DrawScripts.AnimSprite>();
            _UI.Add(_spMainMenuB);
            _UI.Add(_spRetryB);
            _UI.Add(_spExitB);
            _UI.Add(_LeaderBoardTextHead);
            _UI.Add(_LeaderBoardText);

            DrawScripts.DrawMenu _draw = new DrawScripts.DrawMenu(this.Game, _UI);
            this.Game.Components.Add(_draw);

            PrevisuMouseVale = 0;

            top = 0;
            bot = 14;
            _LeaderBoard = new List<string[]>();
            fillLeaderBoard();
            makeTextbetween();
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
            dissTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dissTime >= 1)
            {
                CheckMousePosOnCLick();
            }
            base.Update(gameTime);
        }

        void CheckMousePosOnCLick()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //main menu gumb
                if (mouseState.X <= _UI[0].position.X + (_UI[0].texture.Width/2) * 0.5f && mouseState.X >= _UI[0].position.X - (_UI[0].texture.Width/2) * 0.5f && mouseState.Y <= _UI[0].position.Y + (_UI[0].texture.Height/2) * 0.5f && mouseState.Y >= _UI[0].position.Y - (_UI[0].texture.Height/2) * 0.5f)
                {
                    this.Game.Components.Clear();
                    Scenes.MainMenu menu = new Scenes.MainMenu(this.Game);
                    this.Game.Components.Add(menu);
                }

                //retry gumb
                if (mouseState.X <= _UI[1].position.X + (_UI[1].texture.Width/2) * 0.5f && mouseState.X >= _UI[1].position.X - (_UI[1].texture.Width/2) * 0.5f && mouseState.Y <= _UI[1].position.Y + (_UI[1].texture.Height/2) * 0.5f && mouseState.Y >= _UI[1].position.Y - (_UI[1].texture.Height/2) * 0.5f)
                {
                    this.Game.Components.Clear();
                    Scenes.Level level = new Scenes.Level(this.Game);
                    this.Game.Components.Add(level);
                }

                //Exit gumb
                if (mouseState.X <= _UI[2].position.X + (_UI[2].texture.Width/2) * 0.5f && mouseState.X >= _UI[2].position.X - (_UI[2].texture.Width/2) * 0.5f && mouseState.Y <= _UI[2].position.Y + (_UI[2].texture.Height/2) * 0.5f && mouseState.Y >= _UI[2].position.Y - (_UI[2].texture.Height/2) * 0.5f)
                {
                    this.Game.Exit();
                }
            }
            if(mouseState.ScrollWheelValue != PrevisuMouseVale)
            {
                int diff = PrevisuMouseVale - mouseState.ScrollWheelValue;
                if(diff > 0)
                {
                    top = Math.Min(Math.Max(++top, 0), _LeaderBoard.Count-14);
                    bot = Math.Min(Math.Max(++bot, 14), _LeaderBoard.Count);
                }
                else
                {
                    top = Math.Min(Math.Max(--top, 0), _LeaderBoard.Count - 14);
                    bot = Math.Min(Math.Max(--bot, 14), _LeaderBoard.Count);
                }
                PrevisuMouseVale = mouseState.ScrollWheelValue;
                makeTextbetween();
            }
        }

        void fillLeaderBoard()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_file);

            XmlNodeList name = xmlDoc.GetElementsByTagName("name");
            XmlNodeList score = xmlDoc.GetElementsByTagName("score");
            for (int i = 0; i < name.Count; i++)
            {
                String[] person = new String[] { name[i].InnerText, score[i].InnerText };
                _LeaderBoard.Add(person);
            }

            Boolean switched = false;
            do
            {
                switched = false;
                for (int y = 1; y < _LeaderBoard.Count; y++)
                {
                    if (Int32.Parse(_LeaderBoard[y][1]) > Int32.Parse(_LeaderBoard[y - 1][1]))
                    {
                        switched = true;
                        String[] temp = _LeaderBoard[y];
                        _LeaderBoard[y] = _LeaderBoard[y - 1];
                        _LeaderBoard[y - 1] = temp;
                    }
                }
            } while (switched);
        }

        void makeTextbetween()
        {
            _LeaderBoardText.text = "";
            if(_LeaderBoard.Count >= 14)
            {
                for (int i = top; i < bot; i++)
                {
                    _LeaderBoardText.text += _LeaderBoard[i][0] + " -- " + _LeaderBoard[i][1] + "\n";
                }
            }else
            {
                for (int i = top; i < _LeaderBoard.Count; i++)
                {
                    _LeaderBoardText.text += _LeaderBoard[i][0] + " -- " + _LeaderBoard[i][1] + "\n";
                }
            }
            
        }
    }
}
