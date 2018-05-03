using System;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]

namespace ChilliSource.Mobile.UI
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    {
        ExtendedWebView _extendedWebView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {

                _extendedWebView = e.NewElement as ExtendedWebView;

                if (_extendedWebView.IsTransparent)
                {
                    BackgroundColor = UIColor.Clear;
                    Opaque = false;
                }


            }
        }
    }
}

