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
    /// Xamarin Forms Switch extension
    /// </summary>
    public class ExtendedSwitch : Switch
    {
        /// <summary>
        /// Backing store for the <c>TintColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(ExtendedSwitch), default(Color));

        /// <summary>
        /// Gets or sets the tint color of the switch. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the tint color of the switch.</value>
		public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }
    }
}
