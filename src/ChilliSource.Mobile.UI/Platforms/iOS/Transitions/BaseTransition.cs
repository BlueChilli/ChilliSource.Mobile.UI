#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;
using Xamarin.Forms;
using CoreGraphics;
using Foundation;

namespace ChilliSource.Mobile.UI
{
    public abstract class BaseTransition : UIViewControllerAnimatedTransitioning
    {
        /// <summary>
        /// Duration in seconds
        /// </summary>
		public float Duration { get; set; }

        public bool ToHasNavigationBar { get; set; }

        public bool FromHasNavigationBar { get; set; }

        public UINavigationControllerOperation TransitionOperation { get; set; }

        public UIViewController GetViewController(IUIViewControllerContextTransitioning context, NSString key)
        {
            return context.GetViewControllerForKey(key);
        }

        public CGRect GetFrameWithNavigationBarOffset(CGRect baseFrame, UIViewController destinationVC, bool hasNavBar)
        {
            if (destinationVC.NavigationController != null && hasNavBar)
            {
                var offsetFrame = baseFrame;
                var navBarFrame = destinationVC.NavigationController.NavigationBar.Frame;
                offsetFrame.Y = navBarFrame.Y + navBarFrame.Height;

                return offsetFrame;
            }
            return baseFrame;
        }
    }
}
