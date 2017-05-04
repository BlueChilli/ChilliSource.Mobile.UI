#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Provides methods that enhance the usage of native message alert functionality
	/// by allowing styling customization and providing TPL wrappers
	/// </summary>
	public interface IAlertService
	{

		ExtendedFont TitleFont { get; set; }

		ExtendedFont MessageFont { get; set; }

		/// <summary>
		/// Displays an alert with one button to dismiss
		/// </summary>
		/// <returns>The alert.</returns>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="buttonText">Button text</param>
		Task DisplayAlert(string title, string message, string buttonText = "Ok");

		Task<bool> DisplayYesNoAlert(string title, string message, string positive = "Yes", string negative = "No", bool destructivePositive = false);

		Task<string> DisplayTextInputAlert(string title, string message);

		Task<bool> DisplaySettingsAlert(string title, string message, string cancel = "Cancel");

		Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] options);

		OperationResult DisplaySharingSheet(string text, string filePath = "");
	}
}

