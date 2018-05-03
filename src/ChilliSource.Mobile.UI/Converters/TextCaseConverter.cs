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
    /// <summary>
    /// Executes custom string converter for XAML data bindings
    /// </summary>
	public class TextCaseConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <c>TextCaseConverter</c>  class.
        /// </summary>
        public TextCaseConverter() { }

        /// <summary>
        /// Initializes a new instance of the <c>TextCaseConverter</c> class.
        /// </summary>
        /// <param name="caseConverter">Case converter.</param>
        public TextCaseConverter(Func<string, string> caseConverter)
        {
            CaseConverter = caseConverter;
        }

        /// <summary>
        /// Gets or sets the case converter.
        /// </summary>
        /// <value>The case converter.</value>
        public Func<string, string> CaseConverter { get; set; }


        /// <summary>
        /// Converts the specified <paramref name="value"/> to the <paramref name="targetType"/>.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as string;

            if (v == null) return String.Empty;

            return CaseConverter?.Invoke(v) ?? String.Empty;
        }


        /// <summary>
        /// Converts the <paramref name="value"/> back.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
