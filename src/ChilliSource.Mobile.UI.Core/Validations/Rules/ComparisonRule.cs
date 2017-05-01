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

namespace ChilliSource.Mobile.UI.Core
{
	public abstract class ComparisonRule<T> : IValidationRule<T>
	{
		readonly Func<T> _otherValue;
		readonly IComparer<T> _comparer;

		public ComparisonRule(Func<T> otherValue, string validationMessage) : this(otherValue, validationMessage, new NullableComparer<T>())
		{

		}

		public ComparisonRule(Func<T> otherValue, string validationMessage, IComparer<T> comparer)
		{
			if (otherValue == null)
			{
				throw new ArgumentNullException(nameof(otherValue));
			}

			_otherValue = otherValue;
			ValidationMessage = validationMessage;
			_comparer = comparer;
		}

		public abstract Comparison Comparison { get; }

		public string ValidationMessage { get; set; }

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

		public Task<bool> ValidateAsync(T value)
		{
			return Task.FromResult(Validate(value));
		}
	}
}
