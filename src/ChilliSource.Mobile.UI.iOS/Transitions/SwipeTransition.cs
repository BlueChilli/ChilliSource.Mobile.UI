#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;
using CoreGraphics;

namespace ChilliSource.Mobile.UI
{
	public class SwipeTransition : BaseTransition
	{
		public SwipeDirection SwipeDirection { get; set; }

		public bool ApplyPopTransitionToOrigin { get; set; }

		public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
		{
			var toViewController = GetViewController(transitionContext, UITransitionContext.ToViewControllerKey);
			var fromViewController = GetViewController(transitionContext, UITransitionContext.FromViewControllerKey);

			var detailView = new UIView();

			if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
			{
				detailView = fromViewController.View;
				detailView.Frame = GetFrameWithNavigationBarOffset(detailView.Frame, fromViewController, FromHasNavigationBar);
			}
			else
			{
				detailView = toViewController.View;
				detailView.Frame = GetFrameWithNavigationBarOffset(detailView.Frame, toViewController, ToHasNavigationBar);
			}

			transitionContext.ContainerView.AddSubview(detailView);

			if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
			{
				var toView = toViewController.View;
				toView.Frame = GetFrameWithNavigationBarOffset(toView.Frame, fromViewController, FromHasNavigationBar);
				transitionContext.ContainerView.InsertSubviewBelow(toView, detailView);
			}
			else
			{
				detailView.Transform = GetInitialTranslation(detailView);
			}

			UIView.Animate(Duration, () =>
			{
				if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
				{
					detailView.Transform = GetOriginViewPopTranslation(detailView);
				}
				else
				{
					detailView.Transform = CGAffineTransform.MakeIdentity();
				}

			}, () =>
			{
				if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
				{
					detailView.RemoveFromSuperview();
				}
				transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
			});
		}

		public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}

		CGAffineTransform GetInitialTranslation(UIView detailView)
		{
			switch (SwipeDirection)
			{
				case SwipeDirection.RightToLeft:
					return CGAffineTransform.MakeTranslation(detailView.Frame.Size.Width, 0);

				case SwipeDirection.LeftToRight:
					return CGAffineTransform.MakeTranslation(-detailView.Frame.Size.Width, 0);

				case SwipeDirection.TopToBotton:
					return CGAffineTransform.MakeTranslation(0, -detailView.Frame.Size.Height);

				case SwipeDirection.BottomToTop:
					return CGAffineTransform.MakeTranslation(0, detailView.Frame.Size.Height);
			}

			return CGAffineTransform.MakeTranslation(0, 0);
		}

		CGAffineTransform GetOriginViewPopTranslation(UIView detailView)
		{
			switch (SwipeDirection)
			{
				case SwipeDirection.RightToLeft:
					return CGAffineTransform.MakeTranslation(-detailView.Frame.Size.Width, 0);

				case SwipeDirection.LeftToRight:
					return CGAffineTransform.MakeTranslation(detailView.Frame.Size.Width, 0);

				case SwipeDirection.TopToBotton:
					return CGAffineTransform.MakeTranslation(0, detailView.Frame.Size.Height);

				case SwipeDirection.BottomToTop:
					return CGAffineTransform.MakeTranslation(0, -detailView.Frame.Size.Height);
			}

			return CGAffineTransform.MakeTranslation(0, 0);
		}
	}
}
