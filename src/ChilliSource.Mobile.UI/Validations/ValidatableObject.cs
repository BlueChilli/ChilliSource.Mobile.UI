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


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Wrapper that provides validation mechanism for any type
    /// </summary>
    public class ValidatableObject<T> : ObservableObject, IValidatableObject<T>
    {
        readonly List<IValidationRule<T>> _validations;
        readonly ObservableRangeCollection<string> _errors;

        bool _isValid;
        T _value;

        /// <summary>
        /// Initializes a new instance that is valid by default
        /// </summary>
        public ValidatableObject()
        {
            _validations = new List<IValidationRule<T>>();
            _errors = new ObservableRangeCollection<string>();
            IsValid = true;
        }

        /// <summary>
        /// Validation rules conforming to <see cref="IValidationRule{T}"/>
        /// </summary>        
        public List<IValidationRule<T>> Validations => _validations;

        /// <summary>
        /// Specifies whether the validation rules have passed.
        /// </summary>
        /// <value><c>true</c> if it is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return _isValid; }
            private set { SetProperty(ref _isValid, value, nameof(IsValid)); }
        }

        /// <summary>
        /// Underlying object being validated.
        /// </summary>
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, nameof(Value)); }
        }

        /// <summary>
        /// Validation errors that are populated if errors occured when <see cref="Validate"/> or <see cref="ValidateAsync"/> is called
        /// </summary>        
        public ObservableCollection<string> Errors => _errors;

        /// <summary>
        /// Determines if the validation rules specify the object to be required (non-empty)
        /// </summary>
        public bool IsRequired => _validations.Any(v => v is IsNotNullOrEmptyRule<T>);

        /// <summary>
        /// Performs validation based on provided rules and populates <see cref="Errors"/> if any errors occured 
        /// </summary>
        /// <returns><c>true</c> if validation successful, <c>false</c> otherwise.</returns>
        public bool Validate()
        {
            _errors.Clear();

            var errors = _validations.Where(v => !v.Validate(Value))
                                                     .Select(v => v.ValidationMessage);

            _errors.AddRange(errors);

            IsValid = !Errors.Any();

            return IsValid;
        }

        /// <summary>
        /// Forces validation to be passed
        /// </summary>
        public void MakeValid()
        {
            Errors.Clear();
            IsValid = true;
        }

        public void AddError(string message)
        {
            Errors.Add(message);
            IsValid = false;
        }

        /// <summary>
        /// Performs validation asynchronously based on provided rules and populates <see cref="Errors"/> if any errors occured
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> including whether the operation succeeded.</returns>
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

            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
