#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xunit;
using ChilliSource.Mobile.UI.Core;
using System.Threading.Tasks;

namespace Validations
{
	
	public class GreatherThanOrEqualToValidationRuleTests
	{

		[Fact]
		public void ctor_ShouldThrowException_When_OtherValueProperty_Is_Null()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new GreaterThanOrEqualToRule<string>(null, "Password must be same"));
		}

		[Fact]
		public void Validate_ShouldBeFalse_When_Both_Value_Or_OtherValue_Is_Null()
		{
			var rule = new GreaterThanOrEqualToRule<string>(() => null, "Password must be same");
			var isValid = rule.Validate(null);
			Assert.True(isValid);
		}

		[Fact]
		public void Validate_ShouldBeFalse_When_OtherValue_Is_Null()
		{
			var rule = new GreaterThanOrEqualToRule<string>(() => null, "Password must be same");
			var isValid = rule.Validate("Test");
			Assert.True(isValid);
		}

		[Fact]
		public void Validate_ShouldBeTrue_When_Value_Is_Null()
		{
			var rule = new GreaterThanOrEqualToRule<string>(() => "test", "Password must be same");
			var isValid = rule.Validate(null);
			Assert.False(isValid);
		}


		[Fact]
		public void Validate_ShouldValidate_When_Value_Is_GreaterThan_OtherValue()
		{
			var rule = new GreaterThanOrEqualToRule<int>(() => 5, "Password must be same");
			var isValid = rule.Validate(6);
			Assert.True(isValid);
		}

		[Fact]
		public async Task ValidateAsync_ShouldValidateAsyn_When_Value_Is_GreaterThan_OtherValue()
		{
			var rule = new GreaterThanOrEqualToRule<int>(() => 5, "Password must be same");
			var isValid = await rule.ValidateAsync(6);
			Assert.True(isValid);
		}



	}
}
