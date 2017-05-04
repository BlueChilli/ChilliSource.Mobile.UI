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
	public partial class PlacesServiceExamplePage : GoogleExampleBasePage
	{
		PlacesServiceExamplePageViewModel _viewModel;

		public IndexItem Item { get; set; }

		public PlacesServiceExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			Title = Item.Title;
			_viewModel = new PlacesServiceExamplePageViewModel();
			BindingContext = _viewModel;
			SetupCommands();
			InitializeComponent();
			SetupScreen();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			CheckAPIKey((result, apiKey) =>
			{
				Global.Instance.PlacesService.ApiKey = apiKey;
				searchBar.Focus();
			});
		}

		void SetupScreen()
		{
			searchBar.SearchButtonPressed += async (sender, e) => await SearchForAddresses();
			searchBar.TextChanged += async (sender, e) => await SearchForAddresses();
		}

		public async Task SearchForAddresses()
		{
			await Task.Delay(700);
			_viewModel.SearchAddress(searchBar.Text);
		}
	}
}
