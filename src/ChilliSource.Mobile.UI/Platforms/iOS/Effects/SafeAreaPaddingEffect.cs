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
                element.Padding = GetSafeAreaPadding(_padding, thickness);
            }

            if(layout is Page page)
            {
                _padding = page.Padding;
                page.Padding = GetSafeAreaPadding(_padding, thickness);

            }
        }

        private Thickness GetSafeAreaPadding(Thickness originalPadding, Thickness additionalInsets)
        {
            var insets = SafeAreaInsets;

            if (insets.Top > 0) // iPhone X
            {
                return new Thickness(originalPadding.Left + insets.Left + additionalInsets.Left, originalPadding.Top + insets.Top + additionalInsets.Top, originalPadding.Right + insets.Right + additionalInsets.Right, originalPadding.Bottom + additionalInsets.Bottom);
            }

            var statusBar = UIApplication.SharedApplication?.StatusBarFrame.Height ?? 20.0;

            int topPadding = _includeStatusBar ? (int)statusBar : 0;

            return new Thickness(originalPadding.Left, originalPadding.Top + topPadding, originalPadding.Right, originalPadding.Bottom);

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

        internal Thickness SafeAreaInsets
        {
            get
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {

                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;

                    if (insets.Top > 0) // iPhone X
                    {
                        return new Thickness(insets.Left, insets.Top, insets.Right, insets.Bottom);
                    }

                    
                }

                return new Thickness();
            }
        }
    }
    

}
