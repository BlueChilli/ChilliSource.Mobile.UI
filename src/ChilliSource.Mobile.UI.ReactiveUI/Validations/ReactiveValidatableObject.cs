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
using System.Text;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI.Core;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using System.Reactive.Disposables;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Validation objects that manages validation results
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ReactiveValidatableObject<T> : DisposableReactiveObject, IValidatableObject<T>
	{
		private readonly List<IValidationRule<T>> _validations;
		private readonly SourceList<string> _errors;
        private readonly ObservableRangeCollectionExtended<string> _observableCollection;
		public ReactiveValidatableObject()
		{
			_validations = new List<IValidationRule<T>>();
			_errors = new SourceList<string>();
			IsValid = true;
			this.WhenAnyValue(m => m.Value)
				.Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler)
				.DistinctUntilChanged()
				.SelectMany(async m => await this.ValidateAsync())
				.Subscribe()
                .DisposeWith(Disposables);
			
			_observableCollection = new ObservableRangeCollectionExtended<string>();

		     _errors.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
		        .Bind(_observableCollection)
		        .DisposeMany()
		        .Subscribe()
		        .DisposeWith(Disposables);

		}

		public List<IValidationRule<T>> Validations => _validations;

		private bool _isValid;

		public bool IsValid
		{
			get { return _isValid; }
			private set { this.RaiseAndSetIfChanged(ref _isValid, value); }
		}

		private T _value;
		public T Value
		{
			get { return _value; }
			set { this.RaiseAndSetIfChanged(ref _value, value); }
		}

		public ObservableCollection<string> Errors => _observableCollection;

		public bool IsRequired => _validations.Any(v => v is IsNotNullOrEmptyRule<T>);

		public bool Validate()
		{
			var errors = _validations.Where(v => !v.Validate(Value))
				 .Select(v => v.ValidationMessage);

			_errors.Edit(innerList =>
			{
				innerList.Clear();
				innerList.AddRange(errors);
                this.IsValid = !innerList.Any();
			});

		
			return this.IsValid;
		}

		public void MakeValid()
		{
			this._errors.Clear();
			this.IsValid = true;
		}

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

			_errors.Edit(innerList =>
			{
				innerList.Clear();
				innerList.AddRange(errors);
                this.IsValid = !innerList.Any();
			});
	
			return this.IsValid;
		}
	}
}
