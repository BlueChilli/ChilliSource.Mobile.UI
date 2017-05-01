#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Xunit;
using ChilliSource.Mobile.Api;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace Api
{

	
	public class ApiClientFactoryTests
	{
		[Fact]
		public void CreateClient_ShouldThrowException_WhenMessageHandlerFactory_Is_Null()
		{
            Assert.Throws<ArgumentNullException>(() => ApiClientFactory<ITestApi>.Create("http://www.test.com/api", null, ApiConfiguration.DefaultJsonSerializationSettingsFactory));
		}

		[Fact]
		public void CreateClient_ShouldThrowException_WhenJsonSerizationSettingsFactory_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => ApiClientFactory<ITestApi>.Create("http://www.test.com/api", () =>
			{
				return new MockHttpMessageHandler();
			}, null));
		}

		[Fact]
		public void CreateClient_ShouldReturnCorrectConcreteClient_With_Given_URL()
		{
			var isExecuted = false;

			var client = ApiClientFactory<ITestApi>.Create("http://www.test.com/api", () =>
			{
				isExecuted = true;
				return new MockHttpMessageHandler();
			}, ApiConfiguration.DefaultJsonSerializationSettingsFactory);

			Assert.True(client is ITestApi);
			Assert.True(isExecuted);
		}
	}
}
