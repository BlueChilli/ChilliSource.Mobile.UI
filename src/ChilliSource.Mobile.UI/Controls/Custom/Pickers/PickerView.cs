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
	public class PickerView : View
	{
		public PickerView()
		{
		}

		public static BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IList<IList<string>>), typeof(PickerView), default(IList));

		public IList<IList<string>> ItemsSource
		{
			get { return (IList<IList<string>>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public static BindableProperty SelectedItemsProperty =
			BindableProperty.Create(nameof(SelectedItems), typeof(IList<string>), typeof(PickerView), new List<string>());

		public IList<string> SelectedItems
		{
			get { return (IList<string>)GetValue(SelectedItemsProperty); }
			set { SetValue(SelectedItemsProperty, value); }
		}

		public static BindableProperty ComponentFixedTextItemsProperty =
			BindableProperty.Create(nameof(ComponentFixedTextItems), typeof(List<string>), typeof(PickerView), new List<string>());

		public List<string> ComponentFixedTextItems
		{
			get { return (List<string>)GetValue(ComponentFixedTextItemsProperty); }
			set { SetValue(ComponentFixedTextItemsProperty, value); }
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(PickerView), null);

		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty CustomSelectedFontProperty =
			BindableProperty.Create(nameof(CustomSelectedFont), typeof(ExtendedFont), typeof(PickerView), null);

		public ExtendedFont CustomSelectedFont
		{
			get { return (ExtendedFont)GetValue(CustomSelectedFontProperty); }
			set { SetValue(CustomSelectedFontProperty, value); }
		}

		public static readonly BindableProperty RowHeightProperty =
			BindableProperty.Create(nameof(RowHeight), typeof(float), typeof(PickerView), 40.0f);

		public float RowHeight
		{
			get { return (float)GetValue(RowHeightProperty); }
			set { SetValue(RowHeightProperty, value); }
		}
	}
}

