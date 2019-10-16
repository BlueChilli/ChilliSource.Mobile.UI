#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Adds ability to customize the rendering of fonts in more detail 
    /// by exposing properties for features such as kerning, line spacing, and accessibliy.
    /// </summary>
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

        /// <summary>
        /// Specifies the horizontal spacing between letters
        /// </summary>
        /// <value>The kerning.</value>
        public float Kerning { get; private set; }

        /// <summary>
        /// Specifies the vertical spacing between lines of text
        /// </summary>
        /// <value>The line spacing.</value>
        public float LineSpacing { get; private set; }

        public bool IsUnderlined { get; private set; }

        /// <summary>
        /// Specifies font options such as bold/italic
        /// </summary>
        /// <value>The font attributes.</value>
        public FontAttributes FontAttributes { get; private set; }

        /// <summary>
        /// Specifies a <see cref="AccessibilityTextStyle"/> that has a relative font size 
        /// that can be customized by the user on their device
        /// </summary>
        /// <value>The accessibility text style.</value>
        public AccessibilityTextStyle AccessibilityTextStyle { get; private set; }

        /// <summary>
        /// Specifies that this font instance should use relative font sizes defined by <see cref="AccessibilityTextStyle"/>
        /// </summary>
        /// <value><c>true</c> if is accessibility font; otherwise, <c>false</c>.</value>
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

        public ExtendedFont CopyWith(
            string family = null,
            float? size = null, 
            Color? color = null, 
            float? kerning = null, 
            float? lineSpacing = null, 
            bool? isUnderlined = null, 
            FontAttributes? fontAttributes = null,
            AccessibilityTextStyle? accessibilityTextStyle = null,
            bool? isAccessibilityFont = null
            )
        {
            return new ExtendedFont
            {
                Family = family ?? this.Family,
                Size = size ?? this.Size,
                Color = color ?? this.Color,
                Kerning = kerning ?? this.Kerning,
                LineSpacing = lineSpacing ?? this.LineSpacing,
                IsUnderlined = isUnderlined ?? this.IsUnderlined,
                FontAttributes = fontAttributes ??  this.FontAttributes,
                AccessibilityTextStyle = accessibilityTextStyle ?? this.AccessibilityTextStyle,
                IsAccessibilityFont = isAccessibilityFont ?? this.IsAccessibilityFont
            };
            
            
        }
    }
}

