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
    /// Represent async validation rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ActionAsyncValidationRule<T> : IValidationRule<T>
	{
		readonly Func<T, Task<bool>> _predicateAsync;

		public ActionAsyncValidationRule(Func<T, Task<bool>> predicateAsync, string validationMessage)
		{
			if (predicateAsync == null)
			{
				throw new ArgumentNullException(nameof(predicateAsync));
			}

			this._predicateAsync = predicateAsync;
			this.ValidationMessage = validationMessage;
		}

		public string ValidationMessage { get; set; }

		public bool Validate(T value)
		{
			throw new InvalidOperationException("You are trying to use synchronous method for async validation. Use ValidateAsync method instead");
		}

		public Task<bool> ValidateAsync(T value)
		{
			return _predicateAsync.Invoke(value);
		}
	}

}
