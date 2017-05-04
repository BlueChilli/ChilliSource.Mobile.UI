#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using ChilliSource.Mobile.UI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Examples
{
	public partial class AdvancedActionSheetExamplePage : BaseContentPage
	{
		public IndexItem Item { get; set; }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await AdvancedActionSheet.ShowActionSheet(new List<AdvancedActionSheetAction>()
							{
								AdvancedActionSheetAction.CreateAction("Option one", ThemeManager.CellTitleFont, null),
								AdvancedActionSheetAction.CreateAction("Option two", ThemeManager.CellSubtitleFont, null),
								AdvancedActionSheetAction.CreateAction("Cancel", ThemeManager.AdvancedActionSheetCancelFont, new Command(async () => {
									await PopupNavigation.PopAsync();
								}), ActionType.Cancel),

							}, "Im Advanced!", ThemeManager.AdvancedActionSheetTitleFont);
		}

		public AdvancedActionSheetExamplePage(IndexItem indexItem)
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
		}
	}
}
