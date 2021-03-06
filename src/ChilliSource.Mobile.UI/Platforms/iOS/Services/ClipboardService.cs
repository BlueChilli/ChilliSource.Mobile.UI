#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;

[assembly: Dependency(typeof(ClipboardService))]

namespace ChilliSource.Mobile.UI
{
	public class ClipboardService : IClipboardService
	{
		public void CopyToClipboard(String text)
		{
			UIPasteboard clipboard = UIPasteboard.General;
			clipboard.String = text;
		}
	}
}

