#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChilliSource.Mobile;
using ChilliSource.Mobile.Api;
using ChilliSource.Mobile.Core;
using Xunit;
using RichardSzalay.MockHttp;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Api
{
	
	public class ApiAuthenticatedHandlerTests : BaseApiTests
	{
        private IApi<ITestApi> _api;
		private MockHttpMessageHandler handler;
		private readonly string baseUrl = "http://www.test.com/api";

		private ApiToken GetApiToken(string userkey = null) => new ApiToken(
				"123",
                new EnvironmentInformation("test", "app1", "av1", "AU/Sydney", "ios", "test app", "test device"),
				userkey

		);

		[Fact]
		public async Task ApiAuthenticationHandler_ShouldSetAuthenticationHeaders()
		{

			handler = new MockHttpMessageHandler();
			HttpRequestMessage request = null;

			handler.When("http://www.test.com/api/testmessage")
				   .Respond((req) =>
				   {
					   request = req;
					   var res = new HttpResponseMessage(HttpStatusCode.OK);
					   res.Content = new StringContent("Testing");
					   return res;
				   });


			var config = GetConfig(baseUrl, () => GetHandler(GetApiToken("user1"), handler));

			_api = GetApiManager<ITestApi>(config);

			await _api.Client.GetTestMessage().WaitForResponse();

			Assert.NotNull(request.Headers);
			var header = request.Headers.GetValues("ApiKey");
			Assert.True(request.Headers.Contains("ApiKey"));
			Assert.Equal("123", header.ToList().First());

			header = request.Headers.GetValues("UserKey");
			Assert.True(request.Headers.Contains("UserKey"));
			Assert.Equal("user1", header.ToList().First());

			header = request.Headers.GetValues("AppVersion");
			Assert.True(request.Headers.Contains("AppVersion"));
			Assert.Equal("av1", header.ToList().First());

			header = request.Headers.GetValues("Timezone");
			Assert.True(request.Headers.Contains("Timezone"));
			Assert.Equal("AU/Sydney", header.ToList().First());

			header = request.Headers.GetValues("Platform");
			Assert.True(request.Headers.Contains("Platform"));
			Assert.Equal("ios", header.ToList().First());

			header = request.Headers.GetValues("AppId");
			Assert.True(request.Headers.Contains("AppId"));
			Assert.Equal("app1", header.ToList().First());

		}

		[Fact]
		public async Task ApiAuthenticationHandler_ShouldNotSetAuthenticationHeaders_WhenApiToken_Is_Null()
		{
			handler = new MockHttpMessageHandler();

			HttpRequestMessage request = null;

			handler.When("http://www.test.com/api/testmessage")
				   .Respond((req) =>
				   {
					   request = req;
					   var res = new HttpResponseMessage(HttpStatusCode.OK);
					   res.Content = new StringContent("Testing");
					   return res;
				   });

			var config = GetConfig(baseUrl, () => GetHandler(null, handler));
			_api = GetApiManager<ITestApi>(config);

			await _api.Client.GetTestMessage().WaitForResponse();

			Assert.NotNull(request.Headers);
			Assert.False(request.Headers.Contains("ApiKey"));
		
			Assert.False(request.Headers.Contains("UserKey"));

			Assert.False(request.Headers.Contains("AppVersion"));

			Assert.False(request.Headers.Contains("Timezone"));

			Assert.False(request.Headers.Contains("Platform"));

			Assert.False(request.Headers.Contains("AppId"));

		}

		[Fact]
		public async Task ApiAuthenticationHandler_ShouldNotSetUserKeyAuthenticationHeaders_WhenUserKey_Is_EmptyOrWhiteSpace()
		{

			handler = new MockHttpMessageHandler();
			HttpRequestMessage request = null;

			handler.When("http://www.test.com/api/testmessage")
				   .Respond((req) =>
				   {
					   request = req;
					   var res = new HttpResponseMessage(HttpStatusCode.OK);
					   res.Content = new StringContent("Testing");
					   return res;
				   });

			var config = GetConfig(baseUrl, () => GetHandler(GetApiToken(), handler));

			_api = GetApiManager<ITestApi>(config);

			await _api.Client.GetTestMessage().WaitForResponse();

			Assert.NotNull(request.Headers);
			var header = request.Headers.GetValues("ApiKey");
			Assert.True(request.Headers.Contains("ApiKey"));
			Assert.Equal("123", header.ToList().First());

			Assert.False(request.Headers.Contains("UserKey"));

			header = request.Headers.GetValues("AppVersion");
			Assert.True(request.Headers.Contains("AppVersion"));
			Assert.Equal("av1", header.ToList().First());

			header = request.Headers.GetValues("Timezone");
			Assert.True(request.Headers.Contains("Timezone"));
			Assert.Equal("AU/Sydney", header.ToList().First());

			header = request.Headers.GetValues("Platform");
			Assert.True(request.Headers.Contains("Platform"));
			Assert.Equal("ios", header.ToList().First());

			header = request.Headers.GetValues("AppId");
			Assert.True(request.Headers.Contains("AppId"));
			Assert.Equal("app1", header.ToList().First());

		}

		[Fact]
		public async Task ApiAuthenticationHandler_ShouldReturnServiceResultWithCorrectData()
		{
			handler = new MockHttpMessageHandler();
            handler.When("http://www.test.com/api/testmessage")
				   .Respond((req) =>
				   {
					   var res = new HttpResponseMessage(HttpStatusCode.OK);
					   res.Content = new StringContent("Testing");
					   return res;
				   });

			var config = GetConfig(baseUrl, () => GetHandler(GetApiToken("user1"), handler));
			_api = GetApiManager<ITestApi>(config);

			var r = await _api.Client.GetTestMessage().WaitForResponse();
			Assert.Equal((int)HttpStatusCode.OK, r.StatusCode);
			Assert.Equal("Testing", r.Result);
		}

	}

}
