#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// <see cref="EventArgs"/> to store the HTML response data for <see cref="ResponseParsingWebView"/>.
    /// </summary>
    public class HtmlResponseEventArgs : EventArgs
    {
        public HtmlResponseEventArgs(string htmlResponse)
        {
            HtmlResponse = htmlResponse;
        }

        public string HtmlResponse
        {
            get;
            private set;
        }
    }
}
