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
using ReactiveUI.Testing;
using Microsoft.Reactive.Testing;
using ChilliSource.Mobile.UI.ReactiveUI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using ChilliSource.Mobile.Tests;

namespace ChilliSource.ReactiveUI
{
	
	public class ReactiveValidatableObjectTest
	{

		[Fact]
		public void IsValid_ShouldValidate_WhenValueIsChanged()
		{
			var scheduler = new TestScheduler();
			scheduler.With((TestScheduler sched) =>
			{

				var v = new ReactiveValidatableObject<string>();

				var isExecuted = false;
				v.Validations.Add(new IsNotNullOrEmptyRule<string>(c =>
				{
					isExecuted = true;
					return false;

				}, "Email must not be empty"));

				v.Validations.Add(new EmailRule<string>()
				{
					ValidationMessage = "You have entered an invalid email"
				});

				v.Validations.Add(new ActionAsyncValidationRule<string>(arg => { return Task.FromResult(false); }, "You executed async method"));

				v.Value = "t";
				Assert.True(v.IsValid);

				sched.AdvanceByMs(40);
				v.Value = "test";
				Assert.True(v.IsValid);
				sched.AdvanceByMs(200);
				v.Value = "test";
				Assert.True(isExecuted);
				Assert.False(v.IsValid);
				sched.AdvanceByMs(300);
				isExecuted = false;
				v.Value = "test";
				Assert.False(isExecuted);

				Assert.Equal("You have entered an invalid email", v.Errors[0]);
				Assert.True(v.Errors.Contains("You executed async method"));
			});


		}


		[Fact]
		public void Validate_ShouldSetIsValid_WhenExecuted()
		{
			var v = new ReactiveValidatableObject<string>();

			v.Validations.Add(new IsNotNullOrEmptyRule<string>("Email must not be empty"));

			v.Validations.Add(new EmailRule<string>()
			{
				ValidationMessage = "You have entered an invalid email"
			});

			Assert.True(v.IsValid);
			v.Validate();
			Assert.False(v.IsValid);

            v.Errors.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(2, v.Errors.Count);
                Assert.Equal("Email must not be empty", v.Errors[0]);
            };
		}

		[Fact]
		public void Validate_IsValidShouldBeTrue_WhenValidEmailIsGiven()
		{
			var v = new ReactiveValidatableObject<string>();

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
			var v = new ReactiveValidatableObject<string>();

			v.Validations.Add(new ActionAsyncValidationRule<string>(arg => { return Task.FromResult(false); }, "You have entered an invalid email"));

			Assert.True(v.IsValid);
			v.Value = "test@test.com";
			await v.ValidateAsync();
			Assert.False(v.IsValid);
            v.Errors.CollectionChanged += (sender, e) =>
           {
               Assert.True(v.Errors.Contains("You have entered an invalid email"));
           };
		}

		[Fact]
		public void Validate_ErrorsShouldContainsErrorMessages_WhenValidationIsNotValid()
		{
			var v = new ReactiveValidatableObject<string>();
			v.Validations.Add(new ActionValidationRule<string>(arg => { return false; }, "You have entered an invalid email"));
			Assert.Equal(0, v.Errors.Count);
			v.Validate();
            v.Errors.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(1, v.Errors.Count);
                Assert.True(v.Errors.Contains("You have entered an invalid email"));
            };


        }
	}
}
