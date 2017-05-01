#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Interfaces.Animations;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class AdvancedAlertViewAnimation : IPopupAnimation
	{
		public void Preparing(View content, PopupPage page)
		{
			//Not used
		}

		public async Task Appearing(View content, PopupPage page)
		{
			content.Opacity = 0;
			await content.FadeTo(1);
		}

		public async Task Disappearing(View content, PopupPage page)
		{
			await content.FadeTo(0);
		}

		public void Disposing(View content, PopupPage page)
		{
			//Not used
		}
	}
}
