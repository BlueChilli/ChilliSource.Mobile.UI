#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Contract for providing methods that enhance the usage of native alert message functionality
    /// by allowing style customization and providing TPL wrappers.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Customizable font for the alert title
        /// </summary>
        /// <value>The title font.</value>
        ExtendedFont TitleFont { get; set; }

        /// <summary>
        /// Customizable font for the alert main text
        /// </summary>
        /// <value>The message font.</value>
        ExtendedFont MessageFont { get; set; }

		/// <summary>
		/// Displays an alert with one button to dismiss.
		/// </summary>
		/// <returns>Returns a <see cref="Task"/>.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="buttonText">Button's text</param>
		Task DisplayAlert(string title, string message, string buttonText = "Ok");

        /// <summary>
        /// Displays an alert prompting the user to choose between an affirmative and a negative action.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> with the user's response as a bool.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="affirmativeOption">The text for the affirmative user action, e.g. "Yes", "Ok".</param>
        /// <param name="negativeOption">The text for the negative user action, e.g. "No, "Cancel".</param>
        /// <param name="isAffirmativeDestructive">Determines whether the affirmative option should be displayed as a destructive action, e.g. deleting a file etc.</param>
        Task<bool> DisplayYesNoAlert(string title, string message, string affirmativeOption = "Yes", string negativeOption = "No", bool isAffirmativeDestructive = false);

		/// <summary>
		/// Displays an alert prompting the user for text input
		/// </summary>
		/// <returns>Returns a <see cref="Task"/> with the user's input.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		Task<string> DisplayTextInputAlert(string title, string message);

		/// <summary>
		/// Displays an alert prompting the user to open the app's settings page of the operating system.
		/// </summary>
		/// <returns>Returns a <see cref="Task"/> with the user's response as a bool 
        /// indicating whether the user chose to open the settings page.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="cancel">Cancel.</param>
		Task<bool> DisplaySettingsAlert(string title, string message, string cancel = "Cancel");

		/// <summary>
		/// Displays an action sheet with multiple options
		/// </summary>
		/// <returns>Returns a <see cref="Task"/> with the user's response as the name of the option.</returns>
		/// <param name="title">Title.</param>
		/// <param name="cancelOption">The text for the cancellation option. Ignored if empty.</param>
		/// <param name="destructiveOption">The text for the destructive option. Ignored if empty.</param>
		/// <param name="options">Additional options to display in the action sheet.</param>
		Task<string> DisplayActionSheet(string title, string cancelOption, string destructiveOption, params string[] options);

        /// <summary>
        /// Displays the native sharing sheet.
        /// </summary>
        /// <returns>The sharing sheet.</returns>
        /// <param name="text">Main text to share.</param>
        /// <param name="description">Optional description.</param>
        /// <param name="filePath">Optional path to file to be shared.</param>
        OperationResult DisplaySharingSheet(string text, string description, string filePath = "");
    }
}

