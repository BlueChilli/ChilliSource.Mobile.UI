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
   	public class GreaterThanRule<T> : ComparisonRule<T>
	{
		public GreaterThanRule(Func<T> otherVal, string validationMessage) : base(otherVal, validationMessage)
		{

		}

		public GreaterThanRule(Func<T> otherVal, string validationMessage, IComparer<T> comparer) : base(otherVal, validationMessage, comparer)
		{
		}

		public override Comparison Comparison => Comparison.GreaterThan;
	}
}
