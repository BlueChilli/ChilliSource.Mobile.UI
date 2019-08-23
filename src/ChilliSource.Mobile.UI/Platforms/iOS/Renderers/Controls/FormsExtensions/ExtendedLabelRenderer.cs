#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedLabelRenderer : LabelRenderer
	{

		private NSLayoutManager _layoutManager;
		private readonly NSTextContainer _textContainer = new NSTextContainer();

		private List<KeyValuePair<int, StyledTextPart>> _styledTextPartList;

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			SetStyle();
			UpdateLineFit();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (ExtendedLabel == null)
			{
				return;
			}

			if (e.PropertyName == Label.TextProperty.PropertyName ||
				e.PropertyName == Label.FormattedTextProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.CustomFontProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.ChildrenProperty.PropertyName)
			{
				SetStyle();
			}
			else if (e.PropertyName == ExtendedLabel.NumberOfLinesProperty.PropertyName)
			{
				UpdateLineFit();
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			if (Control != null)
			{
				_textContainer.Size = Control.Bounds.Size;
			}
		}

        public ExtendedLabel ExtendedLabel => Element as ExtendedLabel;
		
		private void SetStyle()
		{

			if (ExtendedLabel.Children != null && ExtendedLabel.Children.Count > 0)
			{
				NSTextStorage attributedString = new NSTextStorage();
				_styledTextPartList = new List<KeyValuePair<int, StyledTextPart>>();
				ExtendedLabel.Children.ToList().ForEach(styledTextPart =>
				{
					_styledTextPartList.Add(new KeyValuePair<int, StyledTextPart>(Convert.ToInt32(attributedString.Length), styledTextPart));
					attributedString.Append(styledTextPart.CustomFont.BuildAttributedString(styledTextPart.Text, this.Control.TextAlignment));
				});
				Control.AttributedText = attributedString;

				Control.UserInteractionEnabled = true;
				var tapGestureRecognizer = new UITapGestureRecognizer();
				tapGestureRecognizer.AddTarget(() => LabelTapped(tapGestureRecognizer));
				Control.AddGestureRecognizer(tapGestureRecognizer);


				//if (!ExtendedLabel.TextShouldWrap)
				//{
				//	Control.Lines = 1;
				//}

				_layoutManager = new NSLayoutManager();

				_layoutManager.AddTextContainer(_textContainer);
				attributedString.AddLayoutManager(_layoutManager);

				return;
			}

			if (ExtendedLabel.Text != null)
			{
				var attributedString = ExtendedLabel.CustomFont.BuildAttributedString(ExtendedLabel.Text, this.Control.TextAlignment);
				Control.AttributedText = attributedString;
			}
		}

		void UpdateLineFit()
		{
			if (ExtendedLabel.AdjustFontSizeToFit)
			{
				Control.AdjustsFontSizeToFitWidth = true;
				Control.Lines = 1;
			}
			else if (ExtendedLabel.NumberOfLines > 0)
			{
				Control.Lines = ExtendedLabel.NumberOfLines;
			}			
		}

		void LabelTapped(UITapGestureRecognizer gestureRecognizer)
		{
			var locationOfTouch = gestureRecognizer.LocationInView(gestureRecognizer.View);
			var indexOfCharacter = _layoutManager.GetCharacterIndex(locationOfTouch, _textContainer);

			_styledTextPartList.ForEach(part =>
			{
				if (Convert.ToInt32(indexOfCharacter) >= part.Key && Convert.ToInt32(indexOfCharacter) < part.Key + part.Value.Text.Length - 1)
				{
					part.Value.Command?.Execute(part.Value.CommandParameter);
				}
			});
		}
	}
}

