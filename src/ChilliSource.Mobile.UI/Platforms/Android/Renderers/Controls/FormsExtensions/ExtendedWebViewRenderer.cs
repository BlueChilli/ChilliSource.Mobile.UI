using System;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]

namespace ChilliSource.Mobile.UI
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    {
        ExtendedWebView _extendedWebView;

        public ExtendedWebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {

                _extendedWebView = e.NewElement as ExtendedWebView;

                if (_extendedWebView.IsTransparent)
                {
                    this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }


            }
        }

    }
}

