#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using ChilliSource.Mobile.UI.Core;
using Xunit;

namespace Validations
{
	
	public class ActionAsyncValidationRuleTests
	{
		[Fact]
		public void Validate_ShouldThrowException_WhenCalled()
		{
			var rule = new ActionAsyncValidationRule<string>(arg => { return Task.FromResult(true); }, "You have entered an invalid email");
			Assert.Throws<InvalidOperationException>(() => rule.Validate(String.Empty));
		}

		[Fact]
		public async Task ValidateAsync_ShouldValidate_WhenCalled()
		{
			var rule = new ActionAsyncValidationRule<string>(arg => { return Task.FromResult(true); }, "You have entered an invalid email");
			var isValid = await rule.ValidateAsync("hello");
			Assert.True(isValid);
		}

	}
}
