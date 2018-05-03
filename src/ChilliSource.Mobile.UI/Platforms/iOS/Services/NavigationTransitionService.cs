#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationTransitionService))]

namespace ChilliSource.Mobile.UI
{
    public class NavigationTransitionService : INavigationTransitionService
    {
        #region Interface Implementation

        public async Task PushAsync(Page originPage, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var nativeOriginPage = originPage.GetNativeController();

            var transitionDelegate = new NavigationDelegate()
            {
                TransitionType = transitionEffect,
                Options = options
            };

            nativeOriginPage.NavigationController.Delegate = transitionDelegate;

            await originPage.Navigation.PushAsync(destinationPage);

            nativeOriginPage.NavigationController.Delegate = null;
        }

        public async Task PopAsync(Page originPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var nativeOriginPage = originPage.GetNativeController();

            var navigationController = nativeOriginPage.NavigationController;

            var transitionDelegate = new NavigationDelegate()
            {
                TransitionType = transitionEffect,
                Options = options
            };

            navigationController.Delegate = transitionDelegate;

            await originPage.Navigation.PopAsync();

            navigationController.Delegate = null;
        }

        //Does not currently animate the transition. PR has been merged. Waiting for release.
        public async Task PushModalAsync(Page originPage, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var nativeDest = destinationPage.GetNativeController();

            var transitionDelegate = new ViewControllerTransitioningDelegate()
            {
                TransitionType = transitionEffect,
                Options = options
            };

            nativeDest.ModalPresentationStyle = UIModalPresentationStyle.Custom;

            nativeDest.TransitioningDelegate = transitionDelegate;

            await originPage.Navigation.PushModalAsync(destinationPage);
        }

        public async Task PopModalAsync(Page originPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var nativeOriginPage = originPage.GetNativeController();

            var transitionDelegate = new ViewControllerTransitioningDelegate()
            {
                TransitionType = transitionEffect,
                Options = options
            };

            nativeOriginPage.TransitioningDelegate = transitionDelegate;

            await originPage.Navigation.PopModalAsync();
        }

        #endregion

        #region Helpers

        public static IUIViewControllerAnimatedTransitioning GetTransition(bool isPushing, PageTransitionEffectType transitionEffect, TransitionOptions options)
        {
            BaseTransition transition = null;

            if (options == null)
            {
                options = new TransitionOptions();
            }

            if (isPushing)
            {
                switch (transitionEffect)
                {
                    case PageTransitionEffectType.None:
                        return null;

                    case PageTransitionEffectType.Checkerboard:
                        transition = new CheckerBoardTransition();
                        break;

                    case PageTransitionEffectType.ZoomFade:
                        transition = new ZoomFadeTransition();
                        break;

                    case PageTransitionEffectType.Fade:
                        transition = new FadeTransition();
                        break;

                    case PageTransitionEffectType.Swipe:
                        transition = new SwipeTransition();                        
                        ((SwipeTransition)transition).SwipeDirection = options.PushSwipeTransitionDirection;                        
                        break;

                    case PageTransitionEffectType.DoorSlide:
                        transition = new DoorSlideTransition();
                        break;
                    case PageTransitionEffectType.Fold:
                        transition = new FoldTransition();
                        break;
                    case PageTransitionEffectType.Turn:
                        transition = new TurnTransition();
                        break;
                }

                transition.TransitionOperation = UINavigationControllerOperation.Push;
            }
            else
            {
                switch (transitionEffect)
                {
                    case PageTransitionEffectType.None:
                        return null;

                    case PageTransitionEffectType.Checkerboard:
                        transition = new CheckerBoardTransition();
                        break;
                    case PageTransitionEffectType.ZoomFade:
                        transition = new ZoomFadeTransition();
                        break;
                    case PageTransitionEffectType.Fade:
                        transition = new FadeTransition();
                        break;
                    case PageTransitionEffectType.Swipe:
                        transition = new SwipeTransition();                        
                        ((SwipeTransition)transition).SwipeDirection = options.PopSwipeTransitionDirection;
                        ((SwipeTransition)transition).ApplyPopTransitionToOrigin = options.EnablePopSwipeTransition;
                        
                        break;

                    case PageTransitionEffectType.DoorSlide:
                        transition = new DoorSlideTransition();                        
                        ((DoorSlideTransition)transition).ApplyPopTransitionToOrigin = options.EnablePopSwipeTransition;
                        
                        break;
                    case PageTransitionEffectType.Fold:
                        transition = new FoldTransition();
                        break;
                    case PageTransitionEffectType.Turn:
                        transition = new TurnTransition();
                        break;
                }

                transition.TransitionOperation = UINavigationControllerOperation.Pop;
            }

            transition.Duration = options.TransitionDuration;
            transition.ToHasNavigationBar = options.DestinationPageHasNavigationBar;
            transition.FromHasNavigationBar = options.SourcePageHasNavigationBar;

            return transition;
        }
    }

    #endregion

    #region Delegates

    internal class NavigationDelegate : UINavigationControllerDelegate
    {
        public PageTransitionEffectType TransitionType { get; set; }

        public TransitionOptions Options { get; set; }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForOperation(UINavigationController navigationController, UINavigationControllerOperation operation, UIViewController fromViewController, UIViewController toViewController)
        {
            switch (operation)
            {
                case UINavigationControllerOperation.None:
                    return null;
                case UINavigationControllerOperation.Push:
                    return NavigationTransitionService.GetTransition(true, TransitionType, Options);
                case UINavigationControllerOperation.Pop:
                    return NavigationTransitionService.GetTransition(false, TransitionType, Options);
            }

            return null;
        }
    }

    internal class ViewControllerTransitioningDelegate : UIViewControllerTransitioningDelegate
    {
        public PageTransitionEffectType TransitionType { get; set; }

        public TransitionOptions Options { get; set; }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            return NavigationTransitionService.GetTransition(true, TransitionType, Options);
        }

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            return NavigationTransitionService.GetTransition(false, TransitionType, Options);
        }
    }

    #endregion
}
