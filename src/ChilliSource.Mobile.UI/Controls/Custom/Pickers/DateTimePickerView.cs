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
	public enum PickerMode
	{
		DateTime,
		Date,
	}

	public class DateTimePickerView : View
	{
		public DateTimePickerView()
		{
		}

		public static readonly BindableProperty MinimumDateTimeProperty =
			BindableProperty.Create(nameof(MinimumDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.MinValue);

		public DateTime MinimumDateTime
		{
			get { return (DateTime)GetValue(MinimumDateTimeProperty); }
			set { SetValue(MinimumDateTimeProperty, value); }
		}

		public static readonly BindableProperty MaximumDateTimeProperty =
			BindableProperty.Create(nameof(MaximumDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.MaxValue);

		public DateTime MaximumDateTime
		{
			get { return (DateTime)GetValue(MaximumDateTimeProperty); }
			set { SetValue(MaximumDateTimeProperty, value); }
		}

		public static readonly BindableProperty SelectedDateTimeProperty =
			BindableProperty.Create(nameof(SelectedDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.Now);

		public DateTime SelectedDateTime
		{
			get { return (DateTime)GetValue(SelectedDateTimeProperty); }
			set { SetValue(SelectedDateTimeProperty, value); }
		}

		public static readonly BindableProperty PickerModeProperty =
			BindableProperty.Create(nameof(PickerMode), typeof(PickerMode), typeof(DateTimePickerView), PickerMode.DateTime);

		public PickerMode PickerMode
		{
			get { return (PickerMode)GetValue(PickerModeProperty); }
			set { SetValue(PickerModeProperty, value); }
		}
	}
}

