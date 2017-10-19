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


namespace Invasion_of_the_bunny_snatchers.DrawScripts
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AnimSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {

        SpriteBatch _spriteBatch;
        public AnimSprite(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        #region Members
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public List<Rectangle> animations { get; set; }
        public Vector2 scale { get; set; }
        public Vector2 center { get; set; }
        public int currentAnim { get; set; }
        public Boolean slika { get; set; }

        //za tekst
        public String text { get; set; }
        public SpriteFont font { get; set; }
        public int helth { get; set; }
        public bool helthBar { get; set; }


        #endregion

    }
}
