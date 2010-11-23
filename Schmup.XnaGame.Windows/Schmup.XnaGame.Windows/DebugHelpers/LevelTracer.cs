using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Levels;

namespace Schmup.XnaGame.DebugHelpers
{
    /// <summary>
    /// Provides tracing informations about the level
    /// </summary>
    public class LevelTracer : DrawableGameComponent
    {
        private TimeSpan elapsedTime;
        private SpriteFont font;
        private Level level;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initialize a new instance of LevelTracer associated to a level
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="level">The level to trace</param>
        public LevelTracer(Game game, Level level)
            : base(game)
        {
            this.level = level;
        }

        #region DrawableGameComponent

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Fonts/LevelTracer");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            elapsedTime = gameTime.TotalGameTime.Subtract(level.StartTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 position = new Vector2(0, Game.GraphicsDevice.Viewport.Height - font.LineSpacing);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Ship's bullets : " + level.ShipBulletsCount, position, Color.Gray);
            position.Y -= font.LineSpacing;
            spriteBatch.DrawString(font, "Ennemies : " + level.EnnemiesCount, position, Color.Gray);
            position.Y -= font.LineSpacing;
            spriteBatch.DrawString(font, elapsedTime.ToString(), position, Color.Gray);
            spriteBatch.End();
        }

        #endregion
    }
}
