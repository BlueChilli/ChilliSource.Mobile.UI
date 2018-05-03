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
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Determines how the <see cref="AdvancedActionSheet"/> class should be animated when is displayed or dismissed.
    /// </summary>
    public class AdvancedActionSheetAnimation : IPopupAnimation
    {
        /// <summary>
        /// This is called before the <see cref="Page.OnAppearing"/> method to prepare the content and page for the advanced action sheet.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
        public void Preparing(View content, PopupPage page)
        {
            //Not used
        }

        /// <summary>
        ///  This is called after the <see cref="Page.OnAppearing"/> method to show the advanced action sheet with animation.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that shows the advanced action sheet.</returns>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
        public async Task Appearing(View content, PopupPage page)
        {
            content.TranslationY = content.Height;
            await content.TranslateTo(0, 0, length: 200, easing: Easing.Linear);
        }

        /// <summary>
        /// This is called before the <see cref="Page.OnDisappearing"/> method to animate hiding the advanced action sheet.
        /// </summary>
        ///<returns>Returns a <see cref="Task"/> that hides the action sheet.</returns>
        /// <param name="content">Content.</param>
        /// <param name="page">Page.</param>
        public async Task Disappearing(View content, PopupPage page)
        {
            await content.TranslateTo(0, content.Height, length: 200, easing: Easing.Linear);
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
    }
}

