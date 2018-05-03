/*
 * Based on
 * Source: TransitionsDemo (https://github.com/xamarin/ios-samples/tree/master/TransitionsDemo)
 * Author: Xamarin (https://github.com/xamarin)
 * License: MIT
*/

using System;
using UIKit;
using CoreAnimation;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
namespace ChilliSource.Mobile.UI
{
    public enum TurnTransitionDirection
    {
        Horizontal = 0,
        Vertical
    }

    public class TurnTransition : BaseTransition
    {
        public TurnTransitionDirection TurnDirection { get; private set; }

        public bool Reverse { get; set; }

        public TurnTransition()
        {
            TurnDirection = TurnTransitionDirection.Vertical;
        }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            var toViewController = GetViewController(transitionContext, UITransitionContext.ToViewControllerKey);
            var fromViewController = GetViewController(transitionContext, UITransitionContext.FromViewControllerKey);

            UIView fromView = fromViewController.View;
            UIView toView = toViewController.View;


            UIView containerView = transitionContext.ContainerView;
            containerView.AddSubview(toView);

            // Add a perspective transform
            var transform = CATransform3D.Identity;
            transform.m34 = -0.002f;
            containerView.Layer.SublayerTransform = transform;

            // Give both VCs the same start frame
            CGRect initialFrame = transitionContext.GetInitialFrameForViewController(fromViewController);
            fromView.Frame = initialFrame;
            toView.Frame = initialFrame;

            float factor = Reverse ? 1f : -1f;

            // flip the to VC halfway round - hiding it
            toView.Layer.Transform = GetRotation(factor * -(float)Math.PI / 2);
            double duration = TransitionDuration(transitionContext);

            Action animations = () =>
            {
                UIView.AddKeyframeWithRelativeStartTime(0.0, 0.5, () =>
                {
                    fromView.Layer.Transform = GetRotation(factor * (float)Math.PI / 2);
                });

                UIView.AddKeyframeWithRelativeStartTime(0.5, 0.5, () =>
                {
                    toView.Layer.Transform = GetRotation(0f);
                });
            };

            UIView.AnimateKeyframes(duration, 0.0, UIViewKeyframeAnimationOptions.CalculationModeLinear, animations, (finished) =>
            {
                transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
            });

        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return Duration;
        }

        private CATransform3D GetRotation(float angle)
        {
            if (TurnDirection == TurnTransitionDirection.Horizontal)
            {
                return TransitionHelper.BuildXRotationTransform(angle);
            }
            else
            {
                return TransitionHelper.BuildYRotationTransform(angle);
            }
        }
    }
}
