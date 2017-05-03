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
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class IndicatorsExamplePage : BaseContentPage
	{
		HudService _hudService;
		LoadingIndicatorView _loadingIndicator;

		public IndicatorsExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
			_loadingIndicator = new LoadingIndicatorView()
			{
				CustomFont = ThemeManager.CellTitleFont
			};
		}

		void SetupCommands()
		{

			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});

			BannerCommand = new Command(async () =>
			{
				await BannerView.DisplayToast("Much banner", ThemeManager.CellTitleFont, "dogesmall.png", Color.Orange, Navigation, BannerPosition.Top);
			});

			HUD1Command = new Command(async () =>
			{
				_hudService = new HudService();
				_hudService.Show("Hello there");
				await Task.Delay(500);
				_hudService.Dismiss();
			});

			HUD2Command = new Command(async () =>
			{
				mainStack.Children.Add(_loadingIndicator);
				await Task.Delay(500);
				mainStack.Children.Remove(_loadingIndicator);
			});
		}


		public IndexItem Item { get; set; }
		public ICommand BannerCommand { get; set; }
		public ICommand HUD1Command { get; set; }
		public ICommand HUD2Command { get; set; }

	}
}
