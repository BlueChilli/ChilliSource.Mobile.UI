#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using ChilliSource.Mobile.Location;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Examples
{
	public partial class LocationServiceExamplePage : BaseContentPage, IDisposable
	{
		string _latitudeText;
		string _longitudeText;
		string _altitudeText;
		string _headingText;

		public LocationServiceExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();

			Global.Instance.LocationService.PositionChanged += LocationService_PositionChanged;
			Global.Instance.LocationService.StartListening(0, 0, true);
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public string LatitudeText
		{
			get
			{
				return _latitudeText;
			}

			set
			{
				_latitudeText = value;
				OnPropertyChanged();
			}
		}


		public string LongitudeText
		{
			get
			{
				return _longitudeText;
			}

			set
			{
				_longitudeText = value;
				OnPropertyChanged();
			}
		}


		public string AltitudeText
		{
			get
			{
				return _altitudeText;
			}

			set
			{
				_altitudeText = value;
				OnPropertyChanged();
			}
		}

		public string HeadingText
		{
			get
			{
				return _headingText;
			}

			set
			{
				_headingText = value;
				OnPropertyChanged();
			}
		}

		public IndexItem Item { get; set; }

		void LocationService_PositionChanged(object sender, PositionEventArgs e)
		{
			LatitudeText = e.Position.Latitude.ToString();
			LongitudeText = e.Position.Longitude.ToString();
			AltitudeText = e.Position.Altitude.ToString();
			HeadingText = e.Position.Heading.ToString();
		}

		public void Dispose()
		{
			Global.Instance.LocationService.StopListening();
		}
	}
}
