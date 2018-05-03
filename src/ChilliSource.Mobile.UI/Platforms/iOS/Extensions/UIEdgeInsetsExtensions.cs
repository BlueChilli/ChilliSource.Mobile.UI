using System;
using UIKit;
using Xamarin.Forms;
namespace ChilliSource.Mobile.UI
{
    public static class ThicknessExtensions
    {
        public static UIEdgeInsets ToUIEdgeInsets(this Thickness thickness)
        {
            return new UIEdgeInsets((nfloat)thickness.Top, (nfloat)thickness.Left, (nfloat)thickness.Bottom, (nfloat)thickness.Right);
        }
    }
}
