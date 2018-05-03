using System;
using ChilliSource.Mobile.UI;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportEffect(typeof(NativeShadowEffect), "ShadowEffect")]
namespace ChilliSource.Mobile.UI
{
    public class NativeShadowEffect : PlatformEffect
    {
        private UIView NativeView => Control ?? Container;
        private View FormView => this.Element as View;
      
        protected override void OnAttached()
        {
            UpdateShadowLayer();
        }

        
        protected override void OnDetached()
        {
           
        }

 
        private void UpdateShadowLayer()
        {
            this.NativeView.Layer.ShadowColor = ShadowEffect.GetShadowColor(this.Element).ToCGColor();
            this.NativeView.Layer.ShadowOpacity = (float) ShadowEffect.GetShadowOpacity(this.Element);
            this.NativeView.Layer.ShadowOffset = new CGSize(ShadowEffect.GetShadowOffsetX(this.Element), ShadowEffect.GetShadowOffsetY(this.Element));
            this.NativeView.Layer.ShadowRadius = (nfloat) ShadowEffect.GetShadowRadius(this.Element);
            this.NativeView.Layer.CornerRadius = (nfloat) ShadowEffect.GetRadius(this.Element);
            this.NativeView.Layer.BorderColor = ShadowEffect.GetBorderColor(this.Element).ToCGColor();
            this.NativeView.Layer.BorderWidth = (nfloat) ShadowEffect.GetBorderWidth(this.Element);

        }


    }
}