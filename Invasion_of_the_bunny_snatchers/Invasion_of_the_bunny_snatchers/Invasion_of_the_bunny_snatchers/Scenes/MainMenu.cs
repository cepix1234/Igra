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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainMenu : Microsoft.Xna.Framework.GameComponent
    {
        List<DrawScripts.AnimSprite> _UI;
        public MainMenu(Game game)
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
            DrawScripts.AnimSprite _spStartB = new DrawScripts.AnimSprite(this.Game);
            _spStartB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Start");
            _spStartB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height / 4);
            _spStartB.animations = new List<Rectangle>();
            _spStartB.animations.Add(new Rectangle(0, 0, _spStartB.texture.Width, _spStartB.texture.Height));
            _spStartB.center = new Vector2(_spStartB.texture.Width / 2, _spStartB.texture.Height / 2);
            _spStartB.scale = new Vector2(0.5f, 0.5f);
            _spStartB.currentAnim = 0;
            _spStartB.slika = true;
            this.Game.Components.Add(_spStartB);

            DrawScripts.AnimSprite _spLeaderBoardB = new DrawScripts.AnimSprite(this.Game);
            _spLeaderBoardB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Leaderboard");
            _spLeaderBoardB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height / 2.7f);
            _spLeaderBoardB.animations = new List<Rectangle>();
            _spLeaderBoardB.animations.Add(new Rectangle(0, 0, _spLeaderBoardB.texture.Width, _spLeaderBoardB.texture.Height));
            _spLeaderBoardB.center = new Vector2(_spLeaderBoardB.texture.Width / 2, _spLeaderBoardB.texture.Height / 2);
            _spLeaderBoardB.scale = new Vector2(0.5f, 0.5f);
            _spLeaderBoardB.currentAnim = 0;
            _spLeaderBoardB.slika = true;
            this.Game.Components.Add(_spLeaderBoardB);

            DrawScripts.AnimSprite _spExitB = new DrawScripts.AnimSprite(this.Game);
            _spExitB.texture = this.Game.Content.Load<Texture2D>("Assets\\MainMenu\\Exit");
            _spExitB.position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, this.Game.GraphicsDevice.Viewport.Height / 2);
            _spExitB.animations = new List<Rectangle>();
            _spExitB.animations.Add(new Rectangle(0, 0, _spExitB.texture.Width, _spExitB.texture.Height));
            _spExitB.center = new Vector2(_spExitB.texture.Width / 2, _spExitB.texture.Height / 2);
            _spExitB.scale = new Vector2(0.5f, 0.5f);
            _spExitB.currentAnim = 0;
            _spExitB.slika = true;
            this.Game.Components.Add(_spExitB);


            _UI = new List<DrawScripts.AnimSprite>();
            _UI.Add(_spStartB);
            _UI.Add(_spLeaderBoardB);
            _UI.Add(_spExitB);

            DrawScripts.DrawMenu _draw = new DrawScripts.DrawMenu(this.Game, _UI);
            this.Game.Components.Add(_draw);

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
            base.Update(gameTime);
        }

        void CheckMousePosOnCLick()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //start gumb
                if (mouseState.X <= _UI[0].position.X + (_UI[0].texture.Width / 2) * 0.5f && mouseState.X >= _UI[0].position.X - (_UI[0].texture.Width / 2) * 0.5f && mouseState.Y <= _UI[0].position.Y + (_UI[0].texture.Height / 2) * 0.5f && mouseState.Y >= _UI[0].position.Y - (_UI[0].texture.Height / 2) * 0.5f)
                {
                    this.Game.Components.Clear();
                    Scenes.Level level = new Scenes.Level(this.Game);
                    this.Game.Components.Add(level);
                }

                //Leaderboard gumb
                if (mouseState.X <= _UI[1].position.X + (_UI[1].texture.Width / 2) * 0.5f && mouseState.X >= _UI[1].position.X - (_UI[1].texture.Width / 2) * 0.5f && mouseState.Y <= _UI[1].position.Y + (_UI[1].texture.Height / 2) * 0.5f && mouseState.Y >= _UI[1].position.Y - (_UI[1].texture.Height / 2) * 0.5f)
                {
                    this.Game.Components.Clear();
                    Scenes.LeaderBoard level = new Scenes.LeaderBoard(this.Game);
                    this.Game.Components.Add(level);
                }

                //Exit gumb
                if (mouseState.X <= _UI[2].position.X + (_UI[2].texture.Width / 2) * 0.5f && mouseState.X >= _UI[2].position.X - (_UI[2].texture.Width / 2) * 0.5f && mouseState.Y <= _UI[2].position.Y + (_UI[2].texture.Height / 2) * 0.5f && mouseState.Y >= _UI[2].position.Y - (_UI[2].texture.Height / 2) * 0.5f)
                {
                    this.Game.Exit();
                }
            }
        }
    }
}
