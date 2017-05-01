#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using ChilliSource.Mobile.UI.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using Foundation;

[assembly: Dependency(typeof(TransitionService))]

namespace ChilliSource.Mobile.UI
{
	public class TransitionService : INavigationTransitionService
	{
		#region Interface Implementation

		public async Task PushAsync(Page originPage, Page destinationPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var nativeOriginPage = originPage.GetNativeController();

			var transitionDelegate = new NavigationDelegate()
			{
				TransitionType = transition,
				Options = options
			};

			nativeOriginPage.NavigationController.Delegate = transitionDelegate;

			await originPage.Navigation.PushAsync(destinationPage);

			nativeOriginPage.NavigationController.Delegate = null;
		}

		public async Task PopAsync(Page originPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var nativeOriginPage = originPage.GetNativeController();

			var navigationController = nativeOriginPage.NavigationController;

			var transitionDelegate = new NavigationDelegate()
			{
				TransitionType = transition,
				Options = options
			};

			navigationController.Delegate = transitionDelegate;

			await originPage.Navigation.PopAsync();

			navigationController.Delegate = null;
		}

		//Does not currently animate the transition. PR has been merged. Waiting for release.
		public async Task PushModalAsync(Page originPage, Page destinationPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var nativeDest = destinationPage.GetNativeController();

			var transitionDelegate = new ViewControllerTransitioningDelegate()
			{
				TransitionType = transition,
				Options = options
			};

			nativeDest.ModalPresentationStyle = UIModalPresentationStyle.Custom;

			nativeDest.TransitioningDelegate = transitionDelegate;

			await originPage.Navigation.PushModalAsync(destinationPage);
		}

		public async Task PopModalAsync(Page originPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var nativeOriginPage = originPage.GetNativeController();

			var transitionDelegate = new ViewControllerTransitioningDelegate()
			{
				TransitionType = transition,
				Options = options
			};

			nativeOriginPage.TransitioningDelegate = transitionDelegate;

			await originPage.Navigation.PopModalAsync();
		}

		#endregion

		#region Helpers

		public static IUIViewControllerAnimatedTransitioning GetTransition(bool isPushing, PageTransitionType transitionType, TransitionOptions options)
		{
			BaseTransition transition = null;

			if (isPushing)
			{
				switch (transitionType)
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
						((SwipeTransition)transition).SwipeDirection = options.PushSwipeTransitionDirection;
						break;

					case PageTransitionType.DoorSlide:
						transition = new DoorSlideTransition();
						break;
				}

				transition.TransitionOperation = UINavigationControllerOperation.Push;
			}
			else
			{
				switch (transitionType)
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
						((SwipeTransition)transition).SwipeDirection = options.PopSwipeTransitionDirection;
						((SwipeTransition)transition).ApplyPopTransitionToOrigin = options.ApplyPopSwipeTransitionToOrigin;
						break;

					case PageTransitionType.DoorSlide:
						transition = new DoorSlideTransition();
						((DoorSlideTransition)transition).ApplyPopTransitionToOrigin = options.ApplyPopSwipeTransitionToOrigin;
						break;
				}

				transition.TransitionOperation = UINavigationControllerOperation.Pop;
			}

			transition.Duration = options.TransitionDuration;
			transition.ToHasNavigationBar = options.ToHasNavigationBar;
			transition.FromHasNavigationBar = options.FromHasNavigationBar;

			return transition;
		}
	}

	#endregion

	#region Delegates

	internal class NavigationDelegate : UINavigationControllerDelegate
	{
		public PageTransitionType TransitionType { get; set; }

		public TransitionOptions Options { get; set; }

		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForOperation(UINavigationController navigationController, UINavigationControllerOperation operation, UIViewController fromViewController, UIViewController toViewController)
		{
			switch (operation)
			{
				case UINavigationControllerOperation.None:
					return null;
				case UINavigationControllerOperation.Push:
					return TransitionService.GetTransition(true, TransitionType, Options);
				case UINavigationControllerOperation.Pop:
					return TransitionService.GetTransition(false, TransitionType, Options);
			}

			return null;
		}
	}

	internal class ViewControllerTransitioningDelegate : UIViewControllerTransitioningDelegate
	{
		public PageTransitionType TransitionType { get; set; }

		public TransitionOptions Options { get; set; }

		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
		{
			return TransitionService.GetTransition(true, TransitionType, Options);
		}

		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
		{
			return TransitionService.GetTransition(false, TransitionType, Options);
		}
	}

	#endregion
}
