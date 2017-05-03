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
using ChilliSource.Mobile.Location.Google.Maps.Directions;
using ChilliSource.Mobile.Location;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class DirectionsExamplePage : GoogleExampleBasePage
	{
		DirectionsManager _directionsManager;
		BaseResponseModel _response;

		public DirectionsExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			SetupCommands();
			BindingContext = this;
			InitializeComponent();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public bool ButtonsVisible { get; set; }

		public bool IndicatorVisible { get; set; }
		public IndexItem Item { get; set; }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (_directionsManager == null)
			{
				CheckAPIKey((result, apiKey) =>
				{
					_directionsManager = new DirectionsManager(apiKey);
					searchBar.Focus();
				});
			}
		}

		#region Handlers

		async void SearchPressed(object sender, EventArgs e)
		{
			IndicatorVisible = true;
			OnPropertyChanged(nameof(IndicatorVisible));

			var position = await Global.Instance.LocationService.GetPositionAsync(2000, false);

			_response = await _directionsManager.RequestDirections(BuildDirectionsRequest(searchBar.Text, position));

			ButtonsVisible = _response != null;
			IndicatorVisible = false;

			OnPropertyChanged(nameof(ButtonsVisible));
			OnPropertyChanged(nameof(IndicatorVisible));
		}

		async void ShowDirectionsPressed(object sender, EventArgs e)
		{
			var steps = _response.Routes.First()?.Legs.First()?.Steps;

			if (steps != null)
			{
				var page = new ContentPage()
				{
					Title = "Directions",
					Content = new ListView()
					{
						ItemsSource = steps.Select((arg1, arg2) =>
						{
							return arg1.Instructions;
						}).ToList()
					}
				};

				await Navigation.PushAsync(page);
			}
		}

		async void ShowInfoPressed(object sender, EventArgs e)
		{
			var page = new ContentPage()
			{
				Title = "Route Info",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					Children =
					{
						new Label()
						{
							Text = $"Duration - {_response.GetJourneyDuration().ToString()}"
						},
						new Label()
						{
							Text = $"Duration with traffic - {_response.GetJourneyDurationWithTraffic().ToString()}"
						},
						new Label()
						{
							Text = $"Distance (M) - {_response.GetJourneyDistance().ToString()}"
						}
					}
				}
			};

			await Navigation.PushAsync(page);
		}

		#endregion

		Request BuildDirectionsRequest(string destinationAddress, Position originPosition)
		{
			return new Request()
			{
				OriginCoordinates = new Tuple<string, string>(Convert.ToString(originPosition.Latitude), Convert.ToString(originPosition.Longitude)),
				DestinationAddress = destinationAddress
			};
		}
	}
}
