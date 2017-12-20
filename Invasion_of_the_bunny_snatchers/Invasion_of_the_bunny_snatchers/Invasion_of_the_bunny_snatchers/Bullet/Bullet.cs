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


namespace Invasion_of_the_bunny_snatchers.Bullet
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bullet : Microsoft.Xna.Framework.GameComponent
    {
        public Bullet(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        #region Members
        public DrawScripts.AnimSprite body { get; set; }
        public Vector2 position { get; set; }

        //za bullet
        public Vector2 direction { get; set; }
        public float rotation { get; set; }
        public Boolean player { get; set; }
        #endregion
    }
}
