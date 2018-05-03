#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* based on
Source: 	DynamicData (https://github.com/RolandPheasant/DynamicData)
Author: 	Roland Pheasant (https://github.com/RolandPheasanto)
License:	Microsoft Public License (https://github.com/RolandPheasant/DynamicData/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive.Disposables;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// An observable range collection which allows the suspension of notifications
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ObservableDynamicDataRangeCollection<T> : ObservableRangeCollection<T>, IObservableDynamicDataRangeCollection<T>
	{
		public void Load(IEnumerable<T> items)
		{
			this.ReplaceRange(items);
		}

		#region Implementation of IObservableCollection

		private bool _suspendNotifications;
		private bool _suspendCount;

		/// <summary>
		/// Suspends notifications. When disposed, a reset notification is fired
		/// </summary>
		/// <returns></returns>
		public IDisposable SuspendNotifications()
		{
			_suspendCount = true;
			_suspendNotifications = true;

			return Disposable.Create(() =>
			{
				_suspendCount = false;
				_suspendNotifications = false;
				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			});
		}

		/// <summary>
		/// Suspends count notifications
		/// </summary>
		/// <returns></returns>
		public IDisposable SuspendCount()
		{
			var count = this.Count;
			_suspendCount = true;

			return Disposable.Create(() =>
			{
				_suspendCount = false;

                if (Count != count)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                }
			});
		}

		/// <summary>
		/// Raises the <see cref="E:PropertyChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
            if (_suspendCount && e.PropertyName == "Count")
            {
                return;
            }

			base.OnPropertyChanged(e);
		}

		/// <summary>
		/// Raises the <see cref="E:CollectionChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
            if (_suspendNotifications)
            {
                return;
            }

			base.OnCollectionChanged(e);
		}

		#endregion
	}
}
