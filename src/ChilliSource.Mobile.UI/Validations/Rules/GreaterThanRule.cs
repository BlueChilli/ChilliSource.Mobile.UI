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
    /// "Greater than" comparison validation rule.
    /// </summary>
    public class GreaterThanRule<T> : ComparisonRule<T>
    {
        /// <summary>
        /// Initializes a new instance with the value to compare to.
        /// </summary>
        /// <param name="otherVal">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        public GreaterThanRule(Func<T> otherVal, string validationMessage) : base(otherVal, validationMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance with the value to compare to and an <see cref="IComparer{T}"/>.
        /// </summary>
        /// <param name="otherVal">Other value.</param>
        /// <param name="validationMessage">Validation message.</param>
        /// <param name="comparer">Comparer.</param>
        public GreaterThanRule(Func<T> otherVal, string validationMessage, IComparer<T> comparer) : base(otherVal, validationMessage, comparer)
        {
        }

        /// <summary>
        /// <see cref="Comparison"/> type       
        /// </summary>
        public override Comparison Comparison => Comparison.GreaterThan;
    }
}
