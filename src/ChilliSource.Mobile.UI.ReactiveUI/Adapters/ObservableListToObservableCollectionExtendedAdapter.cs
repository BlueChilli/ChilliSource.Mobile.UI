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
using DynamicData;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
	/// <summary>
	/// Adaptor used to populate a <see cref="IObservableCollectionExtended{TObject}"/> from an observable changeset.
	/// </summary>
	/// <typeparam name="TObject">The type of the object.</typeparam>
	public class ObservableListToObservableCollectionExtendedAdapter<TObject> : IChangeSetAdaptor<TObject>
	{
		private bool _loaded;
		private readonly IObservableCollectionExtended<TObject> _target;
		private readonly int _resetThreshold;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableListToObservableCollectionExtendedAdapter{TObject,TKey}"/> class.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="resetThreshold">The reset threshold.</param>
		/// <exception cref="System.ArgumentNullException">target</exception>
		public ObservableListToObservableCollectionExtendedAdapter(IObservableCollectionExtended<TObject> target, int resetThreshold = 50)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));
			_target = target;
			_resetThreshold = resetThreshold;
		}


		/// <summary>
		/// Maintains the specified collection from the changes
		/// </summary>
		/// <param name="changes">The changes.</param>
		public void Adapt(IChangeSet<TObject> changes)
		{
			if (changes.Count > _resetThreshold || !_loaded)
			{
				using (_target.SuspendNotifications())
				{
					_target.CloneObservableCollectionExtended(changes);
					_loaded = true;
				}
			}
			else
			{
				_target.CloneObservableCollectionExtended(changes);
			}
		}
	}
}
