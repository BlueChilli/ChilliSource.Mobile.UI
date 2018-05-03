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
            if (Element is Layout element)
            {
                _padding = element.Padding;

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {

                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
                    if (insets.Top > 0) // iPhone X
                    {
                        element.Padding = new Thickness(_padding.Left + insets.Left, _padding.Top + insets.Top, _padding.Right + insets.Right, _padding.Bottom);
                        return;
                    }
                }

                int topPadding = _includeStatusBar ? 20 : 0;

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
    }
    

}
