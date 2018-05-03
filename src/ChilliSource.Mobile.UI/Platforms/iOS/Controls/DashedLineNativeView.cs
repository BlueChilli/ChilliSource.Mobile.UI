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
        /// <summary>
        /// Initializes a new instance of this <c>DashedLineNativeView</c> class.
        /// </summary>
        public DashedLineNativeView()
        {
            BackgroundColor = UIColor.White;
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>A <see cref="UIColor"/> value that represents the line color. The default value is <see cref="UIColor.LightGray"/>.</value>
        public UIColor LineColor { get; set; } = UIColor.LightGray;

        /// <summary>
        /// Gets or sets the width of the dash.
        /// </summary>
        /// <value>The width of the dash; the default is 4.</value>
        public float DashWidth { get; set; } = 4;

        /// <summary>
        /// Gets or sets the amount of the space between dashes.
        /// </summary>
        /// <value>The width of the space; the default is 4.</value>
        public float DashSpaceWidth { get; set; } = 4;

        /// <summary>
        /// Overrides the <see cref="UIView.Draw"/> method to draw the shape view within the passed-in <paramref name="rect"/>.
        /// </summary>
        /// <param name="rect">The rectangle to draw.</param>
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
