using System;
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Holds styling properties for tab items to be used with <see cref="ExtendedTabbedPage"/>
    /// </summary>
    public class TabItem : ITabItem
    {
        public TabItem()
        {
            IsEnabled = true;
        }

        public TabItem(string title, string icon, string selectedIcon)
        {
            IsEnabled = true;
            Title = title;
            Icon = icon;
            SelectedIcon = selectedIcon;
        }

        /// <summary>
        /// Gets or sets the icon for the tab item.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the icon for the tab item when it is selected.
        /// </summary>
        public string SelectedIcon { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the badge count for the item.
        /// </summary>
        public string BadgeCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this tab item is enabled.
        /// </summary>
        /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled { get; set; }
    }
}
