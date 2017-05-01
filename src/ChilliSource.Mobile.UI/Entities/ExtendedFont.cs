#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class ExtendedFont
	{
		public ExtendedFont()
		{
		}

		public ExtendedFont(string family, AccessibilityTextStyle textStyle) : this(family, textStyle, Color.Black)
		{
		}

		public ExtendedFont(string family, AccessibilityTextStyle textStyle, Color color)
		{
			Family = family;
			AccessibilityTextStyle = textStyle;
			Color = color;
			IsAccessibilityFont = true;
		}

		public ExtendedFont(string family, float size) : this(family, size, Color.Black)
		{
		}

		public ExtendedFont(string family, float size, Color color, float kerning = 0, float lineSpacing = 0, bool isUnderlined = false)
		{
			Family = family;
			Size = size;
			Color = color;
			Kerning = kerning;
			LineSpacing = lineSpacing;
			IsUnderlined = isUnderlined;
		}

		public ExtendedFont(float size, Color color, float kerning = 0, float lineSpacing = 0, bool isUnderlined = false, FontAttributes fontAttributes = FontAttributes.None)
		{
			Size = size;
			Color = color;
			Kerning = kerning;
			LineSpacing = lineSpacing;
			IsUnderlined = isUnderlined;
			FontAttributes = fontAttributes;
		}



		public string Family { get; private set; }
		public float Size { get; private set; }
		public Color Color { get; private set; }
		public float Kerning { get; private set; }
		public float LineSpacing { get; private set; }
		public bool IsUnderlined { get; private set; }
		public FontAttributes FontAttributes { get; private set; }
		public AccessibilityTextStyle AccessibilityTextStyle { get; private set; }
		public bool IsAccessibilityFont { get; private set; } = false;

		public ExtendedFont Clone()
		{
			return new ExtendedFont
			{
				Family = this.Family,
				Size = this.Size,
				Color = this.Color,
				Kerning = this.Kerning,
				LineSpacing = this.LineSpacing,
				IsUnderlined = this.IsUnderlined,
				FontAttributes = this.FontAttributes,
				AccessibilityTextStyle = this.AccessibilityTextStyle,
				IsAccessibilityFont = this.IsAccessibilityFont
			};
		}
	}
}

