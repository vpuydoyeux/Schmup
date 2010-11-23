namespace Schmup.XnaGame.Menus
{
    /// <summary>
    /// Menu entry for the Menu component
    /// </summary>
    public class MenuEntry
    {
        /// <summary>
        /// Title of the menu entry
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Initialize a new instance of MenuEntry with the specified title
        /// </summary>
        /// <param name="title">Title of the menu entry</param>
        public MenuEntry(string title)
        {
            Title = title;
        }
    }
}
