#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Individual action or button to display in the <see cref="AdvancedActionSheet"/>.
    /// </summary>
    public class AdvancedActionSheetAction
    {
        /// <summary>
        /// Creates the action for the action sheet.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="font">Font.</param>
        /// <param name="command">Command.</param>
        /// <param name="type">Type of the action.</param>
        public static AdvancedActionSheetAction CreateAction(string title, ExtendedFont font, Command command, ActionType type = ActionType.Default)
        {
            return new AdvancedActionSheetAction()
            {
                Title = title,
                Font = font,
                Command = command,
                ActionType = type
            };
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the font.</value>
        public ExtendedFont Font
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the command for the button.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represent the button's command.</value>
        public ICommand Command
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>A <c>ActionType</c> that represents the type of the action.</value>
        public ActionType ActionType
        {
            get; set;
        }
    }

    /// <summary>
    /// Specifies the type of the action.
    /// </summary>
    public enum ActionType
    {
        Default,
        Cancel
    }
}
