#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public enum EntryToolbarPosition
	{
		LeftToRight,
		RightToLeft
	}

	public class ExtendedEntry : Entry
	{
		public ExtendedEntry()
		{
			ToolbarItems = new List<ToolbarItem>();
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty CustomErrorFontProperty =
			BindableProperty.Create(nameof(CustomErrorFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);


		public ExtendedFont CustomErrorFont
		{
			get { return (ExtendedFont)GetValue(CustomErrorFontProperty); }
			set { SetValue(CustomErrorFontProperty, value); }
		}

		public static readonly BindableProperty CustomPlaceholderFontProperty =
			BindableProperty.Create(nameof(CustomPlaceholderFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);


		public ExtendedFont CustomPlaceholderFont
		{
			get { return (ExtendedFont)GetValue(CustomPlaceholderFontProperty); }
			set { SetValue(CustomPlaceholderFontProperty, value); }
		}

		public static readonly BindableProperty CustomPlaceholderErrorFontProperty =
			BindableProperty.Create(nameof(CustomPlaceholderErrorFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);


		public ExtendedFont CustomPlaceholderErrorFont
		{
			get { return (ExtendedFont)GetValue(CustomPlaceholderErrorFontProperty); }
			set { SetValue(CustomPlaceholderErrorFontProperty, value); }
		}

		public static readonly BindableProperty NormalBackgroundColorProperty =
			BindableProperty.Create(nameof(NormalBackgroundColor), typeof(Color), typeof(ExtendedEntry), Color.White);


		public Color NormalBackgroundColor
		{
			get { return (Color)GetValue(NormalBackgroundColorProperty); }
			set { SetValue(NormalBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty ErrorBackgroundColorProperty =
			BindableProperty.Create(nameof(ErrorBackgroundColor), typeof(Color), typeof(ExtendedEntry), Color.Red);


		public Color ErrorBackgroundColor
		{
			get { return (Color)GetValue(ErrorBackgroundColorProperty); }
			set { SetValue(ErrorBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty IsValidProperty =
			BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(ExtendedEntry), true, BindingMode.OneWay, null, HandleIsValidChanged, HandleIsValidChanging);

		public bool IsValid
		{
			get { return (bool)GetValue(IsValidProperty); }
			set { SetValue(IsValidProperty, value); }
		}

		public static readonly BindableProperty HasBorderProperty =
			BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedEntry), true);

		public bool HasBorder
		{
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ExtendedEntry), Color.FromHex("C7C7C8"));

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		public static readonly BindableProperty MaxLengthProperty =
			BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ExtendedEntry), 50);


		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public static readonly BindableProperty ErrorMessageProperty =
			BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(ExtendedEntry), default(string));

		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}

		public KeyboardReturnKeyType KeyboardReturnType
		{
			get;
			set;
		}

		public static readonly BindableProperty TextIndentationProperty =
			BindableProperty.Create(nameof(TextIndentation), typeof(float), typeof(ExtendedEntry), 0f);

		public float TextIndentation
		{
			get { return (float)GetValue(TextIndentationProperty); }
			set { SetValue(TextIndentationProperty, value); }
		}

		public IList<ToolbarItem> ToolbarItems { get; }

		public EntryToolbarPosition ToolbarPosition { get; set; }

		public static readonly BindableProperty ToolBarItemCustomFontProperty =
			BindableProperty.Create(nameof(ToolBarItemCustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

		public ExtendedFont ToolBarItemCustomFont
		{
			get { return (ExtendedFont)GetValue(ToolBarItemCustomFontProperty); }
			set { SetValue(ToolBarItemCustomFontProperty, value); }
		}

		private static void HandleIsValidChanged(BindableObject bindable, object oldValue, object newValue)
		{

		}

		private static void HandleIsValidChanging(BindableObject bindable, object oldValue, object newValue)
		{

		}

		public static BindableProperty ShouldCancelEditingProperty = BindableProperty.Create(nameof(ShouldCancelEditing), typeof(bool), typeof(ExtendedEntry), true);

		public bool ShouldCancelEditing
		{
			get { return (bool)this.GetValue(ShouldCancelEditingProperty); }
			set { this.SetValue(ShouldCancelEditingProperty, value); }
		}

		public static readonly BindableProperty ShouldFocusWhenInvalidProperty =
			BindableProperty.Create(nameof(ShouldFocusWhenInvalid), typeof(bool), typeof(ExtendedEntry), false);

		public bool ShouldFocusWhenInvalid
		{
			get { return (bool)GetValue(ShouldFocusWhenInvalidProperty); }
			set { SetValue(ShouldFocusWhenInvalidProperty, value); }
		}

		public static readonly BindableProperty KeyboardThemeProperty =
			BindableProperty.Create(nameof(KeyboardTheme), typeof(KeyboardTheme), typeof(ExtendedEntry), KeyboardTheme.Light);

		public KeyboardTheme KeyboardTheme
		{
			get { return (KeyboardTheme)GetValue(KeyboardThemeProperty); }
			set { SetValue(KeyboardThemeProperty, value); }
		}

		#region Floating Label Properties

		#endregion
	}
}

