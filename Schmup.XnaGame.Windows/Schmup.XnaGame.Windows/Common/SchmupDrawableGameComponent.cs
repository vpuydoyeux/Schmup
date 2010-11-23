using Microsoft.Xna.Framework;

namespace Schmup.XnaGame.Common
{
    /// <summary>
    /// Provides a more complete DrawableGameComponent for the schmup game
    /// </summary>
    public abstract class SchmupDrawableGameComponent : DrawableGameComponent
    {
        /// <summary>
        /// Provides easy access to SchmupGame properties
        /// </summary>
        public SchmupGame SchmupGame { get; private set; }

        public SchmupDrawableGameComponent(SchmupGame game)
            : base(game)
        {
            SchmupGame = game;
        }

        /// <summary>
        /// Start the updating and the drawing of the component
        /// </summary>
        public void Start()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Stop the updating and the drawing of the component
        /// </summary>
        public void Stop()
        {
            this.Enabled = false;
            this.Visible = false;
        }
    }
}
