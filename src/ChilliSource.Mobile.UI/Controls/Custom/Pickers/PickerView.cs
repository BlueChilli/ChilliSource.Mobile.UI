#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// iOS-stlye generic picker view
    /// </summary>
    public class PickerView : View
    {
        /// <summary>
        /// Backing store for the <c>ItemsSource</c> bindable property.
        /// </summary>
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList<IList<string>>), typeof(PickerView), default(IList));

        /// <summary>
        /// Gets or sets the source list of items to template and display in the picker view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="IList"/> representing the source of the view.</value>
        public IList<IList<string>> ItemsSource
        {
            get { return (IList<IList<string>>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>SelectedItems</c> bindable property.
        /// </summary>
        public static BindableProperty SelectedItemsProperty =
            BindableProperty.Create(nameof(SelectedItems), typeof(IList<string>), typeof(PickerView), new List<string>());

        /// <summary>
        /// Gets or sets the list of the selected items for the view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="IList"/> that represents the selected items of the picker.</value>
        public IList<string> SelectedItems
        {
            get { return (IList<string>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ComponentFixedTextItems</c> bindable property.
        /// </summary>
        public static BindableProperty ComponentFixedTextItemsProperty =
            BindableProperty.Create(nameof(ComponentFixedTextItems), typeof(List<string>), typeof(PickerView), new List<string>());

        /// <summary>
        /// Gets or sets a list of text to display next to the selected item. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="System.Collections.Generic.List{T}"/> representing a list of text to display next to the selected item.</value>
        public List<string> ComponentFixedTextItems
        {
            get { return (List<string>)GetValue(ComponentFixedTextItemsProperty); }
            set { SetValue(ComponentFixedTextItemsProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(PickerView), null);

        /// <summary>
        /// Gets or sets the custom font for the items of the picker view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the items on the list.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CustomSelectedFont</c> bindable property. 
        /// </summary>
        public static readonly BindableProperty CustomSelectedFontProperty =
            BindableProperty.Create(nameof(CustomSelectedFont), typeof(ExtendedFont), typeof(PickerView), null);

        /// <summary>
        ///  Gets or sets the custom font for the selected item. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the selected item.</value>
        public ExtendedFont CustomSelectedFont
        {
            get { return (ExtendedFont)GetValue(CustomSelectedFontProperty); }
            set { SetValue(CustomSelectedFontProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>RowHeight</c> bindable property.
        /// </summary>
        public static readonly BindableProperty RowHeightProperty =
            BindableProperty.Create(nameof(RowHeight), typeof(float), typeof(PickerView), 40.0f);

        /// <summary>
        /// Gets or sets the height of the picker's row. This is a bindable property.
        /// </summary>
        /// <value>The height of the row; the default is 40.</value>
        public float RowHeight
        {
            get { return (float)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }
    }
}

