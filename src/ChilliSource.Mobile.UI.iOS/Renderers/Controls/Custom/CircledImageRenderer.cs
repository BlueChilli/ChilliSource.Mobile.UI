#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(CircledImage), typeof(CircledImageRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class CircledImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
				return;

			CreateCircle();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				e.PropertyName == VisualElement.WidthProperty.PropertyName)
			{
				CreateCircle();
			}
		}

		private void CreateCircle()
		{
			try
			{
				double min = Math.Min(Element.Width, Element.Height);
				Control.Layer.CornerRadius = (float)(min / 2.0);
				Control.Layer.MasksToBounds = false;
				Control.ClipsToBounds = true;

				var image = Element as CircledImage;
				var borderWidth = (float)image.BorderWidth;

				Control.Layer.BorderWidth = (nfloat)image.BorderWidth;
				Control.Layer.BorderColor = image.BorderColor.ToUIColor().CGColor;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Unable to create circle image: " + ex);
			}
		}
	}
}

