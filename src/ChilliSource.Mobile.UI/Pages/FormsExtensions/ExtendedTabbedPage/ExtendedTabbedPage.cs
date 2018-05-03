#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies whether the tab items are centered in the tab layout or filling the tab layout. 
    /// Only applicable for <see cref="StyledTabBarLayoutMode.Fixed"/> and currently only available on Android.
    /// </summary>
    public enum StyledTabBarGravity
    {
        Fill,
        Center
    }

    /// <summary>
    /// Specifies whether the tab bar is fixed or scrollable. 
    /// Currently only supported on Android.
    /// </summary>
    public enum StyledTabBarLayoutMode
    {
        Fixed,
        Scrollable
    }

    /// <summary>
    /// Adds additional styling properties to the standard <see cref="TabbedPage"/>
    /// </summary>
    public abstract class ExtendedTabbedPage : TabbedPage
    {
        /// <summary>
        /// Backing store for the <see cref="TabItems"/> bindable property.
        /// </summary>
        public static BindableProperty TabItemsProperty =
            BindableProperty.Create(nameof(TabItems), typeof(IList<ITabItem>), typeof(ExtendedTabbedPage), new ObservableCollection<ITabItem>());

        /// <summary>
        /// Gets or sets the tab items to display. This is a bindable property.
        /// </summary>
        public IList<ITabItem> TabItems
        {
            get { return (IList<ITabItem>)GetValue(TabItemsProperty); }
            set { SetValue(TabItemsProperty, value); }
        }

        /// <summary>
        /// Determines whether this page is opaque or not.
        /// </summary>
        public bool IsOpaque { get; set; }

        /// <summary>
        /// Gets or sets the tint color for the tab bar.
        /// </summary>
        public Color TintColor { get; set; }

        /// <summary>
        /// Gets or sets the tint color of the selected tab item.
        /// </summary>
        public Color SelectedTabItemTintColor { get; set; }

        /// <summary>
        /// Gets or sets the color for the text of the selected tab item. Currently only available for Android.
        /// </summary>
        public Color? SelectedTabItemTextColor { get; set; }

        /// <summary>
        /// Specifies whether the tab bar is fixed or scrollable. 
        /// Currently only supported on Android.
        /// </summary>
        public StyledTabBarLayoutMode TabMode { get; set; }

        /// <summary>
        /// Specifies whether the tab items are centered in the tab layout or filling the tab layout. 
        /// Only applicable for <see cref="StyledTabBarLayoutMode.Fixed"/> and currently only available on Android.
        /// </summary>
        public StyledTabBarGravity Gravity { get; set; }

    }
}
