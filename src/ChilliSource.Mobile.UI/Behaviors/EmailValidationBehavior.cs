#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class EmailValidationBehavior : Behavior<Entry>
	{
		const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
		@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

		public static readonly BindableProperty IsValidProperty =
		BindableProperty.CreateAttached(nameof(IsValid), typeof(bool), typeof(EmailValidationBehavior), false);

		public bool IsValid
		{
			get { return (bool)GetValue(IsValidProperty); }
			private set { SetValue(IsValidProperty, value); }
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			IsValid = (Regex.IsMatch(e.NewTextValue, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;

		}
	}
}
