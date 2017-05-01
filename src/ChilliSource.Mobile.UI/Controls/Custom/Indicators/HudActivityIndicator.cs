#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.Core;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class HudActivityIndicator : IActivityIndicator
	{
		IHudService _hudService;

		public HudActivityIndicator()
		{
			_hudService = DependencyService.Get<IHudService>();
		}

		public void Hide()
		{
			_hudService.Dismiss();
		}

		public void Show()
		{
			_hudService.Show();
		}

		public void Show(string text)
		{
			_hudService.Show(text);
		}
	}
}
