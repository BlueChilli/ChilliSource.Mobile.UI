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
  
    public class DebugConverter : IValueConverter
    {
        /// <summary>
        /// Convert the specified <paramref name="value"/> to the <paramref name="targetType"/>.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        /// <summary>
        /// Converts the <paramref name="value"/> back from the <paramref name="targetType"/>.
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}

