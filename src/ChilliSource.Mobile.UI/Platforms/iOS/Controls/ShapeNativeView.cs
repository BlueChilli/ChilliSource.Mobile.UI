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
using System.Drawing;

namespace ChilliSource.Mobile.UI
{    
	public class ShapeNativeView : UIView
    {
        /// <summary>
        /// Initializes a new instance of the <c>ShapeNativeView</c> class.
        /// </summary>
        /// <param name="shapeType">Shape type.</param>
        /// <param name="fillColor">Fill color.</param>
        /// <param name="borderColor">Border color.</param>
        /// <param name="borderWidth">Border width.</param>
        /// <param name="cornerRadius">Corner radius.</param>
        public ShapeNativeView(ShapeType shapeType, UIColor fillColor, UIColor borderColor, int borderWidth = 1, int cornerRadius = 0)
        {
            BorderColor = borderColor;
            BorderWidth = borderWidth;
            CornerRadius = cornerRadius;
            ShapeType = shapeType;
            FillColor = fillColor;
            BackgroundColor = UIColor.Clear;
        }

        /// <summary>
        /// Gets or sets the type of the shape.
        /// </summary>
        /// <value>A <see cref="ShapeType"/> that represents the type of the shape.</value>
        public ShapeType ShapeType { get; set; }

        /// <summary>
        /// Gets or sets the border color of the shape.
        /// </summary>
        /// <value>A <see cref="UIColor"/> value that represents the color of the border.</value>
        public UIColor BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the color to fill the shape.
        /// </summary>
        /// <value>A <see cref="UIColor"/> value that fills the shape.</value>
        public UIColor FillColor { get; set; }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the corner radius for the shape view.
        /// </summary>
        public int CornerRadius { get; set; }


        /// <summary>
        /// Overrides the <see cref="UIView.Draw"/> method to draw the shape view within the passed-in <paramref name="rect"/>.
        /// </summary>
        /// <param name="rect">The rectangle to draw.</param>
        public override void Draw(CGRect rect)
        {
            float x = (float)rect.X;
            float y = (float)rect.Y;
            float width = (float)rect.Width;
            float height = (float)rect.Height;
            var cx = width / 2f;
            var cy = height / 2f;

            var context = UIGraphics.GetCurrentContext();

            var hasFill = false;
            var hasStroke = false;

            var strokeWidth = 0f;

            if (BorderWidth > 0 && BorderColor.CGColor.Alpha > 0)
            {
                context.SetLineWidth(BorderWidth);
                BorderColor.SetStroke();

                hasStroke = true;
                strokeWidth = BorderWidth;

                x += strokeWidth / 2f;
                y += strokeWidth / 2f;
                width -= strokeWidth;
                height -= strokeWidth;
            }

            if (FillColor.CGColor.Alpha > 0)
            {
                FillColor.SetFill();
                hasFill = true;
            }

            switch (ShapeType)
            {
                case ShapeType.Rectangle:
                    {
                        DrawBox(context, x, y, width, height, CornerRadius, hasFill, hasStroke);
                        break;
                    }
                case ShapeType.Circle:
                    {
                        DrawCircle(context, cx, cy, Math.Min(height, width) / 2f, hasFill, hasStroke);
                        break;
                    }

                case ShapeType.Triangle:
                    {
                        DrawTriangle(context, rect, hasFill, hasStroke);
                        break;
                    }
            }
        }


        protected virtual void DrawBox(CGContext context, float x, float y, float width, float height, float cornerRadius, bool fill, bool stroke)
        {
            var rect = new RectangleF(x, y, width, height);
            if (cornerRadius > 0)
            {
                context.AddPath(UIBezierPath.FromRoundedRect(rect, cornerRadius).CGPath);
            }
            else
            {
                context.AddRect(rect);
            }

            DrawPath(context, fill, stroke);
        }

        protected virtual void DrawCircle(CGContext context, float cx, float cy, float radius, bool fill, bool stroke)
        {
            context.AddArc(cx, cy, radius, 0, (float)Math.PI * 2, true);
            DrawPath(context, fill, stroke);
        }

        protected virtual void DrawTriangle(CGContext context, CGRect rect, bool fill, bool stroke)
        {
            var trianglePath = new CGPath();

            trianglePath.MoveToPoint(rect.Width / 2, 0);
            trianglePath.AddLineToPoint(rect.Size.Width, rect.Size.Height);
            trianglePath.AddLineToPoint(0, rect.Size.Height);
            trianglePath.AddLineToPoint(rect.Width / 2, 0);

            context.AddPath(trianglePath);
            context.Clip();
            context.AddPath(trianglePath);

            DrawPath(context, fill, stroke);
        }

        void DrawPath(CGContext context, bool fill, bool stroke)
        {
            if (fill && stroke)
            {
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
            else if (fill)
            {
                context.DrawPath(CGPathDrawingMode.Fill);
            }
            else if (stroke)
            {
                context.DrawPath(CGPathDrawingMode.Stroke);
            }
        }
    }
}
