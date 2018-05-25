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
     
        public static Regex EmailRegex => new Regex(_emailRegexPattern);
        readonly Func<T, bool> _emailValidatorPredicate;

        /// <summary>
        /// Initializes a new instance with custom validation predicate.
        /// </summary>
        /// <param name="validationMessage">Validation message.</param>
        public EmailRule(string validationMessage)
        {
            ValidationMessage = validationMessage;
         }

        /// <summary>
        /// Initializes a new instance with custom validation predicate.
        /// </summary>
        /// <param name="validationMessage">Validation message.</param>
        public EmailRule(Func<T, bool> validator, string validationMessage)
        {
            ValidationMessage = validationMessage;
            _emailValidatorPredicate = validator;
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
            if(_emailValidatorPredicate != null) 
            {
                return _emailValidatorPredicate.Invoke(value);    
            }

            var val = value as string;

            if(!String.IsNullOrWhiteSpace(val))
            {
                var match = EmailRegex.Match(val);

                return match.Success;
            }
            else
            {
                return true;
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
