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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Rule to validate email
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class EmailRule<T> : IValidationRule<T>
	{
		const string _emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
		static Regex _regex = new Regex(_emailRegex);

		public EmailRule()
		{
		}

		public EmailRule(string validationMessage)
		{
			ValidationMessage = validationMessage;
		}

		public string ValidationMessage { get; set; }

		public bool Validate(T value)
		{
			if (value == null)
			{
				return false;
			}

			var val = value as string;

			var match = _regex.Match(val);

			return match.Success;
		}

		public Task<bool> ValidateAsync(T value)
		{
			return Task.FromResult(Validate(value));
		}
	}
}
