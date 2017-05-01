#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class BooleanToObjectConverter<T> : IValueConverter
	{
        public T TrueObject { get; set; }
        public T FalseObject { get; set; }

	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return ((bool) value) ? TrueObject : FalseObject;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return ((T) value).Equals(TrueObject);
	    }
	}
}
