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

namespace Examples
{
	public partial class LottieAnimationsExamplePage : BaseContentPage
	{
		float _animationSpeed = 1.0f;

		public LottieAnimationsExamplePage()
		{
			BindingContext = this;

			PlayCommand = new Command(() =>
			{
				anim.Animation.Play();
			});

			PauseCommand = new Command(() =>
			{
				anim.Animation.Pause();
			});

			SpeedCommand = new Command(() =>
			{
				if (_animationSpeed == 1.0f)
				{
					_animationSpeed = 2.0f;
				}
				else if (_animationSpeed == 2.0f)
				{
					_animationSpeed = 0.50f;
				}
				else if (_animationSpeed == 0.50f)
				{
					_animationSpeed = 1.0f;
				}

				anim.Animation.Speed = _animationSpeed;
				OnPropertyChanged(nameof(SpeedText));
			});

			InitializeComponent();
		}

		public string SpeedText
		{
			get
			{
				return string.Format("Speed - {0}", _animationSpeed.ToString());
			}
		}

		public ICommand PlayCommand { get; set; }

		public ICommand PauseCommand { get; set; }

		public ICommand SpeedCommand { get; set; }
	}
}
