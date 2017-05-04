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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Validation object to store validation results and errors
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ValidatableObject<T> : ObservableObject, IValidatableObject<T>
	{
		private readonly List<IValidationRule<T>> _validations;
		private readonly ObservableRangeCollection<string> _errors;

		public ValidatableObject()
		{
			_validations = new List<IValidationRule<T>>();
			_errors = new ObservableRangeCollection<string>();
			IsValid = true;
		}

		public List<IValidationRule<T>> Validations => _validations;

		private bool _isValid;

		public bool IsValid
		{
			get { return _isValid; }
			private set { SetProperty(ref _isValid, value, nameof(IsValid)); }
		}

		private T _value;
		public T Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value, nameof(Value)); }
		}

		public ObservableCollection<string> Errors => _errors;

		public bool IsRequired => _validations.Any(v => v is IsNotNullOrEmptyRule<T>);

		public bool Validate()
		{
			_errors.Clear();

			var errors = _validations.Where(v => !v.Validate(Value))
													 .Select(v => v.ValidationMessage);

			_errors.AddRange(errors);

			this.IsValid = !this.Errors.Any();

			return this.IsValid;
		}

		public void MakeValid()
		{
			this.Errors.Clear();
			this.IsValid = true;
		}

		public async Task<bool> ValidateAsync()
		{
			_errors.Clear();

			var results = new List<Task<bool>>();

			foreach (var validation in _validations)
			{
				results.Add(validation.ValidateAsync(Value));
			}

			var r = await Task.WhenAll(results);
			var errors = new List<string>();
			for (var i = 0; i < _validations.Count; i++)
			{
				var isValid = r[i];
				var validation = _validations[i];

				if (!isValid)
				{
					errors.Add(validation.ValidationMessage);
				}
			}

			_errors.AddRange(errors);

			this.IsValid = !this.Errors.Any();

			return this.IsValid;
		}
	}
}
