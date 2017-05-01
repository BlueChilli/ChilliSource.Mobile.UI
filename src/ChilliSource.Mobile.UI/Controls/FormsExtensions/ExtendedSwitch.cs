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
	public class ExtendedSwitch : Switch
	{
		public static readonly BindableProperty TintColorProperty =
			BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(ExtendedSwitch), default(Color));

		public Color TintColor
		{
			get { return (Color)GetValue(TintColorProperty); }
			set { SetValue(TintColorProperty, value); }
		}
	}
}
