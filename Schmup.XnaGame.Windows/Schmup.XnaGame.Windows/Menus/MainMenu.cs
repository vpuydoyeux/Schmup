using Microsoft.Xna.Framework;

namespace Schmup.XnaGame.Menus
{
    /// <summary>
    /// The main menu of the game
    /// </summary>
    public class MainMenu : Menu
    {
        private const int startMenuItemIndex = 0;
        private const int optionsMenuItemIndex = 1;
        private const int exitMenuItemIndex = 2;

        /// <summary>
        /// Initialize a new instance of MainMenu
        /// </summary>
        /// <param name="game">The game</param>
        public MainMenu(SchmupGame game)
            : base(game)
        {
            MenuEntry menuEntry;
            menuEntry = new MenuEntry("Start");
            AddEntry(menuEntry);
            menuEntry = new MenuEntry("Options");
            AddEntry(menuEntry);
            menuEntry = new MenuEntry("Exit");
            AddEntry(menuEntry);
        }

        #region Menu Members

        /// <summary>
        /// Defines the behaviour of the menu when a menu item is selected
        /// </summary>
        /// <param name="selectedEntryIndex">Index of the selected menu item (starts at 0)</param>
        protected override void OnSelectedEntry(int selectedEntryIndex)
        {
            switch (selectedEntryIndex)
            {
                case startMenuItemIndex:
                    SchmupGame.ScoreManager.Start();
                    SchmupGame.Level.Start();
                    Stop();
                    break;
                case optionsMenuItemIndex:
                    SchmupGame.OptionsMenu.Start();
                    Stop();
                    break;
                case exitMenuItemIndex:
                    Game.Exit();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
