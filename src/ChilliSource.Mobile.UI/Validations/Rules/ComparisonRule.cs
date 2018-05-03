#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* inspired from
 * Source:     BikeSharing360 (https://github.com/Microsoft/BikeSharing360_MobileApps)
 * Author:     Microsoft (https://github.com/Microsoft)
 * License:    MIT https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Comparison validation rule
    /// </summary>
    public abstract class ComparisonRule<T> : IValidationRule<T>
    {
        readonly Func<T> _otherValue;
        readonly IComparer<T> _comparer;

        /// <summary>
        /// Initializes a new instance with the value to compare to.
        /// </summary>
        /// <param name="otherValue">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        protected ComparisonRule(Func<T> otherValue, string validationMessage) : this(otherValue, validationMessage, new NullableComparer<T>())
        {

        }

        /// <summary>
        /// Initializes a new instance with the value to compare to and an <see cref="IComparer{T}"/>.
        /// </summary>
        /// <param name="otherValue">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        /// <param name="comparer">Comparer.</param>
        protected ComparisonRule(Func<T> otherValue, string validationMessage, IComparer<T> comparer)
        {
            _otherValue = otherValue ?? throw new ArgumentNullException(nameof(otherValue));
            ValidationMessage = validationMessage;
            _comparer = comparer;
        }

        /// <summary>
        /// <see cref="Comparison"/> type to be overriden in subclasses.
        /// </summary>
        public abstract Comparison Comparison { get; }

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Validates the specified <paramref name="value"/>.
        /// </summary>
        /// <returns><c>true</c> if it is valid, otherwise <c>false</c>.</returns>
        /// <param name="value">Value.</param>
        public bool Validate(T value)
        {
            var result = _comparer.Compare(value, _otherValue());

            switch (Comparison)
            {
                case Comparison.LessThan:
                    return result < 0;
                case Comparison.LessThanEqualTo:
                    return result <= 0;
                case Comparison.GreaterThan:
                    return result > 0;
                case Comparison.GreaterThanEqualTo:
                    return result >= 0;
                default:
                    return result == 0;
            }
        }

        /// <summary>
        /// Validates the specified <paramref name="value"/> asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> including the operation result.</returns>
        /// <param name="value">Value.</param>
        public Task<bool> ValidateAsync(T value)
        {
            return Task.FromResult(Validate(value));
        }
    }
}
