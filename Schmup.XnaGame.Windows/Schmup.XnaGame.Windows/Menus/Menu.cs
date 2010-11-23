using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Schmup.XnaGame.Common;

namespace Schmup.XnaGame.Menus
{
    /// <summary>
    /// Provides a menu base for the game
    /// </summary>
    public abstract class Menu : SchmupDrawableGameComponent
    {
        /// <summary>
        /// Start drawing menu items from there
        /// </summary>
        public Vector2 MenuTopPosition { get; protected set; }

        private SpriteFont menuFont;
        private List<MenuEntry> menuEntries = new List<MenuEntry>();
        private int selectedEntryIndex = 0;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Initialize a new instance of Menu
        /// </summary>
        /// <param name="game">The game</param>
        public Menu(SchmupGame game)
            : base(game)
        {
            MenuTopPosition = new Vector2(100, 300);
        }

        /// <summary>
        /// Add an entry to the menu
        /// </summary>
        /// <param name="menuEntry">New menu entry</param>
        protected void AddEntry(MenuEntry menuEntry)
        {
            menuEntries.Add(menuEntry);
        }

        /// <summary>
        /// Defines the behaviour of the menu when a menu item is selected
        /// </summary>
        /// <param name="selectedEntryIndex">Index of the selected menu item (starts at 0)</param>
        protected abstract void OnSelectedEntry(int selectedEntryIndex);

        #region SchmupDrawableGameComponent Members

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            menuFont = Game.Content.Load<SpriteFont>("Fonts/Menu");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            InputState input = SchmupGame.InputState;
            if (input.MenuUp)
            {
                selectedEntryIndex--;
                if (selectedEntryIndex < 0)
                    selectedEntryIndex = menuEntries.Count - 1;
            }
            if (input.MenuDown)
            {
                selectedEntryIndex++;
                if (selectedEntryIndex >= menuEntries.Count)
                    selectedEntryIndex = 0;
            }
            if (input.MenuSelect)
            {
                OnSelectedEntry(selectedEntryIndex);
                // If the input is not updated, the first item of the following menu is immediately selected
                input.Update();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 position = MenuTopPosition;
            int delta = menuFont.LineSpacing;
            spriteBatch.Begin();
            int i = 0;
            foreach (MenuEntry menuEntry in menuEntries)
            {
                Color color = Color.White;
                if (i == selectedEntryIndex)
                    color = Color.Red;
                spriteBatch.DrawString(menuFont, menuEntry.Title, position, color);
                position.Y += delta;
                ++i;
            }
            spriteBatch.End();
        }

        #endregion
    }
}
