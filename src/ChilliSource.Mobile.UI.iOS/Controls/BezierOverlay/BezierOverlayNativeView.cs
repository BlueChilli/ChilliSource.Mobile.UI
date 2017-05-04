#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using UIKit;
using CoreAnimation;
using CoreGraphics;

namespace ChilliSource.Mobile.UI
{
    public class BezierOverlayNativeView : UIView
    {
        private List<UIBezierPath> _paths;
        private bool _allowTouches;

        public BezierOverlayNativeView(UIColor backgroundColor, bool allowTouches = false)
        {
            _paths = new List<UIBezierPath>();
            _allowTouches = allowTouches;

            BackgroundColor = backgroundColor;
        }

        public void UpdatePaths(List<ISubtractionPath> paths)
        {
            _paths.Clear();

            UpdateLayer();
            SubtractFromView(paths);
        }

        public void UpdatePath(ISubtractionPath path)
        {
            UpdatePaths(new List<ISubtractionPath>() { path });
        }

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
