#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Windows.Input;
using ChilliSource.Mobile.UI;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class AdvancedAlertViewExamplePage : BaseContentPage
	{
		public AdvancedAlertViewExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});

			ShowSuccessCommand = new Command(async () =>
			{
				await AdvancedAlertView.ShowPresetAlert(new List<ExtendedButton>()
				{
					AdvancedAlertViewButton.CreateButton("Ok", ThemeManager.ButtonNormalFont, AdvancedAlertView.SuccessColor, new Command(async () => {
						await PopupNavigation.PopAsync();
					}))
				}, PresetType.Positive, "Password Changed", ThemeManager.AdvancedActionSheetTitleFont, "Password changed successfully", ThemeManager.CellTitleFont, ThemeManager.ButtonNormalFont);
			});

			ShowNegativeCommand = new Command(async () =>
			{
				await AdvancedAlertView.ShowPresetAlert(new List<ExtendedButton>()
				{
					AdvancedAlertViewButton.CreateButton("Ok", ThemeManager.ButtonNormalFont, AdvancedAlertView.NegativeColor, new Command(async () => {
						await PopupNavigation.PopAsync();
					}))}, PresetType.Negative, "An Error Occured", ThemeManager.AdvancedActionSheetCancelFont, "Please try again", ThemeManager.CellTitleFont, ThemeManager.ButtonNormalFont);
			});

			ShowInfoCommand = new Command(async () =>
			{
				await AdvancedAlertView.ShowPresetAlert(new List<ExtendedButton>()
				{
					AdvancedAlertViewButton.CreateButton("Not bad", ThemeManager.ButtonNormalFont, AdvancedAlertView.DefaultColor, new Command(async () => {
						await PopupNavigation.PopAsync();
					}))}, PresetType.Info, "Hello", ThemeManager.CellTitleFont, "How are you?", ThemeManager.CellSubtitleFont, ThemeManager.ButtonNormalFont);
			});

			ShowWaitingCommand = new Command(async () =>
			{
				await AdvancedAlertView.ShowWaitingAlert(Color.Purple, "Loading...", ThemeManager.AdvancedActionSheetCancelFont, duration: 3000);
			});

			ShowCustomCommand = new Command(async () =>
			{
				await AdvancedAlertView.ShowCustomAlert(new List<View>()
				{
					new ExtendedButton ()
					{
						Text ="Long press",
						PressedBackgroundColor = Color.Orange,
						CustomFont = ThemeManager.CellTitleFont,
						EnableLongPress = true,
						LongPressFillDirection = LongPressDirection.RightToLeft,
						TouchUpAfterLongPressCommand = new Command(async () => {
							await PopupNavigation.PopAsync();
						})
					}
				}, Color.Orange, "Custom", ThemeManager.AdvancedActionSheetTitleFont, alertType: AlertType.CustomImage, customImage: "Images/Misc/dogemedium");
			});
		}

		public ICommand ShowSuccessCommand { get; set; }

		public ICommand ShowNegativeCommand { get; set; }

		public ICommand ShowInfoCommand { get; set; }

		public ICommand ShowWaitingCommand { get; set; }

		public ICommand ShowCustomCommand { get; set; }

		public IndexItem Item { get; set; }
	}
}
