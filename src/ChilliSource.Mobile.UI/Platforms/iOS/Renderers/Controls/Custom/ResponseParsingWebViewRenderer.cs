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
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ResponseParsingWebView), typeof(ResponseParsingWebViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ResponseParsingWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				WeakDelegate = new WebViewDelegate(ResponseParsingWebView);
			}
		}

		public ResponseParsingWebView ResponseParsingWebView
		{
			get { return Element as ResponseParsingWebView; }
		}

		private class WebViewDelegate : UIWebViewDelegate
		{
			ResponseParsingWebView _responseParsingWebView;

			public WebViewDelegate(ResponseParsingWebView customWebView)
			{
				_responseParsingWebView = customWebView;
			}

			public override void LoadingFinished(UIWebView webView)
			{
				String html = webView.EvaluateJavascript(_responseParsingWebView.HtmlElementName);
				_responseParsingWebView.OnResponseHtmlTitleReceived(html);
			}
		}
	}
}

