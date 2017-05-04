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
    public class DashedLineNativeView : UIView
    {
        public DashedLineNativeView()
        {
            BackgroundColor = UIColor.White;
        }

        public UIColor LineColor { get; set; } = UIColor.LightGray;
        public float DashWidth { get; set; } = 4;
        public float DashSpaceWidth { get; set; } = 4;

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var context = UIGraphics.GetCurrentContext();
            context.SetLineWidth(Bounds.Height);
            context.SetStrokeColor(LineColor.CGColor);
            context.SetLineDash(0, new nfloat[] { DashSpaceWidth, DashWidth }, 2);

            context.MoveTo(0, Bounds.Height);
            context.AddLineToPoint(Bounds.Width, Bounds.Height);

            context.StrokePath();
            context.ClosePath();
        }
    }
}
