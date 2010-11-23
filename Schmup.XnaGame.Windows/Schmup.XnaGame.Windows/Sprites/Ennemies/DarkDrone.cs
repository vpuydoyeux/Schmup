using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup.XnaGame.Sprites.Ennemies
{
    /// <summary>
    /// A dark drone. The weakest ennemy of the game.
    /// </summary>
    public class DarkDrone : Ennemy
    {
        /// <summary>
        /// Default / Max energy
        /// </summary>
        public override int MaxEnergy
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Points earned when the ship is destroyed
        /// </summary>
        public override int Points
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// Initialize a new instance of DarkDrone with the default position and velocity
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="position">Default position</param>
        /// <param name="velocity">Default velocity</param>
        public DarkDrone(SchmupGame game, Vector2 position, Vector2 velocity)
            : base(game, position, velocity)
        {

        }

        #region Ennemy Members

        /// <summary>
        /// Loads the sprite (especially the texture)
        /// </summary>
        public override void Load()
        {
            Texture = Game.Content.Load<Texture2D>("Textures/Game/Ennemies/Default");
        }

        #endregion
    }
}
