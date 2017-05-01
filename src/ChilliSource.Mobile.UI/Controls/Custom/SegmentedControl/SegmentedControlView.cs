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
	public class SegmentedControlView : View, IViewContainer<SegmentedControlItemView>
	{

		public SegmentedControlView()
		{
			Children = new List<SegmentedControlItemView>();
		}

		public IList<SegmentedControlItemView> Children { get; set; }

		public event ValueChangedEventHandler ValueChanged;

		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem),
									typeof(int),
									typeof(SegmentedControlView),
									0, BindingMode.TwoWay,
									null, HandleSelectedItemChanged);


		private static void HandleSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (SegmentedControlView)bindable;
			if (control.ValueChanged != null)
			{
				control.ValueChanged(control, null);
			}
		}

		public int SelectedItem
		{
			get { return (int)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public delegate void ValueChangedEventHandler(object sender, EventArgs e);


		public static readonly BindableProperty TintColorProperty =
			BindableProperty.Create(nameof(TintColor),
									typeof(Color),
									typeof(SegmentedControlView), Color.Default);

		public Color TintColor
		{
			get { return (Color)GetValue(TintColorProperty); }
			set { SetValue(TintColorProperty, value); }
		}

		public static readonly BindableProperty SelectedColorProperty =
			BindableProperty.Create(nameof(SelectedColor),
									typeof(Color),
									typeof(SegmentedControlView), Color.Default);

		public Color SelectedColor
		{
			get { return (Color)GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}



		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty CustomSelectedFontProperty =
			BindableProperty.Create(nameof(CustomSelectedFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

		public ExtendedFont CustomSelectedFont
		{
			get { return (ExtendedFont)GetValue(CustomSelectedFontProperty); }
			set { SetValue(CustomSelectedFontProperty, value); }
		}
	}
}

