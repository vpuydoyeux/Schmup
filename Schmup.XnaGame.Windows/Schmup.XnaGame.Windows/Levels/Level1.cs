using System;
using Microsoft.Xna.Framework;
using Schmup.XnaGame.Sprites.Ennemies;

namespace Schmup.XnaGame.Levels
{
    /// <summary>
    /// The first level of the game !
    /// </summary>
    public class Level1 : Level
    {
        private TimeSpan old = TimeSpan.MinValue;

        /// <summary>
        /// Initialize an instance of Level1
        /// </summary>
        /// <param name="game">The game</param>
        public Level1(SchmupGame game)
            : base(game)
        {
            EnnemyWave wave;
            wave = new EnnemyWave(new TimeSpan(0, 0, 3));
            wave.EnnemyWaveItems.Add(new EnnemyWaveItem(new DarkDrone(game, new Vector2(0, 0), new Vector2(0, 5)), new TimeSpan(0, 0, 0)));
            wave.EnnemyWaveItems.Add(new EnnemyWaveItem(new DarkDrone(game, new Vector2(0, 0), new Vector2(0, 5)), new TimeSpan(0, 0, 1)));
            wave.EnnemyWaveItems.Add(new EnnemyWaveItem(new DarkDrone(game, new Vector2(0, 0), new Vector2(0, 5)), new TimeSpan(0, 0, 2)));
            EnnemyWaves.Add(wave);
        }
    }
}
