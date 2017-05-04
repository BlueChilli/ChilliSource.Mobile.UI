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
    public class CircularSubtractionPath : ISubtractionPath
    {
        public CGRect Frame { get; private set; }
        public UIBezierPath BezierPath { get; private set; }

        public CircularSubtractionPath(CGRect frame, float radius = 0)
        {
            Frame = frame;

            var rect = new CGRect(Frame.GetMidX() - radius, Frame.GetMidY() - radius, 2 * radius, 2 * radius);
            BezierPath = UIBezierPath.FromOval(rect);
        }
    }
}
