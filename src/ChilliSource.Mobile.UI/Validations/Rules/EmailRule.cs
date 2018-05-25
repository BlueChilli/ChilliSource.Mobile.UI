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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Email validation rule
    /// </summary>
    public class EmailRule<T> : IValidationRule<T> where T : class
    {
        const string _emailRegexPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private readonly Func<T, bool> _emailValidationPredicate;

        public static Regex EmailRegex => new Regex(_emailRegexPattern);

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public EmailRule()
        {
        }

           /// <summary>
        /// Initializes a new instance with custom validation predicate.
        /// </summary>
        /// <param name="EmailValidationPredicate">Is null or empty predicate.</param>
        /// <param name="validationMessage">Validation message.</param>
        public EmailRule(Func<T, bool> emailValidationPredicate, string validationMessage)
        {
            ValidationMessage = validationMessage;
            _emailValidationPredicate = emailValidationPredicate;
        }

        /// <summary>
        /// Initializes a new instance with a validation message.
        /// </summary>
        /// <param name="validationMessage">Validation message.</param>
        public EmailRule(string validationMessage) : this(null, validationMessage)
        {

        }

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Returns a value that identifies whether this email is valid
        /// </summary>
        /// <returns><c>True</c> if the provided value is valid, otherwise <c>false</c>.</returns>
        /// <param name="value">Value.</param>
        public bool Validate(T value)
        {
            var preValidation = _emailValidationPredicate?.Invoke(value) ?? value == null;

            if(!preValidation) return preValidation;

            var val = value as string;

            if(!String.IsNullOrWhiteSpace(val))
            {
                var match = EmailRegex.Match(val);

                return match.Success;
            }
            else
            {
                return preValidation;
            }

        }

        /// <summary>
        ///  Performs the email validation asynchronously.
        /// </summary>
        /// <returns>Returns a <see cref="Task{T}"/> holding the outcome of the validation.</returns>
        /// <param name="value">Value.</param>
        public Task<bool> ValidateAsync(T value)
        {
            return Task.FromResult(Validate(value));
        }
    }
}
