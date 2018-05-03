#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using UIKit;
using CoreAnimation;

namespace ChilliSource.Mobile.UI
{    
    public class BezierOverlayNativeView : UIView
    {
        private List<UIBezierPath> _paths;
        private bool _allowTouches;

        /// <summary>
        /// Initializes a new instance of this <c>BezierOverlayNativeView</c> class.
        /// </summary>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="allowTouches">If set to <c>true</c> touches are allowed.</param>
        public BezierOverlayNativeView(UIColor backgroundColor, bool allowTouches = false)
        {
            _paths = new List<UIBezierPath>();
            _allowTouches = allowTouches;

            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Updates a list of bezier <paramref name="paths"/>.
        /// </summary>
        /// <param name="paths">A <see cref="List{T}"/> of bezier paths.</param>
        public void UpdatePaths(List<ISubtractionPath> paths)
        {
            _paths.Clear();

            UpdateLayer();
            SubtractFromView(paths);
        }

        /// <summary>
        /// Updates the specified bezier <paramref name="path"/>.
        /// </summary>
        /// <param name="path">Path.</param>
        public void UpdatePath(ISubtractionPath path)
        {
            UpdatePaths(new List<ISubtractionPath>() { path });
        }

        /// <summary>
        /// Overrides the <see cref="UIView.PointInside"/> method to identify whether a specified <paramref name="point"/> is within the view's bounds.
        /// </summary>
        /// <returns><c>true</c>, if the point is inside, <c>false</c> otherwise.</returns>
        /// <param name="point">Point.</param>
        /// <param name="uievent">The event that triggered this method call.</param>
        public override bool PointInside(CoreGraphics.CGPoint point, UIEvent uievent)
        {
            if (_allowTouches)
            {
                foreach (var path in _paths)
                {
                    if (path.ContainsPoint(point))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return base.PointInside(point, uievent);
            }
        }

        void UpdateLayer()
        {
            var bounds = Bounds;

            var maskLayer = new CAShapeLayer();
            maskLayer.Frame = bounds;
            maskLayer.FillColor = UIColor.Black.CGColor;

            var path = UIBezierPath.FromRect(bounds);
            maskLayer.Path = path.CGPath;
            maskLayer.FillRule = CAShapeLayer.FillRuleEvenOdd;

            Layer.Mask = maskLayer;
        }

        void SubtractFromView(List<ISubtractionPath> paths)
        {
            var layer = Layer.Mask as CAShapeLayer;
            var oldPath = layer.Path;

            var newPath = UIBezierPath.FromPath(oldPath);
            foreach (var path in paths)
            {
                _paths.Add(path.BezierPath);
                newPath.AppendPath(path.BezierPath);
            }

            layer.Path = newPath.CGPath;
            Layer.Mask = layer;
        }

    }






}
