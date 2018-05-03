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
    /// Custom button for <see cref="AdvancedAlertView"/>
    /// </summary>
    public class AdvancedAlertViewButton : ExtendedButton
    {
        /// <summary>
        /// Creates the button for the alert view.
        /// </summary>
        /// <returns>An <c>AdvancedAlertViewButton</c>.</returns>
        /// <param name="text">Text.</param>
        /// <param name="font">Font.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="command">Command.</param>
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


        /// <summary>
        /// Defines the style of the button.
        /// </summary>
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
