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
	public class SeparatorNativeView : UIView
    {
        double _width;
        double _height;
        double _thickness;
        double _spacingBefore;
        double _spacingAfter;
        UIColor _strokeColor;
        StrokeType _strokeType;
        SeparatorOrientation _orientation;

        /// <summary>
        /// Gets or sets the width of the seperator.
        /// </summary>
        public double Width
        {
            set
            {
                _width = value;
            }
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets or sets the height of the seperator.
        /// </summary>
        public double Height
        {
            set
            {
                _height = value;
            }
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Gets or sets the thickness of the seperator.
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness
        {
            set
            {
                _thickness = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _thickness;
            }
        }

        /// <summary>
        /// Gets or sets the padding before the separator.
        /// </summary>
        public double SpacingBefore
        {
            set
            {
                _spacingBefore = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _spacingBefore;
            }
        }

        /// <summary>
        /// Gets or sets the amount of space after the separator.
        /// </summary>
        public double SpacingAfter
        {
            set
            {
                _spacingAfter = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _spacingAfter;
            }
        }

        /// <summary>
        /// Gets or sets the color of the stroke.
        /// </summary>
        ///  <value>A <see cref="UIColor"/> that represents the color of the stroke.</value>
        public UIColor StrokeColor
        {
            set
            {
                _strokeColor = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _strokeColor;
            }
        }

        /// <summary>
        /// Gets or sets the type of the stroke.
        /// </summary>
        ///  <value>A <see cref="StrokeType"/>that represents the type of the stroke.</value>
        public StrokeType StrokeType
        {
            set
            {
                _strokeType = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _strokeType;
            }
        }

        /// <summary>
        /// Gets or sets the orientation of the seperator view.
        /// </summary>
        /// <value>A <see cref="SeparatorOrientation"/> that represents the orientation.</value>
        public SeparatorOrientation Orientation
        {
            set
            {
                _orientation = value;
                SetNeedsDisplayInRect(Bounds);
            }
            get
            {
                return _orientation;
            }
        }

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        /// <param name="bounds">Bounds.</param>
        public SeparatorNativeView(CGRect bounds) : base(bounds)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        /// <param name="handle">Handle.</param>
        public SeparatorNativeView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        public SeparatorNativeView()
        {
            Initialize();
        }

        /// <summary>
        /// Overrides the <see cref="UIView.Draw"/> method to draw the shape view within the passed-in <paramref name="rect"/>.
        /// </summary>
        /// <param name="rect">The rectangle to draw.</param>
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var height = Bounds.Size.Height;
            var context = UIGraphics.GetCurrentContext();

            context.ClearRect(rect);
            context.SetStrokeColor(StrokeColor.CGColor);
            switch (StrokeType)
            {
                case StrokeType.Dashed:
                    {
                        context.SetLineDash(0, new nfloat[] { 6, 2 });
                        break;
                    }
                case StrokeType.Dotted:
                    {
                        context.SetLineDash(0, new nfloat[] { (nfloat)Thickness, (nfloat)Thickness });
                        break;
                    }
            }

            context.SetLineWidth((float)Thickness);
            var desiredTotalSpacing = SpacingAfter + SpacingBefore;

            float leftForSpacing = 0;
            float actualSpacingBefore = 0;

            if (Orientation == SeparatorOrientation.Horizontal)
            {
                leftForSpacing = (float)Bounds.Size.Height - (float)Thickness;
            }
            else
            {
                leftForSpacing = (float)Bounds.Size.Width - (float)Thickness;
            }
            if (desiredTotalSpacing > 0)
            {
                float spacingCompressionRatio = (float)(leftForSpacing / desiredTotalSpacing);
                actualSpacingBefore = (float)SpacingBefore * spacingCompressionRatio;
            }
            else
            {
                actualSpacingBefore = 0;
            }

            float thicknessOffset = (float)Thickness / 2.0f;

            if (Orientation == SeparatorOrientation.Horizontal)
            {
                context.MoveTo(0, actualSpacingBefore + thicknessOffset);
                context.AddLineToPoint(rect.Width, actualSpacingBefore + thicknessOffset);
            }
            else
            {
                context.MoveTo(actualSpacingBefore + thicknessOffset, 0);
                context.AddLineToPoint(actualSpacingBefore + thicknessOffset, rect.Height);
            }
            context.StrokePath();
            SizeToFit();
        }


        void Initialize()
        {
            BackgroundColor = UIColor.Clear;
            Opaque = false;
        }
    }
}
