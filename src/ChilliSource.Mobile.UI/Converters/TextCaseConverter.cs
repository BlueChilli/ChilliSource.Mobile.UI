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
	public class TextCaseConverter : IValueConverter
	{
        public TextCaseConverter() { }

	    public TextCaseConverter(Func<string, string> caseConverter)
	    {
	        CaseConverter = caseConverter;
	    }

        public Func<string, string> CaseConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        var v = value as string;

	        if (v == null) return String.Empty;

	        return CaseConverter?.Invoke(v) ?? String.Empty;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        throw new NotImplementedException();
	    }
	}
}
