#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Views;
using Android.Graphics;
using Android.Content;

namespace ChilliSource.Mobile.UI
{
	public class ShapeNativeView : View
	{
		readonly float _pixelDensity;

		public ShapeNativeView(Context context, ShapeType shapeType, Color fillColor, Color borderColor, int borderWidth = 1, int cornerRadius = 0) : base(context)
		{
			BorderColor = borderColor;
			BorderWidth = borderWidth;
			CornerRadius = cornerRadius;
			ShapeType = shapeType;
			FillColor = fillColor;

			_pixelDensity = Resources.DisplayMetrics.Density;
		}

		public ShapeType ShapeType { get; set; }
		public Color BorderColor { get; set; }
		public Color FillColor { get; set; }
		public int BorderWidth { get; set; }
		public int CornerRadius { get; set; }

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
			var x = GetX();
			var y = GetY();
			var width = Width;
			var height = Height;
			var cx = width / 2f;
			var cy = height / 2f;
			var strokeWidth = 0f;

			Paint strokePaint = null;

			if (BorderWidth > 0 && BorderColor.A > 0)
			{
				strokeWidth = Resize(BorderWidth);

				strokePaint = new Paint(PaintFlags.AntiAlias);
				strokePaint.SetStyle(Paint.Style.Stroke);
				strokePaint.StrokeWidth = strokeWidth;
				strokePaint.StrokeCap = Paint.Cap.Round;
				strokePaint.Color = BorderColor;


				x += strokeWidth / 2f;
				y += strokeWidth / 2f;
				width -= (int)strokeWidth;
				height -= (int)strokeWidth;
			}

			Paint fillPaint = null;

			if (FillColor.A > 0)
			{
				fillPaint = new Paint(PaintFlags.AntiAlias);
				fillPaint.SetStyle(Paint.Style.Fill);
				fillPaint.Color = FillColor;
			}

			switch (ShapeType)
			{
				case ShapeType.Rectangle:
					{
						DrawBox(canvas, x, y, width, height, CornerRadius, fillPaint, strokePaint);
						break;
					}
				case ShapeType.Circle:
					{
						DrawCircle(canvas, cx, cy, Math.Min(height, width) / 2f, fillPaint, strokePaint);
						break;
					}

				case ShapeType.Triangle:
					{
						DrawTriangle(canvas, width, height, fillPaint, strokePaint);
						break;
					}
			}
		}

		protected virtual void DrawBox(Canvas canvas, float left, float top, float width, float height, float cornerRadius, Paint fillPaint, Paint strokePaint)
		{
			var rect = new RectF(left, top, left + width, top + height);
			if (cornerRadius > 0)
			{
				var resizedCornerRadius = this.Resize(cornerRadius);
				if (fillPaint != null)
				{
					canvas.DrawRoundRect(rect, resizedCornerRadius, resizedCornerRadius, fillPaint);
				}
				if (strokePaint != null)
				{
					canvas.DrawRoundRect(rect, resizedCornerRadius, resizedCornerRadius, strokePaint);
				}
			}
			else
			{
				if (fillPaint != null)
				{
					canvas.DrawRect(rect, fillPaint);
				}
				if (strokePaint != null)
				{
					canvas.DrawRect(rect, strokePaint);
				}
			}
		}

		protected virtual void DrawCircle(Canvas canvas, float cx, float cy, float radius, Paint fillPaint, Paint strokePaint)
		{
			if (fillPaint != null)
			{
				canvas.DrawCircle(cx, cy, radius, fillPaint);
			}

			if (strokePaint != null)
			{
				canvas.DrawCircle(cx, cy, radius, strokePaint);
			}
		}

		protected virtual void DrawTriangle(Canvas canvas, float width, float height, Paint fillPaint, Paint strokePaint)
		{
			var topPoint = new Point((int)(width / 2), 0);
			var rightPoint = new Point((int)width, (int)height);
			var leftPoint = new Point(0, (int)height);

			var trianglePath = new Path();
			trianglePath.SetFillType(Path.FillType.EvenOdd);
			trianglePath.MoveTo(topPoint.X, topPoint.Y);
			trianglePath.LineTo(rightPoint.X, rightPoint.Y);
			trianglePath.LineTo(leftPoint.X, leftPoint.Y);
			trianglePath.LineTo(topPoint.X, topPoint.Y);
			trianglePath.Close();

			if (fillPaint != null)
			{
				canvas.DrawPath(trianglePath, fillPaint);
			}

			if (strokePaint != null)
			{
				canvas.DrawPath(trianglePath, strokePaint);
			}
		}

		float Resize(float input)
		{
			return input * _pixelDensity;
		}
	}
}
