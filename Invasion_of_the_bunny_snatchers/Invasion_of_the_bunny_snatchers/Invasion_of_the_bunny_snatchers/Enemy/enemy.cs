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


namespace Invasion_of_the_bunny_snatchers.Enemy
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class enemy : Microsoft.Xna.Framework.GameComponent
    {
        public enemy(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        #region Members
        public DrawScripts.AnimSprite body;
        public Vector2 position { get; set; }
        public int frameEnemy { get; set; }
        public float shootSpeed { get; set; }
        public Boolean attack { get; set; }
        public int enemyType { get; set; }
        public float lastFire { get; set; }
        public float rotation { get; set; }
        public SpriteEffects orientacija { get; set; }
        public float lastvoice { get; set; }
        public SoundEffect voice { get; set; }
        public SoundEffect die { get; set; }
        public SoundEffect shoot1 { get; set; }
        public SoundEffect shoot2 { get; set; }
        #endregion
    }
}
