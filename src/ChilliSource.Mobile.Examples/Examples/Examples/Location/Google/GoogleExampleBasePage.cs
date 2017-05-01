#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChilliSource.Mobile.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Examples
{
	public abstract class GoogleExampleBasePage : BaseContentPage
	{
		ExtendedEntry _apiKeyEntry;
		Action<bool, string> _handleDialogInputAction;

		public void CheckAPIKey(Action<bool, string> handleResult)
		{
			_handleDialogInputAction = handleResult;

			Device.BeginInvokeOnMainThread(async delegate
			{
				await ShowGoogleApiKeyEntryAlert();
			});
		}

		async Task ShowGoogleApiKeyEntryAlert()
		{
			_apiKeyEntry = new ExtendedEntry();
			_apiKeyEntry.Placeholder = "Enter Key";

			await AdvancedAlertView.ShowCustomAlert(new List<View>()
				{
					_apiKeyEntry,

					new ExtendedButton ()
					{
						Text = "Done",
						CustomFont = ThemeManager.ButtonNormalFont,
						BackgroundColor = Color.Gray,
						Command = new Command(async () => {

							await HandleGoogleApiDialog();
						})
					},

					new ExtendedButton ()
					{
						Text = "Cancel",
						CustomFont = ThemeManager.ButtonNormalFont,
						BackgroundColor = Color.Gray,
						Command = new Command(async () => {

							await PopupNavigation.PopAsync();
							await Navigation.PopAsync();
						})
					}
			}, ThemeManager.OrangePink, "Enter a Google API key", ThemeManager.CellTitleFont, alertType: AlertType.AccentOnly);
		}

		async Task HandleGoogleApiDialog()
		{
			if (_apiKeyEntry.Text != null && _apiKeyEntry.Text.Length > 0)
			{
				await PopupNavigation.PopAsync();
				_handleDialogInputAction.Invoke(true, _apiKeyEntry.Text);
			}
			else
			{
				_apiKeyEntry.BorderColor = Color.Red;
				_handleDialogInputAction.Invoke(false, null);
			}
		}
	}
}

