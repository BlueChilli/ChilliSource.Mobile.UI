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
    /// Allows the async execution of a custom action-based validation rule
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class ActionAsyncValidationRule<T> : IValidationRule<T>
    {
        readonly Func<T, Task<bool>> _predicateAsync;

        /// <summary>
        /// Initializes a new instance with the async validation predicate
        /// </summary>
        /// <param name="asyncPredicate">Async predicate.</param>
        /// <param name="validationMessage">Validation message.</param>
        public ActionAsyncValidationRule(Func<T, Task<bool>> asyncPredicate, string validationMessage)
        {
            _predicateAsync = asyncPredicate ?? throw new ArgumentNullException(nameof(asyncPredicate));
            ValidationMessage = validationMessage;
        }

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Throws InvalidOperationException because synchronous validation is not supported by this class.
        /// </summary>		
        /// <param name="value">Value.</param>
        public bool Validate(T value)
        {
            throw new InvalidOperationException("You are trying to use synchronous method for async validation. Use the ValidateAsync method instead");
        }

        /// <summary>
        /// Performs the validation of the specified <paramref name="value"/> asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> including the operation result.</returns>
        /// <param name="value">Generic type value.</param>
        public Task<bool> ValidateAsync(T value)
        {
            return _predicateAsync.Invoke(value);
        }
    }

}
