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
    /// <summary>
    /// Animation for <see cref="BannerView"/>.
    /// </summary>
    public class BannerViewAnimation : IPopupAnimation
    {
        /// <summary>
        /// This is called before the <see cref="Page.OnAppearing"/> method to prepare the content and page for the animated banner view.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
        public void Preparing(View content, PopupPage page)
        {
            content.Opacity = 0;
        }

        /// <summary>
        /// This is called after the <see cref="Page.OnAppearing"/> method to show the banner with animation.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that shows the banner.</returns>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
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

        /// <summary>
        /// This is called before the <see cref="Page.OnDisappearing"/> method to animate hiding the banner view.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that hides the animation.</returns>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
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

        /// <summary>
        /// This is called after the <see cref="Page.OnDisappearing"/> method to dispose the animation and any unmanaged code.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
        public void Disposing(View content, PopupPage page)
        {
            //Not used
        }

        /// <summary>
        /// Hides the status bar.
        /// </summary>
        /// <param name="Hidden">If set to <c>true</c> is hidden.</param>
        public void SetStatusBarHidden(bool Hidden)
        {
#if __IOS__
            UIApplication.SharedApplication.SetStatusBarHidden(Hidden, UIStatusBarAnimation.Slide);

#else

#endif
        }
    }
}
