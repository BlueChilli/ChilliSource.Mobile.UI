﻿using ChilliSource.Mobile.UI.PlatformConfiguration.iOS;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(NativeSafeAreaPaddingEffect), "SafeAreaPaddingEffect")]
namespace ChilliSource.Mobile.UI.PlatformConfiguration.iOS
{
    public class NativeSafeAreaPaddingEffect : PlatformEffect
    {
        Thickness _padding;
        private bool _includeStatusBar;

        protected override void OnAttached()
        {
            _includeStatusBar = SafeAreaPadding.GetShouldIncludeStatusBar(Element);
            SetSafeArea(Element, SafeAreaPadding.GetSafeAreaInsets(Element));
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == SafeAreaPadding.SafeAreaInsets.PropertyName)
            {
                SetSafeArea(Element, SafeAreaPadding.GetSafeAreaInsets(Element));
            }

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

            var orientation = UIApplication.SharedApplication.StatusBarOrientation;

            bool hasInsets = false;

            switch(orientation)
            {
                case UIInterfaceOrientation.Portrait:
                case UIInterfaceOrientation.PortraitUpsideDown:
                    hasInsets = insets.Top > 0;
                    break;
                case UIInterfaceOrientation.LandscapeLeft:
                case UIInterfaceOrientation.LandscapeRight:
                    hasInsets = insets.Left > 0 || insets.Right > 0;
                    break;
                default:
                    hasInsets = insets.Top > 0;
                    break;
            }

            if (hasInsets) // iPhone X
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


        private Thickness SafeAreaInsets
        {
            get
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {

                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
                    return new Thickness(insets.Left, insets.Top, insets.Right, insets.Bottom);
                }

                return new Thickness();
            }
        }
    }
    

}
