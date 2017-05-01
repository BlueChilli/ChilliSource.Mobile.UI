#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI.Core;
using Xunit;

namespace Validations
{
	
	public class RepeatPassworRuleTests
	{
		[Fact]
		public void Validate_ShouldFailValidation_When_Password_Is_Not_Same()
		{
			var rule = new RepeatPasswordRule<string>(() => "test", "Password must be same");

			var isValid = rule.Validate("dope");
			Assert.False(isValid);
		}

		[Fact]
		public void Validate_ShouldBeTrue_When_Password_Is_Null()
		{
			var rule = new RepeatPasswordRule<string>(() => null, "Password must be same");

			var isValid = rule.Validate(null);
			Assert.True(isValid);
		}
	}
}
