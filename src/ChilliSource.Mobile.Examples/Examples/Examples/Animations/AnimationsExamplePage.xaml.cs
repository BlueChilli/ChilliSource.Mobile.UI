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

namespace Examples
{
	public partial class AnimationsExamplePage : BaseContentPage
	{
		public AnimationsExamplePage()
		{
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
		}

		void SetupCommands()
		{
			BounceCommand = new Command(() =>
			{
				ResetAnimations();
				BoxBounceAnimation.PerformAnimation();
			});

			ScaleCommand = new Command(() =>
			{
				ResetAnimations();
				BoxScaleAnimation.PerformAnimation();
			});

			RotateCommand = new Command(() =>
			{
				ResetAnimations();
				BoxRotationAnimation.PerformAnimation();
			});

			ShakeCommand = new Command(() =>
			{
				ResetAnimations();
				BoxShakeAnimation.PerformAnimation();
			});

			VibrateCommand = new Command(() =>
			{
				ResetAnimations();
				BoxVibrateAnimation.PerformAnimation();
			});

			FadeCommand = new Command(() =>
			{
				ResetAnimations();
				BoxFadeAnimation.PerformAnimation();
			});
		}

		void ResetAnimations()
		{
			BoxFadeAnimation.CancelAnimation();
			BoxVibrateAnimation.CancelAnimation();
			BoxShakeAnimation.CancelAnimation();
			BoxRotationAnimation.CancelAnimation();
			BoxScaleAnimation.CancelAnimation();
			BoxBounceAnimation.CancelAnimation();
		}

		public ICommand BounceCommand { get; private set; }
		public ICommand ScaleCommand { get; private set; }
		public ICommand RotateCommand { get; private set; }
		public ICommand ShakeCommand { get; private set; }
		public ICommand VibrateCommand { get; private set; }
		public ICommand FadeCommand { get; private set; }
	}
}
