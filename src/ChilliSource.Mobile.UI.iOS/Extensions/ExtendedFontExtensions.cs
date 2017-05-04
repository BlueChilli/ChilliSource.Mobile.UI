#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public static class ExtendedFontExtensions
	{
		public static NSMutableAttributedString BuildAttributedString(this ExtendedFont font, string text, UITextAlignment textAlignment = UITextAlignment.Natural)
		{
			if (font == null)
			{
				return new NSMutableAttributedString(text);
			}

			var attributes = new UIStringAttributes();

			attributes.Font = font.ToUIFont();
			attributes.ForegroundColor = font.Color.ToUIColor();
			attributes.BackgroundColor = UIColor.Clear;
			attributes.KerningAdjustment = font.Kerning;

			if (font.IsUnderlined)
			{
				attributes.UnderlineStyle = NSUnderlineStyle.Single;
			}

			var attribString = new NSMutableAttributedString(text, attributes);

			if (font.LineSpacing != 0)
			{
				var paragraphStyle = new NSMutableParagraphStyle()
				{
					LineSpacing = (nfloat)font.LineSpacing,
					Alignment = textAlignment
				};

				attribString.AddAttribute(UIStringAttributeKey.ParagraphStyle, paragraphStyle, new NSRange(0, attribString.Length));
			}

			return attribString;
		}

		public static UIFont ToUIFont(this ExtendedFont font)
		{
			if (font.Family == null)
			{
				switch (font.FontAttributes)
				{
					case FontAttributes.Bold:
						return UIFont.BoldSystemFontOfSize(font.Size);

					case FontAttributes.Italic:
						return UIFont.ItalicSystemFontOfSize(font.Size);

					case FontAttributes.None:
						return UIFont.SystemFontOfSize(font.Size);
				}
			}
			else
			{
				if (font.IsAccessibilityFont)
				{
					UIFontTextStyle textStyle = UIFontTextStyle.Body;
					switch (font.AccessibilityTextStyle)
					{
						case AccessibilityTextStyle.Callout:
							{
								textStyle = UIFontTextStyle.Callout;
								break;
							}
						case AccessibilityTextStyle.Caption1:
							{
								textStyle = UIFontTextStyle.Caption1;
								break;
							}
						case AccessibilityTextStyle.Caption2:
							{
								textStyle = UIFontTextStyle.Caption2;
								break;
							}
						case AccessibilityTextStyle.Footnote:
							{
								textStyle = UIFontTextStyle.Footnote;
								break;
							}
						case AccessibilityTextStyle.Headline:
							{
								textStyle = UIFontTextStyle.Headline;
								break;
							}
						case AccessibilityTextStyle.Subheadline:
							{
								textStyle = UIFontTextStyle.Subheadline;
								break;
							}
						case AccessibilityTextStyle.Title1:
							{
								textStyle = UIFontTextStyle.Title1;
								break;
							}
						case AccessibilityTextStyle.Title2:
							{
								textStyle = UIFontTextStyle.Title2;
								break;
							}
						case AccessibilityTextStyle.Title3:
							{
								textStyle = UIFontTextStyle.Title3;
								break;
							}
					}

					var uiFont = UIFont.GetPreferredFontForTextStyle(textStyle);
					return UIFont.FromName(font.Family, uiFont.PointSize);
				}
				else
				{
					return UIFont.FromName(font.Family, font.Size);
				}
			}

			return UIFont.SystemFontOfSize(font.Size);
		}
	}
}
