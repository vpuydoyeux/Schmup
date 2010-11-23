using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup.XnaGame.DebugHelpers
{
    /// <summary>
    /// Provides a framerate counter to find out about performance issues
    /// </summary>
    public class FrameRateCounter : DrawableGameComponent
    {
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private NumberFormatInfo format;
        private int frameCounter;
        private int frameRate;
        private Vector2 position;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        /// <summary>
        /// Initialize a new instance of FrameRateCounter
        /// </summary>
        /// <param name="game">The game</param>
        public FrameRateCounter(Game game)
            : base(game)
        {
            format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";
            position = new Vector2(5, 5);
        }

        #region DrawableGameComponent Members

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>("Fonts/FramerateCounter");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime <= TimeSpan.FromSeconds(1))
                return;
            elapsedTime -= TimeSpan.FromSeconds(1);
            frameRate = frameCounter;
            frameCounter = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            frameCounter++;
            string fps = string.Format(format, "{0} fps", frameRate);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, fps, position, Color.Gray);
            spriteBatch.End();
        }

        #endregion
    }
}
