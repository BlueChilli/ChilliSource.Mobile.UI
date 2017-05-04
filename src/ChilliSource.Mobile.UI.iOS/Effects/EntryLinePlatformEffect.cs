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

[assembly: ExportEffect(typeof(EntryLinePlatformEffect), "EntryLineEffect")]

namespace ChilliSource.Mobile.UI
{
	public class EntryLinePlatformEffect : PlatformEffect
	{
		Color _lineColor;
		float _lineHeight;

		string _lineText;
		ExtendedFont _lineTextCustomFont;

		EntryLineEffect _effect;
		FloatingLabelEntryNativeView _nativeView;

		UIView _line;
		UILabel _lineTextLabel;

		protected override void OnAttached()
		{
			_effect = (EntryLineEffect)Element.Effects.FirstOrDefault(m => m is EntryLineEffect);

			_nativeView = Control as FloatingLabelEntryNativeView;

			if (_effect != null && _nativeView != null)
			{
				_nativeView.PlaceholderRectSet += (sender, e) => AddLine();

				_lineColor = _effect.LineColor;
				_lineHeight = _effect.LineHeight;

				_lineText = _effect.LineText;
				_lineTextCustomFont = _effect.LineTextCustomFont;

				AddText();
			}
		}

		void AddLine()
		{
			if (_line != null) _line.RemoveFromSuperview();

			_line = new UIView();
			SetLineProperties();

			_nativeView.AddSubview(_line);
		}

		void SetLineProperties()
		{
			_line.BackgroundColor = _lineColor.ToUIColor();
			var textRect = _nativeView.TextRect(_nativeView.Bounds);
			_line.Frame = new CGRect(textRect.X, textRect.Y + textRect.Height + 3.0f, textRect.Width, _lineHeight);
		}

		void AddText()
		{

		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);
		}

		protected override void OnDetached()
		{

		}
	}
}
