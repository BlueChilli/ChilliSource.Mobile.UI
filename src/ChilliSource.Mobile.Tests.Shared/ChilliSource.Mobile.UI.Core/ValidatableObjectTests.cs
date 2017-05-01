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
	
	public class ValidatableObjectTest
	{

		[Fact]
		public void Validate_ShouldRaisePropertyChanged_When_Value_Is_Changed()
		{
			var v = new ValidatableObject<string>();

			v.Validations.Add(new IsNotNullOrEmptyRule<string>("Email must not be empty"));

			v.Validations.Add(new EmailRule<string>()
			{
				ValidationMessage = "You have entered an invalid email"
			});

			bool isPropertyChangedRaised = false;

			v.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName.Equals(nameof(v.Value)))
				{
					isPropertyChangedRaised = true;
					v.Validate();
				}

			};

			Assert.True(v.IsValid);
			v.Value = "t";
			Assert.False(v.IsValid);
			Assert.True(isPropertyChangedRaised);
			Assert.Equal("You have entered an invalid email", v.Errors[0]);
		}

		[Fact]
		public void Validate_ShouldSetIsValid_When_Executed()
		{
			var v = new ValidatableObject<string>();

			v.Validations.Add(new IsNotNullOrEmptyRule<string>("Email must not be empty"));

			v.Validations.Add(new EmailRule<string>()
			{
				ValidationMessage = "You have entered an invalid email"
			});

			Assert.True(v.IsValid);
			v.Validate();
			Assert.False(v.IsValid);
			Assert.Equal("Email must not be empty", v.Errors[0]);
		}

		[Fact]
		public void Validate_IsValidShouldBeTrue_When_Valid_Email_Is_Given()
		{
			var v = new ValidatableObject<string>();

			v.Validations.Add(new IsNotNullOrEmptyRule<string>("Email must not be empty"));

			v.Validations.Add(new EmailRule<string>()
			{
				ValidationMessage = "You have entered an invalid email"
			});

			Assert.True(v.IsValid);
			v.Value = "test@test.com";
			v.Validate();
			Assert.True(v.IsValid);
		}

		[Fact]
		public async Task ValidateAsync_IsValidShouldBeFalse_When_AsyncValidationDelegateReturnsFalse()
		{
			var v = new ValidatableObject<string>();

			v.Validations.Add(new ActionAsyncValidationRule<string>(arg => { return Task.FromResult(false); }, "You have entered an invalid email"));

			Assert.True(v.IsValid);
			v.Value = "test@test.com";
			await v.ValidateAsync();
			Assert.False(v.IsValid);
			Assert.True(v.Errors.Contains("You have entered an invalid email"));
		}
	}
}
