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
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Annotations;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
	public static class ObservableDynamicDataExtensions
	{
        /// <summary>
        /// Binds a clone of the observable changeset to the target <see cref="IObservableDynamicDataRangeCollection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="targetCollection">The target collection.</param>
        /// <param name="resetThreshold">The reset threshold.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// source or targetCollection
        /// </exception>
        public static IObservable<IChangeSet<T>> Bind<T>([NotNull] this IObservable<IChangeSet<T>> source,
			[NotNull] IObservableDynamicDataRangeCollection<T> targetCollection, int resetThreshold = 25)
		{
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (targetCollection == null)
            {
                throw new ArgumentNullException(nameof(targetCollection));
            }

			var adaptor = new ObservableListToObservableDynamicDataRangeCollectionAdapter<T>(targetCollection, resetThreshold);
			return source.Adapt(adaptor);
		}
	}
}
