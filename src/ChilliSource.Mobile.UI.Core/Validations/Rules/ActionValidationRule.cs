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

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Custom validation rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ActionValidationRule<T> : IValidationRule<T>
	{
		readonly Func<T, bool> _predicate;

		public ActionValidationRule(Func<T, bool> predicate, string validationMessage)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			this._predicate = predicate;
			this.ValidationMessage = validationMessage;
		}


		public string ValidationMessage { get; set; }

		public bool Validate(T value)
		{
			return _predicate?.Invoke(value) ?? true;
		}

		public Task<bool> ValidateAsync(T value)
		{
			return Task.FromResult(Validate(value));
		}
	}


}
