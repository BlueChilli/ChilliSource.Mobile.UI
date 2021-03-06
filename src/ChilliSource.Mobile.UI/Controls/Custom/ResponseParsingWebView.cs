#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// <c>WebView</c> that can be used to retrieve a specific Html element from the response.
    /// </summary>
    /// <example>
    /// Example usage for a Google login page that checks if the document title Html element contains "Success code".
    /// <code>
    /// var webView = new ResponseParsingWebView();
    /// webView.ResponseHtmlTitleReceived += WebView_HtmlResultReceived;
    /// webView.HtmlElementName = "document.title";
    /// webView.Source = googleAuthenticationUrl;
    /// 
    /// void WebView_HtmlResultReceived(object sender, HtmlResponseEventArgs e)
    /// {
    ///     string html = e.HtmlResponse;
    ///
    ///     if (html.StartsWith("\"Success code", StringComparison.InvariantCulture))
    ///     {
    ///         OnGoogleLoginCompleted?.Invoke();
    ///     }
    /// </code>
    /// </example>
    public class ResponseParsingWebView : WebView
    {
        /// <summary>
        /// Occurs when the title of the html response is received.
        /// </summary>
        public event EventHandler<HtmlResponseEventArgs> ResponseHtmlTitleReceived;

        /// <summary>
        /// Gets or sets the name of the html element.
        /// </summary>
        public string HtmlElementName { get; set; }

        /// <summary>
        /// Method that raises the <c>ResponseHtmlTitleReceived</c> event.
        /// </summary>
        /// <param name="responseHtmlTitle">Response html title.</param>
        public void OnResponseHtmlTitleReceived(string responseHtmlTitle)
        {
            ResponseHtmlTitleReceived?.Invoke(this, new HtmlResponseEventArgs(responseHtmlTitle));
        }
    }
}
