#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(CarouselView), typeof(CarouselViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class CarouselViewRenderer : ScrollViewRenderer
	{
		UIScrollView _native;

		public CarouselViewRenderer()
		{
			PagingEnabled = true;
			ShowsHorizontalScrollIndicator = false;
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				return;
			}

			_native = (UIScrollView)NativeView;
			_native.Scrolled += NativeScrolled;
			e.NewElement.PropertyChanged += ElementPropertyChanged;
		}

		void NativeScrolled(object sender, EventArgs e)
		{
			var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
			((CarouselView)Element).SelectedIndex = ((int)center) / ((int)_native.Bounds.Width);
		}

		void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == CarouselView.SelectedIndexProperty.PropertyName && !Dragging)
			{
				ScrollToSelection(false);
			}
		}

		void ScrollToSelection(bool animate)
		{
			if (Element == null)
			{
				return;
			}

			_native.SetContentOffset(new CoreGraphics.CGPoint
				(_native.Bounds.Width *
					Math.Max(0, ((CarouselView)Element).SelectedIndex),
					_native.ContentOffset.Y),
				animate);
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
			ScrollToSelection(false);
		}
	}
}

