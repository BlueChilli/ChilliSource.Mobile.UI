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
using System.Text;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Validation to check if item is not null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
	{
		private static Func<T, bool> _defaultIsNullOrEmpty = v =>
		{
			var val = v as string;
			return String.IsNullOrWhiteSpace(val);
		};

		public IsNotNullOrEmptyRule(string validationMessage) :
			this(_defaultIsNullOrEmpty, validationMessage)
		{ }

		readonly Func<T, bool> isNullOrEmptyPredicate;

		public IsNotNullOrEmptyRule(Func<T, bool> isNullOrEmptyPredicate, string validationMessage)
		{
			if (isNullOrEmptyPredicate == null)
			{
				throw new ArgumentNullException(nameof(isNullOrEmptyPredicate));
			}

			this.ValidationMessage = validationMessage;
			this.isNullOrEmptyPredicate = isNullOrEmptyPredicate;
		}

		public string ValidationMessage { get; set; }

		public bool Validate(T value)
		{
			if (value == null) return false;

			return !this.isNullOrEmptyPredicate.Invoke(value);
		}

		public Task<bool> ValidateAsync(T value)
		{
			return Task.FromResult(Validate(value));
		}
	}

}
