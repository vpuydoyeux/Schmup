using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup.XnaGame.DebugHelpers
{
    /// <summary>
    /// Provides tracing informations of the Enabled and Visible properties of game components
    /// </summary>
    public class ComponentsTracer : DrawableGameComponent
    {
        private Vector2 bottomPosition;
        private Dictionary<string, GameComponent> gameComponents = new Dictionary<string, GameComponent>();
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private List<string> statesList = new List<string>();

        /// <summary>
        /// Initialize a new instance of ComponentsTracer
        /// </summary>
        /// <param name="game">The game</param>
        public ComponentsTracer(SchmupGame game)
            : base(game)
        {
        }

        /// <summary>
        /// Adds a component to trace
        /// </summary>
        /// <param name="title">Display name of the component</param>
        /// <param name="component">Component to trace</param>
        public void AddComponent(string title, GameComponent component)
        {
            gameComponents.Add(title, component);
        }

        #region DrawableGameComponent Members

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>("Fonts/ComponentsTracer");
            bottomPosition = new Vector2(Game.Window.ClientBounds.Width - 200,
                                         Game.Window.ClientBounds.Height - spriteFont.LineSpacing);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            statesList.Clear();
            foreach (var item in gameComponents)
            {
                bool enabled = item.Value.Enabled;
                bool visible = false;
                DrawableGameComponent drawable = item.Value as DrawableGameComponent;
                if (drawable != null && drawable.Visible)
                    visible = true;
                statesList.Add((enabled ? "E" : " ") + "|" + (visible ? "V" : " ") + " " + item.Key);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 position = bottomPosition;
            spriteBatch.Begin();
            foreach (var state in statesList)
            {
                spriteBatch.DrawString(spriteFont, state, position, Color.Gray);
                position.Y -= spriteFont.LineSpacing;
            }
            spriteBatch.End();
        }

        #endregion
    }
}
