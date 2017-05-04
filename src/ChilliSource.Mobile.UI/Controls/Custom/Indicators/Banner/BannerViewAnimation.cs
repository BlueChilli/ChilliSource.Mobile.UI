#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using Rg.Plugins.Popup.Interfaces.Animations;
using Rg.Plugins.Popup.Pages;
#if __IOS__
using UIKit;
#endif
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class BannerViewAnimation : IPopupAnimation
	{
		public void Preparing(View content, PopupPage page)
		{
			content.Opacity = 0;
		}

		public async Task Appearing(View content, PopupPage page)
		{
			var banner = page as BannerView;

			if (banner.Position == BannerPosition.Top)
			{
				content.TranslationY = -content.Height;
				SetStatusBarHidden(true);
			}
			else
			{
				content.TranslationY = content.Height;
			}

			content.Opacity = 1;

			await content.TranslateTo(0, 0, easing: Easing.Linear);
		}

		public async Task Disappearing(View content, PopupPage page)
		{
			var banner = page as BannerView;

			if (banner.Position == BannerPosition.Top)
			{
				SetStatusBarHidden(false);
				await content.TranslateTo(0, -content.Height, easing: Easing.Linear);
			}
			else
			{
				await content.TranslateTo(0, content.Height, easing: Easing.Linear);
			}
		}

		public void Disposing(View content, PopupPage page)
		{
			//Not used
		}

		public void SetStatusBarHidden(bool Hidden)
		{
#if __IOS__
			UIApplication.SharedApplication.SetStatusBarHidden(Hidden, UIStatusBarAnimation.Slide);

#else

#endif
		}
	}
}
