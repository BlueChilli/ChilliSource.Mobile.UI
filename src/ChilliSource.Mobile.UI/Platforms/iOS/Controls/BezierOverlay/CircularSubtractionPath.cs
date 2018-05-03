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
    public class CircularSubtractionPath : ISubtractionPath
    {
        /// <summary>
        /// Gets the frame for the circular path.
        /// </summary>
        /// <value>A <see cref="CGRect"/> value that represents the frame.</value>
        public CGRect Frame { get; private set; }

        /// <summary>
        /// Gets the bezier path.
        /// </summary>
        /// <value>A <see cref="UIBezierPath"/> that represents the bezier path.</value>
        public UIBezierPath BezierPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of this <c>CircularSubtractionPath</c> class.
        /// </summary>
        /// <param name="frame">Frame.</param>
        /// <param name="radius">Radius.</param>
        public CircularSubtractionPath(CGRect frame, float radius = 0)
        {
            Frame = frame;

            var rect = new CGRect(Frame.GetMidX() - radius, Frame.GetMidY() - radius, 2 * radius, 2 * radius);
            BezierPath = UIBezierPath.FromOval(rect);
        }
    }
}
