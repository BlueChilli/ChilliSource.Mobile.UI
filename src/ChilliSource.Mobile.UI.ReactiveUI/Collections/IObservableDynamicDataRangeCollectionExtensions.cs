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
using System.Reactive.Linq;
using DynamicData;
using System.Linq;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    internal static class IObservableDynamicDataRangeCollectionExtensions
	{
		internal static void CloneObservableDynamicDataRangeCollection<T>(this IObservableDynamicDataRangeCollection<T> source, IChangeSet<T> changes)
		{
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (changes == null)
            {
                throw new ArgumentNullException(nameof(changes));
            }

			var i = 0;
			foreach (var item in changes)
			{
				switch (item.Reason)
				{
					case ListChangeReason.Add:
						{
							var change = item.Item;
							var hasIndex = change.CurrentIndex >= 0;

                            if (hasIndex)
							{
								source.Insert(change.CurrentIndex, change.Current);
							}
							else
							{
								source.Add(change.Current);
							}
							break;
						}

					case ListChangeReason.AddRange:
					{
							var previousChange = changes.ElementAtOrDefault(i - 1);

							if (previousChange != null && previousChange.Reason == ListChangeReason.Clear)
							{
								source.ReplaceRange(item.Range);
							}
							else
                            {
								source.AddOrInsertRange(item.Range, item.Range.Index);
							}
						    break; 
					}

					case ListChangeReason.Clear:
						{
							var nextChange = changes.ElementAtOrDefault(i + 1);

							if (nextChange != null && nextChange.Reason != ListChangeReason.AddRange)
							{
								source.Clear();
							}

							break;
						}

					case ListChangeReason.Replace:
						{

							var change = item.Item;
							bool hasIndex = change.CurrentIndex >= 0;

                            if (hasIndex && change.CurrentIndex == change.PreviousIndex)
							{
								source[change.CurrentIndex] = change.Current;
							}
							else
							{
								source.RemoveAt(change.PreviousIndex);
								source.Insert(change.CurrentIndex, change.Current);
							}
                            break;
                        }
						
					case ListChangeReason.Remove:
						{
							var change = item.Item;
							bool hasIndex = change.CurrentIndex >= 0;
							if (hasIndex)
							{
								source.RemoveAt(change.CurrentIndex);
							}
							else
							{
								source.Remove(change.Current);
							}
							break;
						}

					case ListChangeReason.RemoveRange:
						{
							source.RemoveRange(item.Range);
                            break;
                        }
						
					case ListChangeReason.Moved:
						{
							var change = item.Item;
							bool hasIndex = change.CurrentIndex >= 0;
                            if (!hasIndex)
                            {
                                throw new UnspecifiedIndexException("Cannot move as an index was not specified");
                            }

							source.Move(change.PreviousIndex, change.CurrentIndex);
							break;
						}
				}

				i++;

			}
		}
	}
}
