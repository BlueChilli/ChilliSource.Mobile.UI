#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Media;
using ChilliSource.Mobile.UI;
using ChilliSource.Mobile.Location;

using Xamarin.Forms;
using ChilliSource.Mobile.Location.Google.Places;
using ChilliSource.Mobile.UI.Core;

namespace Examples
{
	public class Global
	{
		private static readonly Lazy<Global> _lazyInstance = new Lazy<Global>(() => new Global());

		private Global()
		{
		}

		public static Global Instance { get { return _lazyInstance.Value; } }

		public IHudService HudService { get; private set; }

		public IDeviceService DeviceService { get; private set; }

		public IAlertService AlertService { get; private set; }

		public PlacesService PlacesService { get; set; }

		public ILocationService LocationService { get; set; }

		public void Initialize()
		{
			SetupFolders();
			SetupServices();
		}

		void SetupServices()
		{
			DeviceService = DependencyService.Get<IDeviceService>();
			HudService = DependencyService.Get<IHudService>();
			AlertService = DependencyService.Get<IAlertService>();
			PlacesService = new PlacesService("");

			SetupLocationService();
		}

		void SetupLocationService()
		{
			LocationService = DependencyService.Get<ILocationService>();

			if (LocationService != null)
			{
				LocationService.Initialize(LocationAuthorizationType.WhenInUse, false);
			}
		}

		void SetupFolders()
		{

			//TODO: fix android Mobile.Core target framework problems
#if __IOS__
			FileSystemManager.CreateDocumentsSubfolder(MediaType.Photo.ToString());
			FileSystemManager.CreateDocumentsSubfolder(MediaType.Video.ToString());
			FileSystemManager.CreateDocumentsSubfolder(MediaType.Audio.ToString());
#endif
		}
	}
}
