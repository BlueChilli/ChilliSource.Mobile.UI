#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Custom iOS-style segmented control
    /// </summary>
    public class SegmentedControlView : View, IViewContainer<SegmentedControlItemView>
    {
        /// <summary>
        /// Initializes a new instance of the <c>SegmentedControlView</c> class.
        /// </summary>
        public SegmentedControlView()
        {
            Children = new List<SegmentedControlItemView>();
        }

        /// <summary>
        /// Gets or sets a list of child items for the segmented view. This is a bindable property.
        /// </summary>
        /// <value>A list of <see cref="SegmentedControlItemView"/> value that represents segmented view's children.</value>
        public IList<SegmentedControlItemView> Children { get; set; }


        /// <summary>
        /// Backing store for the <c>SelectedItem</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
           BindableProperty.Create(nameof(SelectedItem), typeof(int), typeof(SegmentedControlView), 0, BindingMode.TwoWay, null, HandleSelectedItemChanged);

        /// <summary>
        /// Gets or sets the selected item of the segmented view. This is a bindable property.
        /// </summary>
        public int SelectedItem
        {
            get { return (int)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>TintColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(SegmentedControlView), Color.Default);

        /// <summary>
        /// Gets or sets the tint color of the segmented control. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the tint color of the segmented control. 
        /// The default value is <see cref="Color.Default"/>.</value>
        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>SelectedColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(SegmentedControlView), Color.Default);

        /// <summary>
        /// Gets or sets the color of the selected segmented control item.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the selected color of the segmented control.
        ///  The default value is <see cref="Color.Default"/>.</value>
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }


        /// <summary>
        /// Backinf store for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

        /// <summary>
        /// Gets or sets the custom font for the segmented control item.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the text font for the segmented control item.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CustomSelectedFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomSelectedFontProperty =
            BindableProperty.Create(nameof(CustomSelectedFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

        /// <summary>
        /// Gets or sets the custom font for the selected segmented control item.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the text font for the selected segmented control item.</value>
        public ExtendedFont CustomSelectedFont
        {
            get { return (ExtendedFont)GetValue(CustomSelectedFontProperty); }
            set { SetValue(CustomSelectedFontProperty, value); }
        }


        /// <summary>
        /// Occurs when the value of the segmented control is changed.
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// Delegate for the <c>ValueChanged</c> event.
        /// </summary>
        public delegate void ValueChangedEventHandler(object sender, EventArgs e);


        private static void HandleSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SegmentedControlView)bindable;
            control.ValueChanged?.Invoke(control, null);
        }
    }
}

