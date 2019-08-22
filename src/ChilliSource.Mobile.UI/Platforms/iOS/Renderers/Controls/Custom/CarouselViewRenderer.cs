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
using System.Threading;

[assembly: ExportRenderer(typeof(ChilliSource.Mobile.UI.CarouselView), typeof(ChilliSource.Mobile.UI.CarouselViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class CarouselViewRenderer : ScrollViewRenderer
	{
        UIScrollView _scrollView;
        bool _isDisposing;
       
		public CarouselViewRenderer()
		{
			PagingEnabled = true;
			ShowsHorizontalScrollIndicator = false;
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _isDisposing = true;                
                _scrollView = null;
            }

            base.Dispose(disposing);
        }

        public CarouselView CarouselView => Element as CarouselView;
       
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				return;
			}

			_scrollView = (UIScrollView)NativeView;

            if (_scrollView != null)
            {
                _scrollView.Scrolled += OnNativeScrolled;
            }

            if (e.NewElement != null)
            {

                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }

            if (CarouselView != null)
            {
                CarouselView.ScrollRequested += OnScrollRequested;
            }
        }

        private void OnScrollRequested(double animationDuration)
        {
            if (CarouselView == null)
            {
                return;
            }
            ScrollToSelection(CarouselView.SelectedIndex + 1, true, animationDuration);
        }

        void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            if (_isDisposing)
            {
                return;
            }

            if (CarouselView == null)
            {
                return;
            }

			if (e.PropertyName == CarouselView.SelectedIndexProperty.PropertyName && !Dragging)
			{
				ScrollToSelection(CarouselView.SelectedIndex, false);
			}
		}

       
        void OnNativeScrolled(object sender, EventArgs e)
        {
            if (_scrollView == null || CarouselView == null)
            {
                return;
            }

            var center = _scrollView.ContentOffset.X + (_scrollView.Bounds.Width / 2);

            if (center > 0)
            {
                CarouselView.SelectedIndex = ((int)center) / ((int)_scrollView.Bounds.Width);
            }
            else
            {
                CarouselView.SelectedIndex = 0;
            }
        }

       
		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
            if (CarouselView != null)
            {
                ScrollToSelection(CarouselView.SelectedIndex, false);
            }
		}


        void ScrollToSelection(int index, bool animate, double animationDuration = 0.5)
        {
            if (Element == null)
            {
                return;
            }

            if (animate)
            {
                UIView.BeginAnimations(null);
                UIView.SetAnimationDuration(animationDuration);
            }

            if (_scrollView != null)
            {

                _scrollView.SetContentOffset(new CoreGraphics.CGPoint
                    (_scrollView.Bounds.Width * Math.Max(0, index), _scrollView.ContentOffset.Y),
                    false);
            }
            if (animate)
            {
                UIView.CommitAnimations();
            }
        }
    }
}

