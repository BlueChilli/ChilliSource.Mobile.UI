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
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(DateTimePickerView), typeof(DateTimePickerViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class DateTimePickerViewRenderer : ViewRenderer<DateTimePickerView, UIDatePicker>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DateTimePickerView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
			{
				return;
			}

			var datePicker = new UIDatePicker();
			datePicker.MinimumDate = Element.MinimumDateTime.ToNSDate();
			datePicker.MaximumDate = Element.MaximumDateTime.ToNSDate();
			datePicker.TimeZone = NSTimeZone.LocalTimeZone;
			datePicker.MinuteInterval = 5;

			switch (Element.PickerMode)
			{
				case PickerMode.Date:
					datePicker.Mode = UIDatePickerMode.Date;
					break;

				case PickerMode.DateTime:
					datePicker.Mode = UIDatePickerMode.DateAndTime;
					break;
			}

			datePicker.Date = Element.SelectedDateTime.ToNSDate();
			datePicker.Locale = NSLocale.CurrentLocale;
			datePicker.ValueChanged += (object sender, EventArgs ev) =>
			{
				Element.SelectedDateTime = (sender as UIDatePicker).Date.ToDateTime();
			};
			SetNativeControl(datePicker);
		}
	}
}

