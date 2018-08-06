using System;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SafeAreaPaddingEffect), nameof(SafeAreaPaddingEffect))]
namespace ChilliSource.Mobile.UI
{
    class SafeAreaPaddingEffect : PlatformEffect
    {
        Thickness _padding;
        readonly bool _includeStatusBar;

        public SafeAreaPaddingEffect(bool includeStatusBar = true)
        {
            _includeStatusBar = includeStatusBar;
        }

        protected override void OnAttached()
        {
            SetSafeArea(Element, new Thickness());
        }

        private void SetSafeArea(Element layout, Thickness thickness)
        {
            if (layout is Layout element)
            {
                _padding = element.Padding;

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {

                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
                    if (insets.Top > 0) // iPhone X
                    {
                        element.Padding = new Thickness(_padding.Left + insets.Left + thickness.Left, _padding.Top + insets.Top + thickness.Top, _padding.Right + insets.Right + thickness.Right, _padding.Bottom + thickness.Bottom);
                        return;
                    }
                }

           
                var statusBar = UIApplication.SharedApplication?.StatusBarFrame.Height ?? 20.0;

                int topPadding = _includeStatusBar ? (int) statusBar : 0;

                element.Padding = new Thickness(_padding.Left, _padding.Top + topPadding, _padding.Right, _padding.Bottom);
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout element)
            {
                element.Padding = _padding;
            }
        }

        internal void SetSafeAreaPadding(Element element, Thickness padding)
        {
            SetSafeArea(element, padding);
        }
    }
    

}
