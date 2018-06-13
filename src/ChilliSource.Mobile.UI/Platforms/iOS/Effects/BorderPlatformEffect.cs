#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.ComponentModel;
using System.Linq;
using ChilliSource.Mobile.UI;
using CoreAnimation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(BorderPlatformEffect), nameof(BorderEffect))]
namespace ChilliSource.Mobile.UI
{
    public class BorderPlatformEffect : PlatformEffect
    {
        private UIView NativeView => Control ?? Container;
        private View FormView => this.Element as View;
      
        protected override void OnAttached()
        {
          
        }

        protected override void OnDetached()
        {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            
            if (args.PropertyName == "Width"
                || args.PropertyName == "Height"
             )
            {
                if (FormView.Width > 0 && FormView.Height > 0)
                {
                    UpdateLayer();

                }
            }
        }

        private void UpdateLayer()
        {
            var effect = (BorderEffect)Element.Effects.FirstOrDefault(m => m is BorderEffect);
     
            if (effect != null && NativeView != null)
            {
                if(effect.Type == BorderType.Dashed)
                {
                    var shapeLayer = new CAShapeLayer();
                    shapeLayer.Frame = NativeView.Bounds;
                    shapeLayer.Path = UIBezierPath.FromRect(NativeView.Bounds).CGPath;
                    shapeLayer.LineDashPattern = new[] { NSNumber.FromInt32(effect.DashWidth), NSNumber.FromInt32(effect.DashSpaceWidth)};
                    shapeLayer.LineWidth = effect.BorderWidth;
                    shapeLayer.FillColor = UIColor.Clear.CGColor;
                    shapeLayer.StrokeColor = effect.BorderColor.ToCGColor();
                    shapeLayer.CornerRadius =  effect.BorderRadius;
                    shapeLayer.LineJoin = CAShapeLayer.JoinRound;
                    NativeView.Layer.AddSublayer(shapeLayer);
                }
                else
                {
                    NativeView.Layer.CornerRadius = effect.BorderRadius;
                    NativeView.Layer.BorderWidth = effect.BorderWidth;
                    NativeView.Layer.BorderColor = effect.BorderColor.ToCGColor();
                    NativeView.Layer.MasksToBounds = true;
                }
            }
        }
    }
}
