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

namespace Api
{
	
	
	public class ApiClientTests : BaseApiTests
	{

		private IApi<ITestApi> _api;
		private MockHttpMessageHandler handler;
		private readonly string baseUrl = "http://www.test.com/api";
		private ApiToken token;

        public ApiClientTests()
        {
            handler = new MockHttpMessageHandler();

            handler.When("http://www.test.com/api/sessionexpired")
                .Respond(HttpStatusCode.Unauthorized);
            token = ApiToken.Empty;
            _api = new ApiManager<ITestApi>(GetConfig(baseUrl, () => GetHandler(token, handler)));

        }


	    [Fact]
	    public async Task Execute_ShouldFireOnSessionExpired_And_ReturnFailedResult_WhenUnauthorized_And_OnSessionExpiredHandlerHasBeenSetup()
	    {

	        bool isSessionExpired = false;

			var config = GetConfig(baseUrl, () => GetHandler(token, handler));

			_api = GetApiManager<ITestApi>(config);

			var r = await _api.Client.TestSessionExpired()
			                  .WaitForResponse(new ApiExceptionHandlerConfig(onSessionExpired: result =>
								{
									isSessionExpired = true;
								}));
	
            Assert.True(isSessionExpired);
			Assert.False(r.IsSuccessful);
			Assert.Equal((int)HttpStatusCode.Unauthorized, r.StatusCode);
	    }

		[Fact]
		public async Task Execute_ShouldReturnFailedResult_WhenUnauthorized()
		{
			var r = await _api.Client.TestSessionExpired()
			                  .WaitForResponse();

            Assert.False(r.IsSuccessful);
			Assert.Equal((int)HttpStatusCode.Unauthorized, r.StatusCode);
		}


		[Fact]
		public async Task Execute_ShouldExecuteApiRequest_WhenThereIsNetwork()
		{
			handler.When("http://www.test.com/api/networktest1")
				   .Respond((req) =>
				   {
					   return new HttpResponseMessage(HttpStatusCode.OK);
				   });

			bool hasNetwork = true;

			var config = GetConfig(baseUrl, () => GetHandler(token, handler), true);

			_api = GetApiManager<ITestApi>(config);

            var r = await _api.Client.TestNoNetwork()
			                  .WaitForResponse(new ApiExceptionHandlerConfig(onNoNetworkConnectivity: result =>
			{
				hasNetwork = false;
			}));

			Assert.True(hasNetwork);
			Assert.True(r.IsSuccessful);
			Assert.Equal((int)HttpStatusCode.OK, r.StatusCode);
		}


		[Fact]
		public async Task Execute_ShouldFireNoNetworkHandler_And_ReturnFailedResult_WhenThereIsNoNetwork()
		{
 			
			bool hasNetwork = true;

			var config = GetConfig(baseUrl, () => GetHandler(token,handler), false);

			_api = GetApiManager<ITestApi>(config);

			var r = await _api.Client.TestNoNetwork()
			       .WaitForResponse(new ApiExceptionHandlerConfig(onNoNetworkConnectivity: result =>
			{
				hasNetwork = false;
			}));

			Assert.False(hasNetwork);
			Assert.False(r.IsSuccessful);
			Assert.Equal((int)HttpStatusCode.RequestTimeout, r.StatusCode);
		}


		[Fact]
		public async Task Execute_ShouldNotExecuteApiRequest_WhenThereIsNoNetwork()
		{
			
			bool hasNetwork = true;
		
			var config = GetConfig(baseUrl, () => GetHandler(token,handler), false);

			_api = GetApiManager<ITestApi>(config);

			// is this a better api
			var r = await _api.Client.TestNoNetwork2()
							      .WaitForResponse(new ApiExceptionHandlerConfig(onNoNetworkConnectivity: result =>
									{
										hasNetwork = false;
									}));


			// or this one				 
			//var r = await _api.Execute(_api.Client.TestNoNetwork2);

			Assert.False(hasNetwork);
			Assert.False(r.IsSuccessful);
			Assert.NotNull(r.Exception is ApiHandledException);
			Assert.Equal((int)HttpStatusCode.RequestTimeout, r.StatusCode);

			var ex = r.Exception as ApiHandledException;

			var content = ex.GetErrorResults(ApiConfiguration.DefaultJsonSerializationSettingsFactory());

			Assert.Equal(ErrorMessages.NoNetWorkError, content.ErrorMessage);

		}

		[Fact]
		public async Task Execute_ShouldReturnServiceResultWithCorrectData()
		{
			handler.When("http://www.test.com/api/testmessage")
				   .Respond((req) =>
				   {
					   var res = new HttpResponseMessage(HttpStatusCode.OK);
					   res.Content = new StringContent("Testing");
					   return res;
				   });


			var config = GetConfig(baseUrl, () => GetHandler(token,handler), true);

			_api = GetApiManager<ITestApi>(config);

			var r = await _api.Client.GetTestMessage().WaitForResponse();
			Assert.Equal((int)HttpStatusCode.OK, r.StatusCode);
			Assert.Equal("Testing", r.Result);
		}

	
	}


}
