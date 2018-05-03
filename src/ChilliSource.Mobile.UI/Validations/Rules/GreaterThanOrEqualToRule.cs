#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// "Greater than or equal" comparison validation rule.
    /// </summary>
    public class GreaterThanOrEqualToRule<T> : ComparisonRule<T>
    {
        /// <summary>
        /// Initializes a new instance with the value to compare to.
        /// </summary>
        /// <param name="otherValue">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        public GreaterThanOrEqualToRule(Func<T> otherValue, string validationMessage) : base(otherValue, validationMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance with the value to compare to and an <see cref="IComparer{T}"/>.
        /// </summary>
        /// <param name="otherValue">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        /// <param name="comparer">Comparer.</param>
        public GreaterThanOrEqualToRule(Func<T> otherValue, string validationMessage, IComparer<T> comparer) : base(otherValue, validationMessage, comparer)
        {
        }

        /// <summary>
        /// <see cref="Comparison"/> type 		
        /// </summary>
        public override Comparison Comparison => Comparison.GreaterThanEqualTo;
    }
}
