#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreGraphics;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	public class FadeTransition : BaseTransition
	{
		public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
		{
			var toViewController = GetViewController(transitionContext, UITransitionContext.ToViewControllerKey);

			bool isPushing = TransitionOperation == UINavigationControllerOperation.Push;

			var detailView = toViewController.View;

			detailView.Frame = GetFrameWithNavigationBarOffset(detailView.Frame, toViewController, ToHasNavigationBar);

			transitionContext.ContainerView.AddSubview(detailView);

			detailView.Alpha = 0;

			if (!isPushing)
			{
				toViewController.View.Alpha = 0.0f;
			}

			UIView.Animate(Duration, () =>
			{
				detailView.Alpha = 1;
			}, () =>
			{
				transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
			});
		}

		public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}
	}
}
