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
using System.IO;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class AudioRecordingExamplePage : BaseContentPage
	{
		bool _isRecording;
		bool _isPlaying;

		IAudioRecordingService _recordingService;
		IAudioPlaybackService _playbackService;

		public AudioRecordingExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();

			_recordingService = DependencyService.Get<IAudioRecordingService>();
			_playbackService = DependencyService.Get<IAudioPlaybackService>();

			InitializeServices();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		void InitializeServices()
		{
			var recResult = _recordingService.Initialize(FileLocation());

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			_recordingService.Dispose();
			_playbackService.Dispose();
		}

		public string RecordText
		{
			get
			{
				return _isRecording ? "Stop Recording" : "Record";
			}
		}

		public string PlayText
		{
			get
			{
				return _isPlaying ? "Pause" : "Play";
			}
		}

		public IndexItem Item { get; set; }

		#region Events

		void RecordTapped(object sender, System.EventArgs e)
		{
			if (_isRecording)
			{
				_recordingService.Stop();
			}
			else
			{
				_recordingService.Record();
			}

			_isRecording = !_isRecording;
			OnPropertyChanged(nameof(RecordText));
		}

		void PlayTapped(object sender, System.EventArgs e)
		{
			if (_isPlaying)
			{
				_playbackService.Pause();
			}
			else
			{
				_playbackService.Initialize(FileLocation());

				_playbackService.OnPlaybackCompleted += (senderr, er) =>
				{
					_isPlaying = !_isPlaying;
					OnPropertyChanged(nameof(PlayText));
				};

				_playbackService.Play();
			}

			_isPlaying = !_isPlaying;
			OnPropertyChanged(nameof(PlayText));
		}

		#endregion

		string FileLocation()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"AudioExampleFile.caf");
		}
	}
}
