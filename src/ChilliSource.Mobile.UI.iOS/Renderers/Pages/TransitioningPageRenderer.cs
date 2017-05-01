#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Page renderer for the transitioning page.
	/// </summary>
	public class TransitioningPageRenderer : PageRenderer, ITransitionalNavigation, IUINavigationControllerDelegate
	{
		PageTransitionType _pushTransitionType;
		PageTransitionType _popTransitionType;
		TransitionOptions _options;

		Page _transitioningPage;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
			{
				return;
			}

			_transitioningPage = Element as Page;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (HasNavigationController())
			{
				NavigationController.WeakDelegate = this;
				AutomaticallyAdjustsScrollViewInsets = false;
				EdgesForExtendedLayout = UIRectEdge.None;
			}
		}

		//Returns the correct transiton to use depending on the operation and transition type
		[Export("navigationController:animationControllerForOperation:fromViewController:toViewController:")]
		public IUIViewControllerAnimatedTransitioning GetAnimationControllerForOperation(UINavigationController navigationController, UINavigationControllerOperation operation, UIViewController fromViewController, UIViewController toViewController)
		{
			BaseTransition transition = null;

			switch (operation)
			{
				case UINavigationControllerOperation.None:
					return null;
				case UINavigationControllerOperation.Push:
					switch (_pushTransitionType)
					{
						case PageTransitionType.None:
							return null;

						case PageTransitionType.Checkerboard:
							transition = new CheckerBoardTransition();
							break;

						case PageTransitionType.ZoomFade:
							transition = new ZoomFadeTransition();
							break;

						case PageTransitionType.Fade:
							transition = new FadeTransition();
							break;

						case PageTransitionType.Swipe:
							transition = new SwipeTransition();
							((SwipeTransition)transition).SwipeDirection = _options.PushSwipeTransitionDirection;
							break;

						case PageTransitionType.DoorSlide:
							transition = new DoorSlideTransition();
							break;
					}

					transition.TransitionOperation = operation;

					transition.Duration = _options.TransitionDuration;
					transition.ToHasNavigationBar = _options.ToHasNavigationBar;
					transition.FromHasNavigationBar = _options.FromHasNavigationBar;

					return transition;

				case UINavigationControllerOperation.Pop:
					switch (_popTransitionType)
					{
						case PageTransitionType.None:
							return null;

						case PageTransitionType.Checkerboard:
							transition = new CheckerBoardTransition();
							break;
						case PageTransitionType.ZoomFade:
							transition = new ZoomFadeTransition();
							break;
						case PageTransitionType.Fade:
							transition = new FadeTransition();
							break;
						case PageTransitionType.Swipe:
							transition = new SwipeTransition();
							((SwipeTransition)transition).SwipeDirection = _options.PopSwipeTransitionDirection;
							((SwipeTransition)transition).ApplyPopTransitionToOrigin = _options.ApplyPopSwipeTransitionToOrigin;
							break;

						case PageTransitionType.DoorSlide:
							transition = new DoorSlideTransition();
							((DoorSlideTransition)transition).ApplyPopTransitionToOrigin = _options.ApplyPopSwipeTransitionToOrigin;
							break;
					}

					transition.TransitionOperation = operation;
					transition.Duration = _options.TransitionDuration;
					transition.ToHasNavigationBar = _options.ToHasNavigationBar;
					transition.FromHasNavigationBar = _options.FromHasNavigationBar;
					return transition;
			}
			return null;
		}

		private bool HasNavigationController()
		{
			return this.NavigationController != null && this.NavigationController.TopViewController != null;
		}

		#region Interface Implementation

		public Task PushAsyncWithTransition(Page destinationPage, PageTransitionType transition, TransitionOptions options)
		{
			_pushTransitionType = transition;
			_options = options ?? new TransitionOptions();

			return _transitioningPage.Navigation.PushAsync(destinationPage);
		}

		public Task PopAsyncWithTransition(PageTransitionType transition, TransitionOptions options)
		{
			_popTransitionType = transition;
			_options = options ?? new TransitionOptions();

			return _transitioningPage.Navigation.PopAsync();
		}

		public Task PerformPushTransitionAsync(INavigation sender, Page destinationPage, PageTransitionType transition, TransitionOptions options = null)
		{
			throw new NotImplementedException();
		}

		public Task PerformPopTransitionAsync(INavigation sender, PageTransitionType transition, TransitionOptions options = null)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
