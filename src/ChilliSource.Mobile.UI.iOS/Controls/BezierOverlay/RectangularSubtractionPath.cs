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
    public class RectangularSubtractionPath : ISubtractionPath
    {
        public CGRect Frame { get; private set; }
        public UIBezierPath BezierPath { get; private set; }

        public RectangularSubtractionPath(CGRect frame, float horizontalPadding = 0, float vertialPadding = 0)
        {
            Frame = frame;

            var rect = frame;
            rect.X -= horizontalPadding;
            rect.Y -= vertialPadding;

            rect.Width += 2 * horizontalPadding;
            rect.Height += 2 * vertialPadding;

            BezierPath = UIBezierPath.FromRect(rect);
        }
    }
}
