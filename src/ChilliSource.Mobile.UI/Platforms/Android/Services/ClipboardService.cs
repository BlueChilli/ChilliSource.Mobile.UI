#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Android.Content;
using ChilliSource.Mobile.UI;

[assembly: Dependency(typeof(ClipboardService))]

namespace ChilliSource.Mobile.UI
{
	public class ClipboardService : IClipboardService
	{
        private Context Context  => Android.App.Application.Context;

		public void CopyToClipboard(String text)
		{
			Context context = Context;
			ClipboardManager clipboard = (ClipboardManager)context.GetSystemService(Context.ClipboardService);
			var clip = ClipData.NewPlainText("copied text", text);
			clipboard.PrimaryClip = clip;
		}
	}
}

