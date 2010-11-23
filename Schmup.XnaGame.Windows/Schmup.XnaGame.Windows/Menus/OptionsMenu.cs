namespace Schmup.XnaGame.Menus
{
    /// <summary>
    /// The options menu of the game
    /// </summary>
    public class OptionsMenu : Menu
    {
        private const int backMenuItemIndex = 0;

        /// <summary>
        /// Initialize a new instance of MainMenu
        /// </summary>
        /// <param name="game">The game</param>
        public OptionsMenu(SchmupGame game)
            : base(game)
        {
            MenuEntry menuEntry;
            menuEntry = new MenuEntry("Back to main menu");
            AddEntry(menuEntry);
            Stop();
        }

        #region Menu Members

        /// <summary>
        /// Defines the behaviour of the menu when a menu item is selected
        /// </summary>
        /// <param name="selectedEntryIndex">Index of the selected menu item (starts at 0)</param>
        protected override void OnSelectedEntry(int selectedEntryIndex)
        {
            if (selectedEntryIndex == backMenuItemIndex)
            {
                SchmupGame.MainMenu.Start();
                Stop();
            }
        }

        #endregion
    }
}
