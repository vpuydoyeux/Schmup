using System;
using Schmup.XnaGame.Sprites.Ennemies;

namespace Schmup.XnaGame.Levels
{
    /// <summary>
    /// Item of an ennemy wave
    /// </summary>
    public class EnnemyWaveItem
    {
        /// <summary>
        /// Delay before the ennemy appears
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Ennemy that should appear after the delay
        /// </summary>
        public Ennemy Ennemy { get; set; }

        /// <summary>
        /// Initialize a new instance of EnnemyWaveItem with the specified ennemy and delay
        /// </summary>
        /// <param name="ennemy"></param>
        /// <param name="delay"></param>
        public EnnemyWaveItem(Ennemy ennemy, TimeSpan delay)
        {
            Ennemy = ennemy;
            Delay = delay;
        }
    }
}
