#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Handles comparison of nullable objects
    /// </summary>
    public class NullableComparer<T> : IComparer<T>
    {

        /// <summary>
        /// Compares the specified nullable <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        /// <returns>A <see cref="int"/> representing the comparison result.</returns>
        /// <param name="x">x.</param>
        /// <param name="y">y.</param>
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
