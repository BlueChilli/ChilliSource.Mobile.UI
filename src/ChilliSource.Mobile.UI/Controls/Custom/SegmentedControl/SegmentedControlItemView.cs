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
	public class SegmentedControlItemView : View
	{
		public SegmentedControlItemView()
		{
			this.SetBinding(TextProperty, new Binding("Text"));
		}

		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlView), string.Empty);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

	}
}
