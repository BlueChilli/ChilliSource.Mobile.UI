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
	public partial class TransitionsExamplePage : BaseContentPage
	{
		TransitionPage _transitionPage;

		public TransitionsExamplePage()
		{
			BindingContext = this;

			_transitionPage = new TransitionPage();

			FadeCommand = new Command(async () =>
			{
				_transitionPage.Title = "Fade";
				_transitionPage.OriginTransitionType = PageTransitionType.Fade;

				await Navigation.PushAsyncWithTransition(_transitionPage, PageTransitionType.Fade);
			});

			FadeAndZoomCommand = new Command(async () =>
			{
				_transitionPage.Title = "Zoom Fade";
				_transitionPage.OriginTransitionType = PageTransitionType.ZoomFade;

				await Navigation.PushAsyncWithTransition(_transitionPage, PageTransitionType.ZoomFade);
			});

			SwipeCommand = new Command(async () =>
			{
				_transitionPage.Title = "Swipe";
				_transitionPage.OriginTransitionType = PageTransitionType.Swipe;

				await Navigation.PushModalAsyncWithTransition(_transitionPage, PageTransitionType.Swipe, new TransitionOptions()
				{
					PushSwipeTransitionDirection = SwipeDirection.TopToBotton,
				});
			});

			DoorSlideCommand = new Command(async () =>
			{
				_transitionPage.Title = "Door Slide";
				_transitionPage.OriginTransitionType = PageTransitionType.DoorSlide;

				await Navigation.PushAsyncWithTransition(_transitionPage, PageTransitionType.DoorSlide);
			});

			InitializeComponent();
		}

		public ICommand FadeCommand { get; set; }

		public ICommand FadeAndZoomCommand { get; set; }

		public ICommand SwipeCommand { get; set; }

		public ICommand DoorSlideCommand { get; set; }
	}
}
