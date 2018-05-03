#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using CoreGraphics;
using UIKit;


namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Rectangular subtraction path.
    /// </summary>
    public class RectangularSubtractionPath : ISubtractionPath
    {
        /// <summary>
        /// Gets the frame for the rectangular path.
        /// </summary>
        /// <value>A <see cref="CGRect"/> value that represents the frame.</value>
        public CGRect Frame { get; private set; }

        /// <summary>
        /// Gets the bezier path.
        /// </summary>
        /// <value>A <see cref="UIBezierPath"/> that represents the bezier path.</value>
        public UIBezierPath BezierPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of this <c>RectangularSubtractionPath</c> class.
        /// </summary>
        /// <param name="frame">Frame.</param>
        /// <param name="horizontalPadding">Horizontal padding.</param>
        /// <param name="vertialPadding">Vertial padding.</param>
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
