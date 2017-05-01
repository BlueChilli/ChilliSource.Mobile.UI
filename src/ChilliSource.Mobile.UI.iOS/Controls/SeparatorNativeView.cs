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
using Xamarin.Forms;

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

		public SeparatorNativeView(CGRect bounds) : base(bounds)
		{
			Initialize();
		}

		public SeparatorNativeView(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		public SeparatorNativeView()
		{
			Initialize();
		}

		void Initialize()
		{
			BackgroundColor = UIColor.Clear;
			Opaque = false;
		}

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
	}
}
