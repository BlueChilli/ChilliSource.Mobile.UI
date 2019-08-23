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
using DynamicData.Binding;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Contract for adding range handling capabilities to DynamicData observable collections
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface IObservableDynamicDataRangeCollection<T> : IObservableCollection<T>
	{
		void ReplaceRange(IEnumerable<T> items);
		void AddRange(IEnumerable<T> items, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add);
		void RemoveRange(IEnumerable<T> collection);
		void InsertRange(int index, IEnumerable<T> collection);
	}
}
