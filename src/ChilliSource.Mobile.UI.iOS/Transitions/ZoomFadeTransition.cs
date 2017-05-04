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
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class ZoomFadeTransition : BaseTransition
	{
		public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
		{
			var toViewController = GetViewController(transitionContext, UITransitionContext.ToViewControllerKey);
			var fromViewController = GetViewController(transitionContext, UITransitionContext.FromViewControllerKey);

			bool isPushing = TransitionOperation == UINavigationControllerOperation.Push;

			var detailView = toViewController.View;
			detailView.Frame = GetFrameWithNavigationBarOffset(detailView.Frame, toViewController, ToHasNavigationBar);

			transitionContext.ContainerView.AddSubview(detailView);
			detailView.Alpha = 0;

			if (!isPushing)
			{
				toViewController.View.Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
				toViewController.View.Alpha = 0.0f;
			}

			UIView.Animate(Duration, () =>
			{
				if (isPushing)
				{
					fromViewController.View.Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
				}
				else
				{
					toViewController.View.Transform = CGAffineTransform.MakeIdentity();
				}

				detailView.Alpha = 1;

			}, () =>
			{
				if (isPushing)
				{
					fromViewController.View.Transform = CGAffineTransform.MakeIdentity();
				}
				else
				{
					toViewController.View.Transform = CGAffineTransform.MakeIdentity();
				}

				transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
			});
		}

		public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}
	}
}
