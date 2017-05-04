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
	public static class AdvancedActionSheetStyles
	{
		public static Style StandardButtonStyle = new Style(typeof(ExtendedButton))
		{
			Setters =
			{
				new Setter
				{
					Property = Button.BorderRadiusProperty,
					Value = 0,
				},

				new Setter
				{
					Property = VisualElement.HeightRequestProperty,
					Value = 50,
				},

				new Setter
				{
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.White,
				},
			}
		};

		public static Style StandardLabelStyle = new Style(typeof(ExtendedLabel))
		{
			Setters =
			{
				new Setter
				{
					Property = Label.HorizontalTextAlignmentProperty,
					Value = TextAlignment.Center,
				},

				new Setter
				{
					Property = Label.VerticalTextAlignmentProperty,
					Value = TextAlignment.Center,
				},

				new Setter
				{
					Property = VisualElement.BackgroundColorProperty,
					Value = Color.White,
				},
			}
		};
	}
}
