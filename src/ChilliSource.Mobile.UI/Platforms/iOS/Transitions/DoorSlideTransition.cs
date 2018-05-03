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
    public class DoorSlideTransition : BaseTransition
    {
        public bool ApplyPopTransitionToOrigin { get; set; }

        public override void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            var toViewController = GetViewController(transitionContext, UITransitionContext.ToViewControllerKey);
            var fromViewController = GetViewController(transitionContext, UITransitionContext.FromViewControllerKey);

            Tuple<UIImageView, UIImageView> splitImageViews;

            if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
            {
                toViewController.View.RemoveFromSuperview();
                fromViewController.View.Frame = GetFrameWithNavigationBarOffset(fromViewController.View.Frame, fromViewController, FromHasNavigationBar);
                transitionContext.ContainerView.AddSubview(fromViewController.View);

                splitImageViews = SplitViewVertically(toViewController.View);
            }
            else
            {
                fromViewController.View.RemoveFromSuperview();
                toViewController.View.Frame = GetFrameWithNavigationBarOffset(toViewController.View.Frame, toViewController, ToHasNavigationBar);
                transitionContext.ContainerView.AddSubview(toViewController.View);

                splitImageViews = SplitViewVertically(fromViewController.View);
            }

            var leftSide = splitImageViews.Item1;
            AddShadowToView(leftSide);

            var rightSide = splitImageViews.Item2;
            AddShadowToView(rightSide);

            leftSide.Frame = GetFrameWithNavigationBarOffset(leftSide.Frame, fromViewController, FromHasNavigationBar);
            rightSide.Frame = GetFrameWithNavigationBarOffset(rightSide.Frame, fromViewController, FromHasNavigationBar);

            transitionContext.ContainerView.AddSubview(leftSide);
            transitionContext.ContainerView.AddSubview(rightSide);

            if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
            {
                leftSide.Transform = CGAffineTransform.MakeTranslation(-leftSide.Frame.Width, 0);
                rightSide.Transform = CGAffineTransform.MakeTranslation(rightSide.Frame.Width, 0);
            }
            else
            {
                toViewController.View.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
            }

            UIView.Animate(Duration, () =>
            {
                if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
                {
                    leftSide.Transform = CGAffineTransform.MakeIdentity();
                    rightSide.Transform = CGAffineTransform.MakeIdentity();
                    fromViewController.View.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
                }
                else
                {
                    leftSide.Transform = CGAffineTransform.MakeTranslation(-leftSide.Frame.Width, 0);
                    rightSide.Transform = CGAffineTransform.MakeTranslation(rightSide.Frame.Width, 0);
                    toViewController.View.Transform = CGAffineTransform.MakeIdentity();
                }

            }, () =>
            {
                leftSide.RemoveFromSuperview();
                rightSide.RemoveFromSuperview();

                if (TransitionOperation == UINavigationControllerOperation.Pop && ApplyPopTransitionToOrigin)
                {
                    transitionContext.ContainerView.AddSubview(toViewController.View);
                }
                transitionContext.CompleteTransition(!transitionContext.TransitionWasCancelled);
            });
        }

        public override double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return Duration;
        }

        Tuple<UIImageView, UIImageView> SplitViewVertically(UIView view)
        {
            UIGraphics.BeginImageContextWithOptions(view.Frame.Size, view.Opaque, 0.0f);
            view.DrawViewHierarchy(view.Bounds, true);
            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var viewSnapShot = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            var leftFrame = new CGRect(0, 0, view.Bounds.Size.Width / 2, view.Bounds.Size.Height);
            var rightFrame = new CGRect(view.Bounds.Size.Width / 2, 0, view.Bounds.Size.Width / 2, view.Bounds.Size.Height);

            var leftSideImage = CropImageFromImage(viewSnapShot, leftFrame);
            var leftSideImageView = new UIImageView(leftSideImage)
            {
                Frame = leftFrame
            };

            var rightSideImage = CropImageFromImage(viewSnapShot, rightFrame);
            var rightSideImageView = new UIImageView(rightSideImage)
            {
                Frame = rightFrame
            };

            return new Tuple<UIImageView, UIImageView>(leftSideImageView, rightSideImageView);
        }

        UIImage CropImageFromImage(UIImage image, CGRect rect)
        {
            var newRect = rect;
            newRect.X = newRect.X * image.CurrentScale;
            newRect.Y = newRect.Y * image.CurrentScale;

            var newRectSize = rect.Size;
            newRectSize.Height = newRectSize.Height * image.CurrentScale;
            newRectSize.Width = newRectSize.Width * image.CurrentScale;

            newRect.Size = newRectSize;

            var sourceImageRef = image.CGImage;

            var newImageRef = sourceImageRef.WithImageInRect(newRect);

            return UIImage.FromImage(newImageRef, image.CurrentScale, image.Orientation);
        }

        void AddShadowToView(UIView view)
        {
            var rightShadowPath = UIBezierPath.FromRect(view.Layer.Bounds).CGPath;

            view.Layer.ShadowColor = UIColor.Black.CGColor;
            view.Layer.ShadowOffset = new CGSize(0, 5);
            view.Layer.ShadowOpacity = 1;
            view.Layer.ShadowRadius = 1.0f;
            view.Layer.ShadowPath = rightShadowPath;
        }
    }
}
