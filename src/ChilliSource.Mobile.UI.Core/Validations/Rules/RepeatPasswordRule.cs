#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* inspired from
 * Source:     BikeSharing360 (https://github.com/Microsoft/BikeSharing360_MobileApps)
 * Author:     Microsoft (https://github.com/Microsoft)
 * License:    MIT https://opensource.org/licenses/MIT
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Validate repeated password 
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class RepeatPasswordRule<T> : ComparisonRule<T> where T : IComparable<T>
	{
		public RepeatPasswordRule(Func<T> otherPassword, string validationMessage)
			: base(otherPassword, validationMessage)
		{

		}

		public override Comparison Comparison => Comparison.Equals;
	}
}
