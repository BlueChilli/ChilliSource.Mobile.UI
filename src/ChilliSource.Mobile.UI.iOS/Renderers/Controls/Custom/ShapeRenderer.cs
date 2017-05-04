#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShapeView), typeof(ShapeRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ShapeRenderer : ViewRenderer<ShapeView, ShapeNativeView>
	{
		ShapeNativeView _shapeView;

		protected override void OnElementChanged(ElementChangedEventArgs<ShapeView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
			{
				return;
			}

			_shapeView = new ShapeNativeView(Element.ShapeType, Element.FillColor.ToUIColor(),
									   Element.BorderColor.ToUIColor(), Element.BorderWidth, Element.CornerRadius);
			SetNativeControl(_shapeView);
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Element == null)
			{
				return;
			}

			switch (e.PropertyName)
			{
				case nameof(Element.ShapeType):
				case nameof(Element.FillColor):
				case nameof(Element.BorderColor):
				case nameof(Element.BorderWidth):
				case nameof(Element.CornerRadius):
					{
						_shapeView.ShapeType = Element.ShapeType;
						_shapeView.FillColor = Element.FillColor.ToUIColor();
						_shapeView.BorderColor = Element.BorderColor.ToUIColor();
						_shapeView.BorderWidth = Element.BorderWidth;
						_shapeView.CornerRadius = Element.CornerRadius;

						_shapeView.SetNeedsDisplay();
						break;
					}
			}
		}
	}
}
