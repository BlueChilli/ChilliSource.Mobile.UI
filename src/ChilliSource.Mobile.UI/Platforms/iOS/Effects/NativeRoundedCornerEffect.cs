using System;
using System.ComponentModel;
using ChilliSource.Mobile.UI;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportEffect(typeof(NativeRoundedCornerEffect), "RoundedCornerEffect")]
namespace ChilliSource.Mobile.UI
{
    public class NativeRoundedCornerEffect : PlatformEffect
    {
       
        private UIView NativeView => Control ?? Container;
        private View FormView => this.Element as View;
      
        protected override void OnAttached()
        {

        }

        
        protected override void OnDetached()
        {
           
        }

        private void SetLayer(UIView view, CAShapeLayer layer)
        {
            view?.Layer.AddSublayer(layer);
        }


        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == "Width"
                || args.PropertyName == "Height"
             )
            {
                if (FormView.Width > 0 && FormView.Height > 0)
                {
                    UpdateLayer();
                }
            }

            if (args.PropertyName == RoundedCornerEffect.RadiusProperty.PropertyName ||
                args.PropertyName == RoundedCornerEffect.BorderColorProperty.PropertyName ||
                args.PropertyName == RoundedCornerEffect.BorderWidthProperty.PropertyName ||
                args.PropertyName == RoundedCornerEffect.RoundedCornerPositionProperty.PropertyName)
            {
                if (FormView.Width > 0 && FormView.Height > 0)
                {
                    UpdateLayer();
                }
            }
        }

        private CAShapeLayer _borderLayer;
        private void UpdateBorderLayer(UIBezierPath maskPath)
        {
            var borderWidth = RoundedCornerEffect.GetBorderWidth(this.Element);
            var borderColor = RoundedCornerEffect.GetBorderColor(this.Element);
            var bounds = new CGRect(0, 0, FormView.Width, FormView.Height);
            _borderLayer?.RemoveFromSuperLayer();
            _borderLayer = new CAShapeLayer
            {
                FillColor = Color.Transparent.ToCGColor(),
                StrokeColor = borderColor.ToCGColor(),
                LineWidth = (nfloat)borderWidth,
                Frame = bounds,
                Path = maskPath.CGPath
            };
            SetLayer(NativeView, _borderLayer);
        }

        private void UpdateLayer()
        {
            var position = RoundedCornerEffect.GetRoundedCornerPosition(this.Element);
            var radius = RoundedCornerEffect.GetRadius(this.Element);
            if (position == RoundedCornerPosition.None) return;
            var bounds = new CGRect(0, 0, FormView.Width, FormView.Height);
          
            var corner = GetCornerPosition(UIRectCorner.AllCorners, false);

            corner = position == RoundedCornerPosition.AllCorners ? UIRectCorner.AllCorners : GetCornerPosition(corner, true);

            var maskPath = UIBezierPath.FromRoundedRect(bounds, corner, new CGSize(radius, radius));

            UpdateMaskLayer(maskPath);
            UpdateBorderLayer(maskPath);

        }

    
        private void UpdateMaskLayer(UIBezierPath maskPath)
        {
            var bounds = new CGRect(0, 0, FormView.Width, FormView.Height);
          
            var maskLayer = new CAShapeLayer
            {
                Frame = bounds,
                Path = maskPath.CGPath
            };

            SetMask(NativeView, maskLayer);
        }

        private void SetMask(UIView view, CAShapeLayer maskLayer)
        {
            if (view != null)
            {
                view.Layer.Mask = maskLayer;
            }
        }

        private UIRectCorner GetCornerPosition(UIRectCorner corner, bool shouldAppend)
        {
            var val = corner;

            if (RoundedCornerEffect.HasTopLeft(this.Element))
            {
                val = shouldAppend ? val | UIRectCorner.TopLeft : UIRectCorner.TopLeft;
            }

            if (RoundedCornerEffect.HasTopRight(this.Element))
            {
                val = shouldAppend ? val | UIRectCorner.TopRight : UIRectCorner.TopRight;
            }

            if (RoundedCornerEffect.HasBottomLeft(this.Element))
            {
                val = shouldAppend ? val | UIRectCorner.BottomLeft : UIRectCorner.BottomLeft;
            }

            if (RoundedCornerEffect.HasBottomRight(this.Element))
            {
                val = shouldAppend ? val | UIRectCorner.BottomRight : UIRectCorner.BottomRight;
            }

            return val;
        }

    }
}
