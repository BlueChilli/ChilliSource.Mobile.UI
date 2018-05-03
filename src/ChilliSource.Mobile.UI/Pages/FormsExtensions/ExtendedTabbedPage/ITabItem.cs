using System;
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Contract defining properties of <see cref="TabItem"/>
    /// </summary>
    public interface ITabItem
    {
        string Icon { get; set; }
        string SelectedIcon { get; set; }
        string Title { get; set; }
        string BadgeCount { get; set; }
        bool IsEnabled { get; set; }
    }
}
