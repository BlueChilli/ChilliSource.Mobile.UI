#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Android.Widget;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using Android.Text.Style;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public static class ExtendedFontExtensions
	{
		public static void ApplyTo(this ExtendedFont font, TextView textView)
		{
			if (font == null)
			{
				return;
			}

			if (font.Family == null)
			{
				TypefaceStyle typefaceStyle = TypefaceStyle.Normal;

				switch (font.FontAttributes)
				{
					case FontAttributes.Bold:
						typefaceStyle = TypefaceStyle.Bold;
						break;

					case FontAttributes.Italic:
						typefaceStyle = TypefaceStyle.Italic;
						break;
				}

				textView.SetTypeface(Typeface.Default, typefaceStyle);
			}
			else
			{
				textView.SetTypeface(TypefaceHelper.GetTypeface(font.Family), TypefaceStyle.Normal);
			}

			textView.SetTextColor(font.Color.ToAndroid());
			textView.SetTextSize(Android.Util.ComplexUnitType.Dip, font.Size);

			if (font.IsUnderlined)
			{
				SpannableString content = new SpannableString(textView.Text);
				content.SetSpan(new UnderlineSpan(), 0, textView.Text.Length, 0);
				textView.TextFormatted = content;
			}

			if (font.LineSpacing != 0)
			{
				textView.SetLineSpacing(font.LineSpacing, 1);
			}
		}
	}
}
