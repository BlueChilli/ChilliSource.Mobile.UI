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
    /// Allows the execution of a custom action-based validation rule
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class ActionValidationRule<T> : IValidationRule<T>
    {
        readonly Func<T, bool> _predicate;

        /// <summary>
        /// Initializes a new instance with the async validation predicate
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <param name="validationMessage">Validation message.</param>
        public ActionValidationRule(Func<T, bool> predicate, string validationMessage)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            ValidationMessage = validationMessage;
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
            return _predicate?.Invoke(value) ?? true;
        }

        /// <summary>
        /// Validates the specified <paramref name="value"/> asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> specifying whether the execution was successful.</returns>
        /// <param name="value">Value.</param>
        public Task<bool> ValidateAsync(T value)
        {
            return Task.FromResult(Validate(value));
        }
    }


}
