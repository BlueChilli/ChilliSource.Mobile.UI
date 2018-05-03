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
using Xamarin.Forms.Platform.iOS;

namespace ChilliSource.Mobile.UI
{
    public static class UIViewExtensions
    {
        /// <summary>
        /// Find the first responder in the <paramref name="view"/>'s subview hierarchy
        /// </summary>
        /// <param name="view">
        /// A <see cref="UIView"/>
        /// </param>
        /// <returns>
        /// A <see cref="UIView"/> that is the first responder or null if there is no first responder
        /// </returns>
        public static UIView FindFirstResponder(this UIView view)
        {
            if (view != null)
            {
                if (view.IsFirstResponder)
                {
                    return view;
                }
                foreach (UIView subView in view.Subviews)
                {
                    var firstResponder = subView.FindFirstResponder();
                    if (firstResponder != null)
                    {
                        return firstResponder;
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Find the first Superview of the specified type (or descendant of)
        /// </summary>
        /// <param name="view">
        /// A <see cref="UIView"/>
        /// </param>
        /// <param name="stopAt">
        /// A <see cref="UIView"/> that indicates where to stop looking up the superview hierarchy
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/> to look for, this should be a UIView or descendant type
        /// </param>
        /// <returns>
        /// A <see cref="UIView"/> if it is found, otherwise null
        /// </returns>
        public static UIView FindSuperviewOfType(this UIView view, UIView stopAt, Type type)
        {
            if (view.Superview != null)
            {
                if (type.IsAssignableFrom(view.Superview.GetType()))
                {
                    return view.Superview;
                }

                if (view.Superview != stopAt)
                    return view.Superview.FindSuperviewOfType(stopAt, type);
            }

            return null;
        }

        /// <summary>
        /// Returns the new view Bottom (Y + Height) coordinates relative to the rootView
        /// </summary>
        /// <returns>The view relative bottom.</returns>
        /// <param name="view">View.</param>
        /// <param name="rootView">Root view.</param>
        public static double GetViewRelativeBottom(this UIView view, UIView rootView)
        {
            var viewRelativeCoordinates = rootView.ConvertPointFromView(view.Frame.Location, view);
            var activeViewRoundedY = Math.Round(viewRelativeCoordinates.Y, 2);

            return activeViewRoundedY + view.Frame.Height;
        }

        /// <summary>
        /// Determines if the UIView is overlapped by the keyboard
        /// </summary>
        /// <returns><c>true</c> if is keyboard overlapping the specified activeView rootView keyboardFrame; otherwise, <c>false</c>.</returns>
        /// <param name="activeView">Active view.</param>
        /// <param name="rootView">Root view.</param>
        /// <param name="keyboardFrame">Keyboard frame.</param>
        public static bool IsKeyboardOverlapping(this UIView activeView, UIView rootView, CGRect keyboardFrame)
        {
            var activeViewBottom = activeView.GetViewRelativeBottom(rootView);
            var pageHeight = rootView.Frame.Height;
            var keyboardHeight = keyboardFrame.Height;

            var isOverlapping = activeViewBottom >= (pageHeight - keyboardHeight);

            return isOverlapping;
        }

        public static UIView ConvertToNative(this Xamarin.Forms.View view, CGRect size)
        {
            var renderer = Platform.CreateRenderer(view);

            renderer.NativeView.Frame = size;
            renderer.NativeView.Hidden = false;
            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsLayout();

            return nativeView;
        }

    }
}
