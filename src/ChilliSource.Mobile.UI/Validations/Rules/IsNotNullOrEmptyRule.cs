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
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// "Not null or empty" validation rule
    /// </summary>
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        readonly Func<T, bool> _isNotNullOrEmptyPredicate;

        static readonly Func<T, bool> DefaultIsNotNullOrEmpty = v =>
        {
            var val = v as string;
            return !String.IsNullOrWhiteSpace(val);
        };

        /// <summary>
        /// Initializes a new instance and uses default string validation predicate.
        /// </summary>
        /// <param name="validationMessage">Validation message.</param>
        public IsNotNullOrEmptyRule(string validationMessage) :
            this(DefaultIsNotNullOrEmpty, validationMessage)
        { }



        /// <summary>
        /// Initializes a new instance with custom validation predicate.
        /// </summary>
        /// <param name="isNotNullOrEmptyPredicate">Is null or empty predicate.</param>
        /// <param name="validationMessage">Validation message.</param>
        public IsNotNullOrEmptyRule(Func<T, bool> isNotNullOrEmptyPredicate, string validationMessage)
        {
            ValidationMessage = validationMessage;
            _isNotNullOrEmptyPredicate = isNotNullOrEmptyPredicate ?? throw new ArgumentNullException(nameof(isNotNullOrEmptyPredicate));
        }

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
           return _isNotNullOrEmptyPredicate?.Invoke(value) ?? true;
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
