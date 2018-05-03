#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Page that encapsulates navigation bar styling properties
    /// </summary>
	public abstract class StyledNavigationBarPage : ContentPage
    {
        private bool _isTransparentNavBar;

        /// <summary>
        /// Gets or sets the title font.
        /// </summary>
        public virtual ExtendedFont TitleFont { get; set; }

        /// <summary>
        /// Gets or sets the title font to be used when a subtitle is not specified.
        /// </summary>
        public virtual ExtendedFont TitleOnlyFont { get; set; }

        /// <summary>
        /// Backing store for the <see cref="Subtitle"/> bindable property.
        /// </summary>
        public static BindableProperty SubtitleProperty =
            BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(StyledNavigationBarPage));

        /// <summary>
        /// Text to display as a subtitle below the main title in the middle of the navigation bar. This is a bindable property.
        /// </summary>
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the subtitle font for the navigation bar.
        /// </summary>
        public virtual ExtendedFont SubtitleFont { get; set; }

        /// <summary>
        /// Gets or sets the tint color of the navigation bar.
        /// </summary>
        public virtual Color BarTintColor { get; set; }

        /// <summary>
        /// Determines whether the navigation bar is transparent.
        /// </summary>
        public bool IsTransparentNavBar
        {
            get { return _isTransparentNavBar; }
            set
            {
                _isTransparentNavBar = value;
                NavigationPage.SetHasNavigationBar(this, !_isTransparentNavBar);
            }
        }

        /// <summary>
        /// Determines whether the navigation bar's back button should be hidden.
        /// </summary>
        public bool HideBackButton { get; set; }

        /// <summary>
        /// Backing store for the <see cref="RightToolbarItemFont"/> bindable property.
        /// </summary>
        public static BindableProperty RightToolbarItemFontProperty =
            BindableProperty.Create(nameof(RightToolbarItemFont), typeof(ExtendedFont), typeof(StyledNavigationBarPage));

        /// <summary>
        /// Gets or sets the font for the right toolbar item. This is a bindable property.
        /// </summary>
        public ExtendedFont RightToolbarItemFont
        {
            get { return (ExtendedFont)GetValue(RightToolbarItemFontProperty); }
            set { SetValue(RightToolbarItemFontProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="RightToolbarItemVisible"/> bindable property.
        /// </summary>
        public static BindableProperty RightToolbarItemVisibleProperty =
            BindableProperty.Create(nameof(RightToolbarItemVisible), typeof(bool), typeof(StyledNavigationBarPage), defaultValue: true);

        /// <summary>
        /// Gets or sets a value indicating whether the right toolbar item is visible. This is a bindable property.
        /// </summary>
        public bool RightToolbarItemVisible
        {
            get { return (bool)GetValue(RightToolbarItemVisibleProperty); }
            set { SetValue(RightToolbarItemVisibleProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="LeftToolbarItemFont"/> bindable property.
        /// </summary>
        public static BindableProperty LeftToolbarItemFontProperty =
            BindableProperty.Create(nameof(LeftToolbarItemFont), typeof(ExtendedFont), typeof(StyledNavigationBarPage));

        /// <summary>
        /// Gets or sets the font for the left toolbar item. This is a bindable property.
        /// </summary>
        public ExtendedFont LeftToolbarItemFont
        {
            get { return (ExtendedFont)GetValue(LeftToolbarItemFontProperty); }
            set { SetValue(LeftToolbarItemFontProperty, value); }
        }

        /// <summary>
        ///  Identifies the <see cref="LeftToolbarItemVisible"/> bindable property.
        /// </summary>
        public static BindableProperty LeftToolbarItemVisibleProperty =
            BindableProperty.Create(nameof(LeftToolbarItemVisible), typeof(bool), typeof(StyledNavigationBarPage), defaultValue: true);

        /// <summary>
        /// Gets or sets a value indicating whether the left toolbar item is visible. This is a bindable property.
        /// </summary>
        public bool LeftToolbarItemVisible
        {
            get { return (bool)GetValue(LeftToolbarItemVisibleProperty); }
            set { SetValue(LeftToolbarItemVisibleProperty, value); }
        }
    }
}

