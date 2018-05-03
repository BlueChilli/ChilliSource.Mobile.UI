#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Xamarin Forms SearchBar extension
    /// </summary>
    public class ExtendedSearchBar : SearchBar
    {
        /// <summary>
        /// Backing store for the <c>BarTint</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BarTintProperty =
            BindableProperty.Create(nameof(BarTint), typeof(Color?), typeof(ExtendedSearchBar), Color.Blue);

        /// <summary>
        /// Gets or sets the tint color for the search bar. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the tint color of the search bar. The default value is <see cref="Color.Blue"/>.</value>
        public Color BarTint
        {
            get { return (Color)GetValue(BarTintProperty); }
            set { SetValue(BarTintProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>SearchStyle</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SearchStyleProperty =
            BindableProperty.Create(nameof(SearchStyle), typeof(string), typeof(ExtendedSearchBar), "Default");

        /// <summary>
        /// Gets or sets the prominence of the search bar. This is a bindable property.
        /// </summary>
        /// <value>A <c>string</c> that represents a <see cref="UIKit.UISearchBarStyle"/> value for the prominence of the search bar.
        /// The default value is <see cref="UIKit.UIBarStyle.Default"/>.</value>
        public string SearchStyle
        {
            get { return (string)GetValue(SearchStyleProperty); }
            set { SetValue(SearchStyleProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>BarStyle</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BarStyleProperty =
            BindableProperty.Create(nameof(BarStyle), typeof(string), typeof(ExtendedSearchBar), "Default");

        /// <summary>
        ///Gets or sets the visual style of the search toolbar. This is a bindable property.
        /// </summary>
        /// <value>A <c>string</c> that represnets a <see cref="UIKit.UIBarStyle"/> value for the appearance of the search bar.
        ///  The default value is <see cref="UIKit.UIBarStyle.Default"/>.</value>
        public string BarStyle
        {
            get { return (string)GetValue(BarStyleProperty); }
            set { SetValue(BarStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the <c>PersistCancelButton</c> bindable property.
        /// </summary>
        public static readonly BindableProperty PersistCancelButtonProperty =
            BindableProperty.Create(nameof(PersistCancelButton), typeof(bool), typeof(ExtendedSearchBar), false);

        /// <summary>
        /// Gets or sets a value indicating whether the cancel button of the search bar is always displayed. This is a bindable property
        /// </summary>
        /// <value><c>true</c> if the cancel button should be visible at all time; otherwise, <c>false</c>. 
        /// The default value is <c>false</c>.</value>
        public bool PersistCancelButton
        {
            get { return (bool)GetValue(PersistCancelButtonProperty); }
            set { SetValue(PersistCancelButtonProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>KeyboardTheme</c> bindable property.
        /// </summary>
        public static readonly BindableProperty KeyboardThemeProperty =
            BindableProperty.Create(nameof(KeyboardTheme), typeof(KeyboardTheme), typeof(ExtendedEntry), KeyboardTheme.Light);

        /// <summary>
        /// Gets or sets the theme of the keyboard. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="UI.KeyboardTheme"/> value that represents the keyboard theme. 
        /// The default value is <see cref="UI.KeyboardTheme.Light"/>.</value>
        public KeyboardTheme KeyboardTheme
        {
            get { return (KeyboardTheme)GetValue(KeyboardThemeProperty); }
            set { SetValue(KeyboardThemeProperty, value); }
        }

        /// <summary>
        /// Occurs when cancel button is pressed.
        /// </summary>
		public event EventHandler<EventArgs> CancelButtonPressed;

        /// <summary>
        /// Method that raises the <c>CancelButtonPressed</c> event.
        /// </summary>
        public void OnCancelButtonClicked()
        {
            CancelButtonPressed?.Invoke(this, new EventArgs());
        }

    }
}


