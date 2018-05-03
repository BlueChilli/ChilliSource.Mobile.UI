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
    /// Styles for the <see cref="AdvancedActionSheet"/> class.
    /// </summary>
    public static class AdvancedActionSheetStyles
    {
        /// <summary>
        /// Defines the standard style for the action buttons.
        /// </summary>
        public static Style StandardButtonStyle = new Style(typeof(ExtendedButton))
        {
            Setters =
            {
                new Setter
                {
                    Property = Button.CornerRadiusProperty,
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

        /// <summary>
        /// Defines the standard style for the label.
        /// </summary>
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
