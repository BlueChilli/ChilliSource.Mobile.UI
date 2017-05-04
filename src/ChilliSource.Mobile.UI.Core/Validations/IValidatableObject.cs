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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI.Core
{
	public interface IValidatableObject<T> : IValidity
	{
		bool Validate();
		Task<bool> ValidateAsync();
		List<IValidationRule<T>> Validations { get; }
		ObservableCollection<string> Errors { get; }
		bool IsRequired { get; }
		T Value { get; set; }
		void MakeValid();
	}
}
