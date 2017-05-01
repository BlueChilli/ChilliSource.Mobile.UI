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

namespace Examples
{
	public partial class IndicatorsExamplePage : BaseContentPage
	{
		HudService _hudService;
		LoadingIndicatorView _loadingIndicator;

		public IndicatorsExamplePage()
		{
			BindingContext = this;
			InitializeComponent();
			_loadingIndicator = new LoadingIndicatorView()
			{
				CustomFont = ThemeManager.CellTitleFont
			};
		}

		public ICommand BannerCommand
		{
			get
			{
				return new Command(async () =>
				{
					await BannerView.DisplayToast("Much banner", ThemeManager.CellTitleFont, "dogesmall.png", Color.Orange, Navigation, BannerPosition.Top);
				});
			}
		}

		public ICommand HUD1Command
		{
			get
			{
				return new Command(async () =>
			   {
				   _hudService = new HudService();
				   _hudService.Show("Hello there");
				   await Task.Delay(500);
				   _hudService.Dismiss();
			   });
			}
		}

		public ICommand HUD2Command
		{
			get
			{
				return new Command(async () =>
			   {
				   mainStack.Children.Add(_loadingIndicator);
				   await Task.Delay(500);
				   mainStack.Children.Remove(_loadingIndicator);
			   });
			}
		}
	}
}
