#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* inspired from
Source:     BikeSharing360 (https://github.com/Microsoft/BikeSharing360_MobileApps)
Author:     Microsoft (https://github.com/Microsoft)
License:    MIT https://opensource.org/licenses/MIT
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Reactive.Disposables;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Reactive-based wrapper that provides validation mechanism for any type
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ReactiveValidatableObject<T> : DisposableReactiveObject, IValidatableObject<T>
	{
		private readonly List<IValidationRule<T>> _validations;
        private readonly ObservableRangeCollection<string> _errors;
        private bool _isValid;
        private bool _isDirty;
        private T _value;

        /// <summary>
        /// Initializes a new instance that is valid by default
        /// </summary>
         public ReactiveValidatableObject()
		{
			_validations = new List<IValidationRule<T>>();
            _errors = new ObservableRangeCollection<string>();
			IsValid = true;
        
			this.WhenAnyValue(m => m.Value, m => m.IsDirty, (v, d) => { 
                    (T val , bool isDirty) result = (v, d);
                    return result; 
                })
                .Where(m => m.isDirty)
                .Select(m => m.val)
				.Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler)
				.DistinctUntilChanged()
				.SelectMany(async m => await this.ValidateAsync())
				.Subscribe()
                .DisposeWith(Disposables);

    	}

        /// <summary>
        /// Validation rules conforming to <see cref="IValidationRule{T}"/>
        /// </summary>        
        public List<IValidationRule<T>> Validations => _validations;

         /// <summary>
        /// Specifies whether the validation rules have passed.
        /// </summary>
        /// <value><c>true</c> if it is valid; otherwise, <c>false</c>.</value>
        public bool IsDirty
		{
			get { return _isDirty; }
		    set { this.RaiseAndSetIfChanged(ref _isDirty, value); }
		}

        /// <summary>
        /// Specifies whether value is dirty or not.
        /// </summary>
        /// <value><c>true</c> if it is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
		{
			get { return _isValid; }
			private set { this.RaiseAndSetIfChanged(ref _isValid, value); }
		}

        /// <summary>
        /// Underlying object being validated.
        /// </summary>
        public T Value
		{
			get { return _value; }
			set { this.RaiseAndSetIfChanged(ref _value, value); }
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
			var errors = _validations.Where(v => !v.Validate(Value))
				 .Select(v => v.ValidationMessage);

            _errors.Clear();
            _errors.AddRange(errors);

            MarkAsDirty();
            IsValid = !_errors.Any();
            return IsValid;
		}

        /// <summary>
        /// Forces validation to be passed
        /// </summary>
        public void MakeValid()
		{
			_errors.Clear();
            IsDirty = false;
			IsValid = true;
		}


        public void AddError(string message)
        {
            _errors.Add(message);
            IsValid = false;
        }

        /// <summary>
        /// Performs validation asynchronously based on provided rules and populates <see cref="Errors"/> if any errors occured
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> including whether the operation succeeded.</returns>
        public async Task<bool> ValidateAsync()
		{
			var errors = await _validations.ToObservable()
						.SelectMany(v =>
						{
							return v.ValidateAsync(Value)
									.ToObservable()
									.Select((arg) => new { isValid = arg, v.ValidationMessage });
						})
						.Where(result => !result.isValid)
						.Select(result => result.ValidationMessage)
						.ToList();

            _errors.Clear();
            _errors.AddRange(errors);
            
            MarkAsDirty();
            IsValid = !_errors.Any();
        	return IsValid;
		}

        private void MarkAsDirty()
        {
            if(!IsDirty) IsDirty = true;
        }

	}
}
