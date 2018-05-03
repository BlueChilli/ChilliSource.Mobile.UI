#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreAnimation;
using UIKit;
namespace ChilliSource.Mobile.UI
{
    public static class TransitionHelper
    {
        public static CATransform3D BuildYRotationTransform(float angle)
        {
            return CATransform3D.MakeRotation(angle, 0.0f, 1.0f, 0.0f);
        }

        public static CATransform3D BuildXRotationTransform(float angle)
        {            
            return CATransform3D.MakeRotation(angle, 1f, 0f, 0f);            
        }

        public static void SetPerspectiveTransform(UIView containerView)
        {
            var transform = CATransform3D.Identity;
            transform.m34 = -0.002f;
            containerView.Layer.SublayerTransform = transform;
        }
    }
}
