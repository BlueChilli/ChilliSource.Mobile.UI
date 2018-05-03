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
    /// Xamarin Forms WebView extension
    /// </summary>
    public class ExtendedWebView : WebView
    {
        /// <summary>
        /// Identifies the <c>IsTransparent</c> bindable property.
        /// </summary>
        public static readonly BindableProperty IsTransparentProperty =
            BindableProperty.Create(nameof(IsTransparent), typeof(bool), typeof(ExtendedWebView), false);

        /// <summary>
        /// Gets or sets a value indicating whether the web view is transparent.
        /// </summary>
        /// <value><c>true</c> if the web view's background is transparent; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool IsTransparent
        {
            get { return (bool)GetValue(IsTransparentProperty); }
            set { SetValue(IsTransparentProperty, value); }
        }

    }
}

