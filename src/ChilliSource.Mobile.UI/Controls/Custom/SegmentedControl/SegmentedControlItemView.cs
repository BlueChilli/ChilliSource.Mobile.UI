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
    /// Single item of <see cref="SegmentedControlView"/>
    /// </summary>
    public class SegmentedControlItemView : View
    {
        /// <summary>
        /// Initializes a new instance of the <c>SegmentedControlItemView</c> class.
        /// </summary>
        public SegmentedControlItemView()
        {
            SetBinding(TextProperty, new Binding("Text"));
        }

        /// <summary>
        /// Backing store for the <c>Text</c> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlView), string.Empty);

        /// <summary>
        /// Gets or sets the text for the segmented view item.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}
