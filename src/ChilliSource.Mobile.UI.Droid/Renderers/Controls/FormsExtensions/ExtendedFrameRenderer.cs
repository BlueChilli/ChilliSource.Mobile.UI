#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Graphics;
using Android.Util;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedFrame), typeof(ExtendedFrameRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedFrameRenderer : FrameRenderer
	{
		private float _cornerRadius;
		private RectF _bounds;
		private Path _path;

		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				return;
			}

			var element = (ExtendedFrame)Element;

			_cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, element.CornerRadius,
				Context.Resources.DisplayMetrics);
		}

		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged(w, h, oldw, oldh);
			if (w != oldw && h != oldh)
			{
				_bounds = new RectF(0, 0, w, h);
			}

			_path = new Path();
			_path.Reset();
			_path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
			_path.Close();
		}

		public override void Draw(Canvas canvas)
		{
			canvas.Save();
			canvas.ClipPath(_path);
			base.Draw(canvas);
			DrawOutline(canvas, canvas.Width, canvas.Height, _cornerRadius);
			canvas.Restore();
		}

		void DrawOutline(Canvas canvas, int width, int height, float cornerRadius)
		{
			using (var paint = new Paint { AntiAlias = true })
			using (var path = new Path())
			using (Path.Direction direction = Path.Direction.Cw)
			using (Paint.Style style = Paint.Style.Stroke)
			using (var rect = new RectF(0, 0, width, height))
			{
				float rx = Forms.Context.ToPixels(cornerRadius);
				float ry = Forms.Context.ToPixels(cornerRadius);
				path.AddRoundRect(rect, rx, ry, direction);

				paint.StrokeWidth = 2f;
				paint.SetStyle(style);
				paint.Color = Element.OutlineColor.ToAndroid();
				canvas.DrawPath(path, paint);
			}
		}
	}
}
