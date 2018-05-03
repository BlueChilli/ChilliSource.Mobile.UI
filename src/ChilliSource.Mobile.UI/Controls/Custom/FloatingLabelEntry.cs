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
    public class FloatingLabelEntry : ExtendedEntry
    {
        /// <summary>
        /// Backing store for the <c>FloatingLabelCustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty FloatingLabelCustomFontProperty =
            BindableProperty.Create(nameof(FloatingLabelCustomFont), typeof(ExtendedFont), typeof(FloatingLabelEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the floating label. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the floating label's text. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont FloatingLabelCustomFont
        {
            get { return (ExtendedFont)GetValue(FloatingLabelCustomFontProperty); }
            set { SetValue(FloatingLabelCustomFontProperty, value); }
        }
    }
}
