using Microsoft.Xna.Framework;

namespace Schmup.XnaGame.Sprites.Ennemies
{
    /// <summary>
    /// Provide a base class for all ennemies
    /// </summary>
    public abstract class Ennemy : Sprite
    {
        /// <summary>
        /// Remaining energy
        /// </summary>
        public int Energy { get; set; }

        /// <summary>
        /// Default / Max energy
        /// </summary>
        public abstract int MaxEnergy { get; }

        /// <summary>
        /// Points earned when the ship is destroyed
        /// </summary>
        public abstract int Points { get; }

        /// <summary>
        /// Initialize a new instance of Ennemy with the default position and velocity
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="position">Default position</param>
        /// <param name="velocity">Default velocity</param>
        public Ennemy(SchmupGame game, Vector2 position, Vector2 velocity)
            : base(game, position)
        {
            Energy = MaxEnergy;
            Velocity = velocity;
        }
    }
}
