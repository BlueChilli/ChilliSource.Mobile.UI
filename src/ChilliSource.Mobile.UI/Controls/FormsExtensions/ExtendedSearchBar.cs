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
	public class ExtendedSearchBar : SearchBar
	{
		public ExtendedSearchBar()
		{

		}

		public static readonly BindableProperty BarTintProperty = BindableProperty.Create(nameof(BarTint), typeof(Color?), typeof(ExtendedSearchBar), defaultValue: Color.Blue);
		public Color BarTint
		{
			get { return (Color)GetValue(BarTintProperty); }
			set { SetValue(BarTintProperty, value); }
		}

		public static readonly BindableProperty SearchStyleProperty = BindableProperty.Create(nameof(SearchStyle), typeof(string), typeof(ExtendedSearchBar), "Default");
		public string SearchStyle
		{
			get { return (string)GetValue(SearchStyleProperty); }
			set { SetValue(SearchStyleProperty, value); }
		}

		public static readonly BindableProperty BarStyleProperty = BindableProperty.Create(nameof(BarStyle), typeof(string), typeof(ExtendedSearchBar), "Default");
		public string BarStyle
		{
			get { return (string)GetValue(BarStyleProperty); }
			set { SetValue(BarStyleProperty, value); }
		}

		public static readonly BindableProperty PersistCancelButtonProperty = BindableProperty.Create(nameof(PersistCancelButton), typeof(bool), typeof(ExtendedSearchBar), false);
		public bool PersistCancelButton
		{
			get { return (bool)this.GetValue(PersistCancelButtonProperty); }
			set { this.SetValue(PersistCancelButtonProperty, value); }
		}

		public event EventHandler<EventArgs> CancelButtonPressed;

		public void OnCancelButtonClicked()
		{
			if (CancelButtonPressed != null)
			{
				CancelButtonPressed(this, new EventArgs());
			}
		}

		public static readonly BindableProperty KeyboardThemeProperty =
			BindableProperty.Create(nameof(KeyboardTheme), typeof(KeyboardTheme), typeof(ExtendedEntry), KeyboardTheme.Light);

		public KeyboardTheme KeyboardTheme
		{
			get { return (KeyboardTheme)GetValue(KeyboardThemeProperty); }
			set { SetValue(KeyboardThemeProperty, value); }
		}
	}
}


