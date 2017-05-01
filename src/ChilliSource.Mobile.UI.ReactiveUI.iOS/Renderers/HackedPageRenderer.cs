#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
Source: 	Blog (http://kent-boogaart.com/blog/hacking-xamarin.forms-page.appearing-for-ios/)
Author: 	Kent Boogaart (http://kent-boogaart.com/)
License:	MIT (http://kent-boogaart.com/license)
*/
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI.ReactiveUI.iOS;
using ChilliSource.Mobile.UI;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Page), typeof(HackedPageRenderer))]
namespace ChilliSource.Mobile.UI.ReactiveUI.iOS
{
    // a hacky PageRenderer subclass that uses the correct hook (ViewWillAppear rather than ViewDidAppear) for the Page.Appearing event on iOS
    // TODO: remove this once XF life cycle is fixed (see https://forums.xamarin.com/discussion/84510/proposal-improved-life-cycle-support)
    public sealed class HackedPageRenderer : PageRenderer
    {
        private static readonly FieldInfo appearedField = typeof(PageRenderer).GetField("_appeared", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo disposedField = typeof(PageRenderer).GetField("_disposed", BindingFlags.NonPublic | BindingFlags.Instance);

        private IPageController PageController => this.Element as IPageController;

        private bool Appeared
        {
            get { return (bool)appearedField.GetValue(this); }
            set { appearedField.SetValue(this, value); }
        }

        private bool Disposed
        {
            get { return (bool)disposedField.GetValue(this); }
            set { disposedField.SetValue(this, value); }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this.Appeared || this.Disposed)
            {
                return;
            }

            // by setting this to true, we also ensure that PageRenderer does not invoke SendAppearing a second time when ViewDidAppear fires
            this.Appeared = true;
            PageController.SendAppearing();
        }

       
    }

    // a hacky PageRenderer subclass that uses the correct hook (ViewWillAppear rather than ViewDidAppear) for the Page.Appearing event on iOS
    // TODO: remove this once XF life cycle is fixed (see https://forums.xamarin.com/discussion/84510/proposal-improved-life-cycle-support)
}