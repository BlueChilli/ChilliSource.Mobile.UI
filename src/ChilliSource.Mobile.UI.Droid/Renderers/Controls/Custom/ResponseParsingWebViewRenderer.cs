#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
 * Based on:
 * Source:  CookiesWebView (https://github.com/seansparkman/CookiesWebView)
 * Author:  Sean Sparkman (https://github.com/seansparkman)
 * License: MIT (https://github.com/seansparkman/CookiesWebView/blob/master/License.md)
*/

using System;
using Android.Webkit;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ResponseParsingWebView), typeof(ResponseParsingWebViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ResponseParsingWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Control.SetWebViewClient(new CookieWebViewClient(ResponseParsingWebView));
			}
		}

		public ResponseParsingWebView ResponseParsingWebView
		{
			get { return Element as ResponseParsingWebView; }
		}

		internal class CookieWebViewClient : WebViewClient
		{
			private readonly ResponseParsingWebView _responseParsingWebView;

			internal CookieWebViewClient(ResponseParsingWebView webView)
			{
				_responseParsingWebView = webView;
			}

			public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
			{
				view.EvaluateJavascript(_responseParsingWebView.HtmlElementName, new ValueCallback(_responseParsingWebView));
			}
		}

		internal class ValueCallback : Java.Lang.Object, IValueCallback
		{
			private readonly ResponseParsingWebView _customWebView;
			public ValueCallback(ResponseParsingWebView customWebView)
			{
				_customWebView = customWebView;
			}

			public void OnReceiveValue(Java.Lang.Object value)
			{
				_customWebView.OnResponseHtmlTitleReceived(value.ToString());
			}
		}
	}
}
