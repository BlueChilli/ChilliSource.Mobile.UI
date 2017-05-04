#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Media;
using ChilliSource.Mobile.UI;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class VideoServiceExamplePage : BaseContentPage
	{
		IVideoService _videoService;

		public VideoServiceExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();

			_videoService = DependencyService.Get<IVideoService>();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public IndexItem Item { get; set; }

		/// <summary>
		/// Handles the combine videos clicked.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		async void Handle_CombineVideosClicked(object sender, EventArgs e)
		{
#if __IOS__



			Global.Instance.HudService.Show();

			var inputvideoPath = Path.Combine(FileSystemManager.GetApplicationBundlePath(), Path.Combine("Videos", "SampleVideo_1280x720_1mb.mp4"));

			var outputVideoPath = FilePathFactory.BuildDocumentPath("output.mp4", MediaType.Video.ToString());

			if (File.Exists(outputVideoPath))
			{
				File.Delete(outputVideoPath);
			}

			var result = await _videoService.CombineVideoFiles(new List<string>() { inputvideoPath, inputvideoPath }, outputVideoPath,
															   VideoExportQuality.High, VideoExportSize.R1080p, exportInLandscape: true, exportAudio: true);

			Global.Instance.HudService.Dismiss();

			if (result.IsSuccessful)
			{
				await Global.Instance.AlertService.DisplayAlert("Video Example", "Combination completed successfully!");
				_videoService.PlayVideo(outputVideoPath, true);
			}
			else
			{
				await Global.Instance.AlertService.DisplayAlert("Video Example", result.Message);
			}
#endif
		}
	}
}
