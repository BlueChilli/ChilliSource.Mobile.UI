#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Linq;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportEffect(typeof(FloatingLabelEntryLinePlatformEffect), nameof(FloatingLabelEntryLineEffect))]

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Platform effect that adds a customizable line to a <see cref="FloatingLabelEntry"/>
    /// </summary>
    public class FloatingLabelEntryLinePlatformEffect : PlatformEffect
    {
        Color _lineColor;
        float _lineHeight;

        FloatingLabelEntryLineEffect _effect;
        FloatingLabelEntryNativeView _nativeView;

        UIView _line;

        protected override void OnAttached()
        {
            _effect = (FloatingLabelEntryLineEffect)Element.Effects.FirstOrDefault(m => m is FloatingLabelEntryLineEffect);

            _nativeView = Control as FloatingLabelEntryNativeView;

            if (_effect != null && _nativeView != null)
            {
                _nativeView.PlaceholderRectSet += HandlePlaceholderRectSet;
            }
        }

        //Adds the Line UIView when the UITextfields rects have been set
        void HandlePlaceholderRectSet(object sender, EventArgs e)
        {
            AddLine();

            _nativeView.PlaceholderRectSet -= HandlePlaceholderRectSet;
        }

        //Add the UIView
        void AddLine()
        {
            if (_line != null)
            {
                _line.RemoveFromSuperview();
            }

            _line = new UIView();
            UpdateLineProperties();

            _nativeView.AddSubview(_line);
        }

        //Update line height and color
        void UpdateLineProperties()
        {
            _lineColor = FloatingLabelEntryLineEffect.GetLineColor(Element);
            _lineHeight = FloatingLabelEntryLineEffect.GetLineHeight(Element);

            _line.BackgroundColor = _lineColor.ToUIColor();
            var textRect = _nativeView.TextRect(_nativeView.Bounds);

            _line.Frame = new CGRect(textRect.X, textRect.Y + textRect.Height + 2.0f, textRect.Width, _lineHeight);
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == FloatingLabelEntryLineEffect.LineColorProperty.PropertyName || args.PropertyName == FloatingLabelEntryLineEffect.LineHeightProperty.PropertyName)
            {
                UpdateLineProperties();
            }
        }

        protected override void OnDetached()
        {
            //
        }
    }
}
