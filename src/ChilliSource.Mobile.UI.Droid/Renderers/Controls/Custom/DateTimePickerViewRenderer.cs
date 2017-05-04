#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Java.Util;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(DateTimePickerView), typeof(DateTimePickerViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class DateTimePickerViewRenderer : ViewRenderer<DateTimePickerView, LinearLayout>, Android.Widget.DatePicker.IOnDateChangedListener, Android.Widget.TimePicker.IOnTimeChangedListener
	{

		protected override void OnElementChanged(ElementChangedEventArgs<DateTimePickerView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
			{
				return;
			}

			var linearLayout = new LinearLayout(Forms.Context);

			var datePicker = new Android.Widget.DatePicker(Forms.Context);
			Calendar calendar = Calendar.Instance;

			datePicker.MinDate = GetDateFromDateTime(Element.MinimumDateTime);
			datePicker.MaxDate = GetDateFromDateTime(Element.MaximumDateTime);

			datePicker.Init(calendar.Get(CalendarField.Year), calendar.Get(CalendarField.Month), calendar.Get(CalendarField.DayOfMonth), this);
			var datePickerParams = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent, 1);
			linearLayout.AddView(datePicker, datePickerParams);

			if (Element.PickerMode == PickerMode.DateTime)
			{
				var timePicker = new Android.Widget.TimePicker(Forms.Context);
				var timePickerParams = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent, 1);
				linearLayout.AddView(timePicker, timePickerParams);
			}

			SetNativeControl(linearLayout);
		}

		long GetDateFromDateTime(DateTime dateTime)
		{
			DateTime _start = new DateTime(1970, 1, 1);
			TimeSpan ts = (dateTime - _start);

			//Add Days to SetMax Days;
			return (long)(TimeSpan.FromDays(ts.Days).TotalMilliseconds);
		}

		// For some reason this method never triggeres
		public void OnDateChanged(Android.Widget.DatePicker view, int year, int monthOfYear, int dayOfMonth)
		{
			// assign values to (Element as StyledDateTimePicker).SelectedDateTime here
		}

		// For some reason this method never triggeres
		public void OnTimeChanged(Android.Widget.TimePicker view, int hourOfDay, int minute)
		{
			// assign values to (Element as StyledDateTimePicker).SelectedDateTime here
		}

		//Why does this happen? Well, this is a known bug on Android Lolipop and later. Is there anyway to fix it? In native, yes. You just change the modes to 'spinner'
		//on both pickers. This for some reason doesn't work here. So, what does work? Well, I haven't tried it, but looks like (from Google-ing) that using DatePickerDialog
		//and TimePickerDialog works. This is troublesome for us here since this control is suppost to be a view. A solution here might be to make two buttons that open dialogs.
		//I don't know weather this is whats needed, so I leave this code in a unfinished state so who ever needs it first can adopt it to their needs.
	}
}

