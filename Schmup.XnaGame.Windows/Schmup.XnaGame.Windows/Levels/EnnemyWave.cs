using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Schmup.XnaGame.Sprites.Ennemies;

namespace Schmup.XnaGame.Levels
{
    /// <summary>
    /// Provides a way to manage ennemy waves in a Shmup
    /// </summary>
    public class EnnemyWave
    {
        /// <summary>
        /// Delay before the wave begins
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// Ennemies released by the wave
        /// </summary>
        public List<EnnemyWaveItem> EnnemyWaveItems { get; set; }

        /// <summary>
        /// Value of the total game time when the wave began
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Initialize a new instance of EnnemyWave that is empty and begins after the specified delay
        /// </summary>
        /// <param name="delay">Delay before the wave begins</param>
        public EnnemyWave(TimeSpan delay)
        {
            Delay = delay;
            EnnemyWaveItems = new List<EnnemyWaveItem>();
        }

        /// <summary>
        /// Get the ennemies that should appear now
        /// </summary>
        /// <param name="gameTime">GameTime parameter of the Update method of GameComponent</param>
        /// <returns>Ennemies that should appear</returns>
        public IEnumerable<Ennemy> GetNewEnnemies(GameTime gameTime)
        {
            for (int i = 0; i < EnnemyWaveItems.Count; i++)
            {
                EnnemyWaveItem ennemyWaveItem = EnnemyWaveItems[i];
                if (gameTime.TotalGameTime.Subtract(StartTime) < ennemyWaveItem.Delay)
                    break;
                EnnemyWaveItems.RemoveAt(i);
                --i;
                yield return ennemyWaveItem.Ennemy;
            }
        }
    }
}
