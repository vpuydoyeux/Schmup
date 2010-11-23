using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Common;
using Schmup.XnaGame.DebugHelpers;
using Schmup.XnaGame.Levels;
using Schmup.XnaGame.Menus;

namespace Schmup.XnaGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SchmupGame : Game
    {
        /// <summary>
        /// Input state of the game
        /// </summary>
        public InputState InputState { get; private set; }

        /// <summary>
        /// Current level
        /// </summary>
        public Level Level { get; private set; }

        /// <summary>
        /// The main menu
        /// </summary>
        public MainMenu MainMenu { get; private set; }

        /// <summary>
        /// The options menu
        /// </summary>
        public OptionsMenu OptionsMenu { get; private set; }

        /// <summary>
        /// The score manager
        /// </summary>
        public ScoreManager ScoreManager { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initialize a new instance of SchmupGame starting with the main menu
        /// </summary>
        public SchmupGame()
        {
            Window.Title = "Schmup";
            Window.AllowUserResizing = false;
            IsMouseVisible = false;

            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;

            // Set the framerate at 62.5 fps (i.e. about 60 fps)
            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 16);

            Content.RootDirectory = "Content";

            InputState = new InputState();

            MainMenu = new MainMenu(this);
            Components.Add(MainMenu);
            OptionsMenu = new OptionsMenu(this);
            Components.Add(OptionsMenu);

            ScoreManager = new ScoreManager(this);
            Components.Add(ScoreManager);

            Level = new Level1(this);
            Components.Add(Level);

#if DEBUG
            FrameRateCounter frameRateCounter = new FrameRateCounter(this);
            frameRateCounter.DrawOrder = 101;
            Components.Add(frameRateCounter);
            ComponentsTracer componentsTracer = new ComponentsTracer(this);
            componentsTracer.DrawOrder = 102;
            componentsTracer.AddComponent("Main Menu", MainMenu);
            componentsTracer.AddComponent("Options Menu", OptionsMenu);
            componentsTracer.AddComponent("Score Manager", ScoreManager);
            componentsTracer.AddComponent("Level", Level);
            Components.Add(componentsTracer);
#endif
        }

        #region Game Members

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputState.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        #endregion
    }
}
