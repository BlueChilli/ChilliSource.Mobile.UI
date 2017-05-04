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
using ChilliSource.Mobile.Media;
using System.Threading.Tasks;
using System.IO;
using ChilliSource.Mobile.Core;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class MediaLibraryExamplePage : BaseContentPage
	{
		IMediaLibraryService _mediaLibraryService;
		ICameraService _cameraService;

		enum SourceType
		{
			PickImage,
			CaptureImage,
			PickVideo,
			CaptureVideo
		}

		public MediaLibraryExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
			_mediaLibraryService = DependencyService.Get<IMediaLibraryService>();
			_cameraService = DependencyService.Get<ICameraService>();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public bool LoadingIndicatorVisible { get; set; }
		public IndexItem Item { get; set; }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (DependencyService.Get<ICameraService>().GetNumberOfVideoCameras() == 0)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Navigation.PopAsync();
					await Global.Instance.AlertService.DisplayAlert("ChilliSource Examples", "No cameras detected");
				});
			}
			else if (!_cameraService.HasCameraAccess)
			{
				_cameraService.RequestCameraAccess();
			}
		}

		#region Events

		async void TakePhotoTapped(object sender, System.EventArgs e)
		{
			await PerformAction(SourceType.CaptureImage);
		}

		async void TakeVideoTapped(object sender, System.EventArgs e)
		{
			await PerformAction(SourceType.CaptureVideo);
		}

		async void PickImageTapped(object sender, System.EventArgs e)
		{
			await PerformAction(SourceType.PickImage);
		}

		async void PickVideoTapped(object sender, System.EventArgs e)
		{
			await PerformAction(SourceType.PickVideo);
		}

		#endregion

		async Task PerformAction(SourceType type)
		{
			await ShowLoadingIndicator(true);

			var destinationPath = BuildDestinationPath(type);

			OperationResult imageResult = null;
			OperationResult videoResult = null;

			switch (type)
			{
				case SourceType.CaptureImage:
					imageResult = await _mediaLibraryService.CapturePhoto(destinationPath);

					if (imageResult.IsSuccessful)
					{
						await ShowImageSavedAlert(destinationPath);
					}
					break;

				case SourceType.CaptureVideo:
					videoResult = await _mediaLibraryService.CaptureVideo(destinationPath);

					if (videoResult.IsSuccessful)
					{
						await ShowVideoSavedAlert(destinationPath);
					}
					break;

				case SourceType.PickImage:
					imageResult = await _mediaLibraryService.PickVideo(destinationPath);

					if (imageResult.IsSuccessful)
					{
						await ShowImageSavedAlert(destinationPath);
					}
					break;

				case SourceType.PickVideo:
					videoResult = await _mediaLibraryService.PickVideo(destinationPath);

					if (videoResult.IsSuccessful)
					{
						await ShowVideoSavedAlert(destinationPath);
					}
					break;
			}

			await ShowLoadingIndicator(false);
		}

		#region Helpers

		Task ShowImageSavedAlert(string destinationPath)
		{
			return Global.Instance.AlertService.DisplayAlert("Image Saved", $"Location - {destinationPath}");
		}

		Task ShowVideoSavedAlert(string destinationPath)
		{
			return Global.Instance.AlertService.DisplayAlert("Video Saved", $"Location - {destinationPath}");
		}

		async Task ShowLoadingIndicator(bool show)
		{
			await Task.Delay(200);
			LoadingIndicatorVisible = show;
			OnPropertyChanged(nameof(LoadingIndicatorVisible));
		}

		string BuildDestinationPath(SourceType type)
		{
			var fileName = "";

			switch (type)
			{
				case SourceType.CaptureImage:
					fileName = $"ExampleCapturedPhoto.png";
					break;

				case SourceType.PickImage:
					fileName = $"ExamplePickedPhoto.png";
					break;

				case SourceType.CaptureVideo:
					fileName = $"ExampleCapturedVideo.mov";
					break;

				case SourceType.PickVideo:
					fileName = $"ExamplePickedVideo.mov";
					break;
			}

			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
		}

		#endregion
	}
}
