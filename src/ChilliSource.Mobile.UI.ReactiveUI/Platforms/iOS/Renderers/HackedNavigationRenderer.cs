#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Reflection;
using ChilliSource.Mobile.UI.ReactiveUI.iOS;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(HackedNavigationRenderer))]
namespace ChilliSource.Mobile.UI.ReactiveUI.iOS
{
    public class HackedNavigationRenderer : NavigationRenderer
    {
        private static readonly FieldInfo appearedField = typeof(NavigationRenderer).GetField("_appeared", BindingFlags.NonPublic | BindingFlags.Instance);
       
        private IPageController PageController => this.Element as IPageController;

        private bool Appeared
        {
            get { return (bool)appearedField.GetValue(this); }
            set { appearedField.SetValue(this, value); }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this.Appeared)
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
