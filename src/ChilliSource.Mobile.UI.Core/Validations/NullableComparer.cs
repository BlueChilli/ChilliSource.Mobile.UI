#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;

namespace ChilliSource.Mobile.UI.Core
{
	public class NullableComparer<T> : IComparer<T>
	{
		public int Compare(T x, T y)
		{
			if (x == null && y == null)
			{
				return 0;
			}

			if (x != null && y == null)
			{
				return 1;
			}

			if (x == null && y != null)
			{
				return -1;
			}

			return Comparer<T>.Default.Compare(x, y);
		}
	}
}
