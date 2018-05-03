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
    /// Allows the conversion of a boolean value to an object of type <typeparam name="T"/>, which
    /// is achieved by specifying a <see cref="TrueObject"/> and a <see cref="FalseObject"/> to be returned
    /// based on the source boolean value.
    /// </summary>
	public class BooleanToObjectConverter<T> : IValueConverter
    {
        /// <summary>
        /// The object to return when the source boolean value is true
        /// </summary>
        public T TrueObject { get; set; }

        /// <summary>
        /// The object to return when the source boolean value is false
        /// </summary>
        public T FalseObject { get; set; }

        /// <summary>
        /// Performs the conversion by returning either the <see cref="TrueObject"/> or the <see cref="FalseObject"/>
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Boolean source value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture info.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? TrueObject : FalseObject;
        }

        /// <summary>
        /// Performs the reverse conversion by assessing whether the provided <see cref="value"/> equals the <see cref="TrueObject"/>
        /// </summary>
        /// <returns>The converted object.</returns>
        /// <param name="value">Value.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((T)value).Equals(TrueObject);
        }
    }
}
