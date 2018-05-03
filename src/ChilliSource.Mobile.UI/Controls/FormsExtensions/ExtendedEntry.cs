#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion


using System.Collections.Generic;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the entry's toolbar position.
    /// </summary>
	public enum EntryToolbarPosition
    {
        LeftToRight,
        RightToLeft
    }

    /// <summary>
    /// Xamarin Forms Entry extension
    /// </summary>
    public class ExtendedEntry : Entry
    {
        /// <summary>
        /// Initializes a new instance of the <c>ExtendedEntry</c> class.
        /// </summary>
        public ExtendedEntry()
        {
            ToolbarItems = new List<ToolbarItem>();
        }

        /// <summary>
        /// Backing store for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the text of the entry. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the entry element text. 
        /// The default value is <c>null</c>, which represents the default font on the platform.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }


        /// <summary>
        /// Backing stroe for the <c>CustomErrorFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomErrorFontProperty =
            BindableProperty.Create(nameof(CustomErrorFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the entry when the input is not valid. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font when the input is invalid. 
        /// The default value is <c>null</c>, which represents the default font on the platform.</value>
        public ExtendedFont CustomErrorFont
        {
            get { return (ExtendedFont)GetValue(CustomErrorFontProperty); }
            set { SetValue(CustomErrorFontProperty, value); }
        }


        /// <summary>
        /// Backing store for <c>CustomPlaceholderFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomPlaceholderFontProperty =
            BindableProperty.Create(nameof(CustomPlaceholderFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the placeholder text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the placeholder text. 
        /// The default value is <c>null</c>, which represents the default font on the platform.</value>
        public ExtendedFont CustomPlaceholderFont
        {
            get { return (ExtendedFont)GetValue(CustomPlaceholderFontProperty); }
            set { SetValue(CustomPlaceholderFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CustomPlaceholderErrorFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomPlaceholderErrorFontProperty =
            BindableProperty.Create(nameof(CustomPlaceholderErrorFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom placeholder when the user's input is not valid. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the placeholder when the input is invalid. 
        /// The default value is <c>null</c>, which represents the default font on the platform.</value>
        public ExtendedFont CustomPlaceholderErrorFont
        {
            get { return (ExtendedFont)GetValue(CustomPlaceholderErrorFontProperty); }
            set { SetValue(CustomPlaceholderErrorFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>NormalBackgroundColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty NormalBackgroundColorProperty =
            BindableProperty.Create(nameof(NormalBackgroundColor), typeof(Color), typeof(ExtendedEntry), Color.White);

        /// <summary>
        /// Gets or sets the default background color for the entry. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the normal background color for the entry. 
        /// The default value is <see cref="Color.White"/>.</value>
        public Color NormalBackgroundColor
        {
            get { return (Color)GetValue(NormalBackgroundColorProperty); }
            set { SetValue(NormalBackgroundColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ErrorBackgroundColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ErrorBackgroundColorProperty =
            BindableProperty.Create(nameof(ErrorBackgroundColor), typeof(Color), typeof(ExtendedEntry), Color.Red);

        /// <summary>
        /// Gets or sets the background color for the entry when the user's input is not valid. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the background color for the entry when the input is invalid. 
        /// The default value is <see cref="Color.Red"/>.</value>
        public Color ErrorBackgroundColor
        {
            get { return (Color)GetValue(ErrorBackgroundColorProperty); }
            set { SetValue(ErrorBackgroundColorProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>IsValid</c> bindable property.
        /// </summary>
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(ExtendedEntry), true, BindingMode.OneWay, null, HandleIsValidChanged, HandleIsValidChanging);

        /// <summary>
        /// Gets or sets a value indicating whether the format of the user's input is valid. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the entry's text format is valid; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
		public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>HasBorder</c> bindable property.
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedEntry), true);

        /// <summary>
        /// Gets or sets a value indicating whether the entry has border. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the entry has border; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
		public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>BorderColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ExtendedEntry), Color.FromHex("C7C7C8"));

        /// <summary>
        /// Gets or sets the color of the entry's border. This is a bindable property.<see cref="Color.FromHex(string)"/>
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the border color of the entry. The default value is <see cref="Color.FromHex(string)"/>.</value>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>MaxLength</c> bindable property.
        /// </summary>
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ExtendedEntry), 50);

        /// <summary>
        /// Gets or sets the maximum length of the entry's content. This is a bindable property.
        /// </summary>
        ///  <value>A number of characters that represents the maximum length of the entry's content; the default is 50.</value>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ErrorMessage</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ErrorMessageProperty =
            BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(ExtendedEntry), default(string));

        /// <summary>
        /// Gets or sets the error message when the input is not valid on Android. This is a bindable property.
        /// </summary>
		public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }


		/// <summary>
		///Backing store for the <c>HorizontalContentPadding</c> bindable property.
		/// </summary>
		public static readonly BindableProperty HorizontalContentPaddingProperty =
			BindableProperty.Create(nameof(HorizontalContentPadding), typeof(float), typeof(ExtendedEntry), 5f);

		/// <summary>
		/// Gets or sets the horizontal padding for the entry's content. This is a bindable property.
		/// </summary>		
		public float HorizontalContentPadding
		{
			get { return (float)GetValue(HorizontalContentPaddingProperty); }
			set { SetValue(HorizontalContentPaddingProperty, value); }
		}

        /// <summary>
        ///Backing store for the <c>TextIndentation</c> bindable property.
        /// </summary>
        public static readonly BindableProperty TextIndentationProperty =
            BindableProperty.Create(nameof(TextIndentation), typeof(float), typeof(ExtendedEntry), 0f);

        /// <summary>
        /// Gets or sets the amount of indentation for the entry. This is a bindable property.
        /// </summary>
        /// <value>A <c>float</c> value representing the amount of indentation should be left before the entry; default is 0.</value>
        public float TextIndentation
        {
            get { return (float)GetValue(TextIndentationProperty); }
            set { SetValue(TextIndentationProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>ShouldCancelEditing</c> bindable property. 
        /// </summary>
        public static BindableProperty ShouldCancelEditingProperty = BindableProperty.Create(nameof(ShouldCancelEditing), typeof(bool), typeof(ExtendedEntry), true);

        /// <summary>
        /// Gets or sets a value indicating whether editing the entry should be ended. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if editing the content should be cancelled; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool ShouldCancelEditing
        {
            get { return (bool)GetValue(ShouldCancelEditingProperty); }
            set { SetValue(ShouldCancelEditingProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>ShouldFocusWhenInvalid</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ShouldFocusWhenInvalidProperty =
            BindableProperty.Create(nameof(ShouldFocusWhenInvalid), typeof(bool), typeof(ExtendedEntry), false);

        /// <summary>
        /// Gets or sets a value indicating whether the entry should be focused when the user's input format is invalid. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the entry should be focused when is invalid; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool ShouldFocusWhenInvalid
        {
            get { return (bool)GetValue(ShouldFocusWhenInvalidProperty); }
            set { SetValue(ShouldFocusWhenInvalidProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>KeyboardTheme</c> bindable property.
        /// </summary>
        public static readonly BindableProperty KeyboardThemeProperty =
            BindableProperty.Create(nameof(KeyboardTheme), typeof(KeyboardTheme), typeof(ExtendedEntry), KeyboardTheme.Light);

        /// <summary>
        /// Gets or sets the theme of the keyboard. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="UI.KeyboardTheme"/> value that represents the keyboard theme. 
        /// The default value is <see cref="UI.KeyboardTheme.Light"/>.</value>
        public KeyboardTheme KeyboardTheme
        {
            get { return (KeyboardTheme)GetValue(KeyboardThemeProperty); }
            set { SetValue(KeyboardThemeProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ToolBarItemCustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ToolBarItemCustomFontProperty =
            BindableProperty.Create(nameof(ToolBarItemCustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the UITextField InputAccessoryView tool bar item. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for thetoolbar item. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont ToolBarItemCustomFont
        {
            get { return (ExtendedFont)GetValue(ToolBarItemCustomFontProperty); }
            set { SetValue(ToolBarItemCustomFontProperty, value); }
        }


        /// <summary>
        /// Gets or sets the text shown on the keyboard's return key.
        /// </summary>
        /// <value>A <see cref="KeyboardReturnKeyType"/> value that represents the text of the keyboard's return key.</value>
        public KeyboardReturnKeyType KeyboardReturnType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a list of toolbar items to display inside the UITextField InputAccessoryView on iOS.
        /// </summary>
        /// <value>A list of <see cref="ToolbarItem"/> to appear in the input accessory view of the entry.</value>
        public IList<ToolbarItem> ToolbarItems { get; }

        /// <summary>
        /// Gets or sets the position of the UITextField InputAccessoryView toolbar.
        /// </summary>
        /// <value>A <see cref="EntryToolbarPosition"/> value that represents the posiotion of the entry's toolbar.</value>
        public EntryToolbarPosition ToolbarPosition { get; set; }


        private static void HandleIsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        private static void HandleIsValidChanging(BindableObject bindable, object oldValue, object newValue)
        {

        }

    }
}

