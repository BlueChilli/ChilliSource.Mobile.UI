#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// The indivial alert buttons that are used with the preset alert type
	/// </summary>
	public class AdvancedAlertViewButton : ExtendedButton
	{
		public static AdvancedAlertViewButton CreateButton(string text, ExtendedFont font, Color backgroundColor, Command command = null)
		{
			var button = new AdvancedAlertViewButton()
			{
				Text = text,
				CustomFont = font,
				BackgroundColor = backgroundColor
			};

			if (command != null) button.Command = command;

			return button;
		}

		public static Style ButtonStyle = new Style(typeof(ExtendedButton))
		{
			Setters =
			{
				new Setter
				{
					Property = VisualElement.HeightRequestProperty,
					Value = 40,
				},

				new Setter
				{
					Property = ExtendedButton.HorizontalContentAlignmentProperty,
					Value = ButtonHorizontalContentAlignment.Center,
				},
			}
		};
	}
}
