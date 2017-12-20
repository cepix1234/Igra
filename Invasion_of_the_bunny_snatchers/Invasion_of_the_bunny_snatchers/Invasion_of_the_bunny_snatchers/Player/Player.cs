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


namespace Invasion_of_the_bunny_snatchers.Player
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Microsoft.Xna.Framework.GameComponent
    {
        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }
        #region Members
        public DrawScripts.AnimSprite body { get; set; }
        public DrawScripts.AnimSprite legs { get; set; }
        public Vector2 position { get; set; }
        public int speed { get; set; }
        public float shootSpeed { get; set; }
        public Boolean multipleBoolets { get; set; }
        public int helth { get; set; }
        public Vector2 legsOffset { get; set; }
        public SoundEffect shoot1 { get; set; }
        public SoundEffect shoot2 { get; set; }

        #endregion
    }
}
